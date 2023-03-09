using System;
using System.Drawing;
using System.Net;
using System.Windows.Forms;

namespace CoinbaseSpoofer;

public partial class MainForm : Form
{
    private readonly WebProxyServer _webProxyServer;
    private Bitmap? _imgFront, _imgBack, _imgSelfe;

    public MainForm()
    {
        InitializeComponent();

        _webProxyServer = new WebProxyServer(IPAddress.Any, 8080);
    }

    private void PbFront_Click(object sender, EventArgs e)
    {
        // Get front image

        using var ofd = new OpenFileDialog();
        ofd.Filter = @"Images|*.jpg;*.jpeg|All Files|*.*";
        if (ofd.ShowDialog() != DialogResult.OK) return;

        try
        {
            pbFront.Image = null;
            _imgFront = new Bitmap(ofd.FileName);
            _webProxyServer.SetFrontImage(_imgFront);
            pbFront.Image = _imgFront;
            if (_imgBack != null && _imgSelfe != null)
                btnStart.Enabled = true;
        }
        catch (Exception ex)
        {
            _imgFront = null;
            _webProxyServer.SetFrontImage(_imgFront);
            btnStart.Enabled = false;
            MessageBox.Show(ex.Message);
        }
    }
    private void PbBack_Click(object sender, EventArgs e)
    {
        // Get Back image

        using var ofd = new OpenFileDialog();
        ofd.Filter = @"Images|*.jpg;*.jpeg|All Files|*.*";
        if (ofd.ShowDialog() != DialogResult.OK) return;

        try
        {
            pbBack.Image = null;
            _imgBack = new Bitmap(ofd.FileName);
            _webProxyServer.SetBackImage(_imgBack);
            pbBack.Image = _imgBack;

            if (_imgFront != null && _imgSelfe != null)
                btnStart.Enabled = true;
        }
        catch (Exception ex)
        {
            _imgBack = null;
            _webProxyServer.SetBackImage(_imgBack);
            btnStart.Enabled = false;
            MessageBox.Show(ex.Message);
        }
    }
    private void PbSelfe_Click(object sender, EventArgs e)
    {
        // Get Selfe image

        using var ofd = new OpenFileDialog();
        ofd.Filter = @"Images|*.jpg;*.jpeg|All Files|*.*";
        if (ofd.ShowDialog() != DialogResult.OK) return;

        try
        {
            pbSelfe.Image = null;
            _imgSelfe = new Bitmap(ofd.FileName);
            _webProxyServer.SetSelfeImage(_imgSelfe);
            pbSelfe.Image = _imgSelfe;

            if (_imgFront != null && _imgBack != null)
                btnStart.Enabled = true;
        }
        catch (Exception ex)
        {
            _imgSelfe = null;
            _webProxyServer.SetSelfeImage(_imgSelfe);
            btnStart.Enabled = false;
            MessageBox.Show(ex.Message);
        }
    }
    
    private void BtnStart_Click(object sender, EventArgs e)
    {
        if (_webProxyServer.IsRunning) return;

        FormWorking(true);
        _webProxyServer.Start();
    }
    private void BtnStop_Click(object sender, EventArgs e)
    {
        if (!_webProxyServer.IsRunning) return;

        FormWorking(false);
        _webProxyServer.Stop();
    }

    private void FormWorking(bool working)
    {
        pbFront.Enabled = !working;
        pbBack.Enabled = !working;
        pbSelfe.Enabled = !working;
        btnStart.Enabled = !working;
        btnStop.Enabled = working;
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        _webProxyServer.Finish();
    }
}