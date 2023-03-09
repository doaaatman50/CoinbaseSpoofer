using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Titanium.Web.Proxy;
using Titanium.Web.Proxy.EventArguments;
using Titanium.Web.Proxy.Models;
using CoinbaseSpoofer.Models;

namespace CoinbaseSpoofer;

public class WebProxyServer
{
    public bool IsRunning => _proxyServer.ProxyRunning;

    private readonly ProxyServer _proxyServer;
    private readonly ExplicitProxyEndPoint _explicitEndPoint;
    private Bitmap? _imgFront, _imgBack, _imgSelfe;

    public WebProxyServer(IPAddress ipAddress, int port, bool decryptSsl = true)
    {
        _proxyServer = new ProxyServer();

        _explicitEndPoint = new ExplicitProxyEndPoint(ipAddress, port, decryptSsl);
        _explicitEndPoint.BeforeTunnelConnectRequest += OnBeforeTunnelConnectRequest;

        _proxyServer.BeforeRequest += OnRequest;
    }

    public void SetFrontImage(Bitmap? imgFront) => _imgFront = imgFront;
    public void SetBackImage(Bitmap? imgBack) => _imgBack = imgBack;
    public void SetSelfeImage(Bitmap? imgSelfe) => _imgSelfe = imgSelfe;

    private Task OnBeforeTunnelConnectRequest(object sender, TunnelConnectSessionEventArgs session)
    {
        var hostname = session.HttpClient.Request.RequestUri.Host;

        if (hostname.Contains("coinbase.com"))
            return Task.FromResult(false);

        session.DecryptSsl = false;
        return Task.FromResult(true);
    }

    private async Task OnRequest(object sender, SessionEventArgs session)
    {
        // Intercept the verification process at https://www.coinbase.com/api/v2/jumio-profiles
        // And Set whatever data we want instead of the original images (front, back, selfe(Camera))
        if (session.HttpClient.Request.Method != "OPTIONS" && session.HttpClient.Request.Url == "https://www.coinbase.com/api/v2/jumio-profiles")
        {
            var body = await session.GetRequestBody().ConfigureAwait(false);
            
            // Make pattern for easier byte replace ###########{id}###########
            var bodyString = PreparePatternForByteSearch(Encoding.UTF8.GetString(body));

            body = Encoding.UTF8.GetBytes(bodyString);
            body = Replace(body, ReplaceTarget.Front);
            body = Replace(body, ReplaceTarget.Back);
            body = Replace(body, ReplaceTarget.Face);
            
            session.SetRequestBody(body);
        }
    }

    private static string PreparePatternForByteSearch(string input)
    {
        var matches = Regex.Matches(input, "name=\"([^\"]+)\"; filename=\"([^\"]+)\"(\\r?\\n)Content-Type:(.+)\\3\\3([\\w\\W]+?)\\3----");
        foreach (Match match in matches)
        {
            var id = match.Groups[1].Value;
            //var fileName = match.Groups[2].Value;
            //var newLine = match.Groups[3].Value;
            //var contentType = match.Groups[4].Value;

            if (id is "front_image" or "back_image" or "face_image")
                input = input.Replace(match.Groups[5].Value, $"###########{id}###########");
        }

        return input;
    }
    private byte[] Replace(byte[] input, ReplaceTarget target)
    {
        byte[] pattern, replacement;
        switch (target)
        {
            case ReplaceTarget.Front:
                pattern = Encoding.UTF8.GetBytes("###########front_image###########");
                replacement = ImageToBytes(_imgFront);
                break;
            case ReplaceTarget.Back:
                pattern = Encoding.UTF8.GetBytes("###########back_image###########");
                replacement = ImageToBytes(_imgBack);
                break;
            case ReplaceTarget.Face:
                pattern = Encoding.UTF8.GetBytes("###########face_image###########");
                replacement = ImageToBytes(_imgSelfe);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(target), target, null);
        }
        
        return ReplaceAllBytes(input, pattern, replacement);
    }
    public static byte[] ImageToBytes(Image? image)
    {
        if (image == null)
            return Array.Empty<byte>();

        var format = ImageFormat.Jpeg;
        using var ms = new MemoryStream();
        image.Save(ms, format);
        return ms.ToArray();
    }
    public static byte[] ReplaceAllBytes(byte[] input, byte[] pattern, byte[]? replacement)
    {
        if (input == null) throw new ArgumentNullException(nameof(input));
        if (pattern == null) throw new ArgumentNullException(nameof(pattern));
        if (input.Length == 0 || pattern.Length == 0) return input;

        var result = new List<byte>();
        replacement ??= Array.Empty<byte>();
        var patternIndex = 0;

        for (var i = 0; i < input.Length; i++)
        {
            if (input[i] == pattern[patternIndex])
            {
                patternIndex++;
                if (patternIndex == pattern.Length)
                {
                    result.AddRange(replacement);
                    patternIndex = 0;
                }
            }
            else
            {
                if (patternIndex > 0)
                {
                    result.AddRange(pattern.Take(patternIndex));
                    patternIndex = 0;
                }
                result.Add(input[i]);
            }
        }

        if (patternIndex > 0) result.AddRange(pattern.Take(patternIndex));

        return result.ToArray();
    }

    public void Start()
    {
        if (_proxyServer.ProxyRunning || _imgFront == null || _imgBack == null || _imgSelfe == null)
            return;

        _proxyServer.AddEndPoint(_explicitEndPoint);
        _proxyServer.Start();
        _proxyServer.SetAsSystemProxy(_explicitEndPoint, ProxyProtocolType.AllHttp);
    }
    public void Stop()
    {
        if (_proxyServer.ProxyRunning)
            _proxyServer.Stop();
    }
    public void Finish()
    {
        _explicitEndPoint.BeforeTunnelConnectRequest -= OnBeforeTunnelConnectRequest;

        _proxyServer.BeforeRequest -= OnRequest;

        Stop();
    }
}