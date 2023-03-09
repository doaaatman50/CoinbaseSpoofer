namespace CoinbaseSpoofer;

partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.pbFront = new System.Windows.Forms.PictureBox();
        this.pbBack = new System.Windows.Forms.PictureBox();
        this.pbSelfe = new System.Windows.Forms.PictureBox();
        this.label1 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.btnStart = new System.Windows.Forms.Button();
        this.btnStop = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)(this.pbFront)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.pbBack)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.pbSelfe)).BeginInit();
        this.SuspendLayout();
        // 
        // pbFront
        // 
        this.pbFront.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.pbFront.Location = new System.Drawing.Point(12, 31);
        this.pbFront.Margin = new System.Windows.Forms.Padding(5);
        this.pbFront.Name = "pbFront";
        this.pbFront.Size = new System.Drawing.Size(160, 160);
        this.pbFront.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
        this.pbFront.TabIndex = 3;
        this.pbFront.TabStop = false;
        this.pbFront.Click += new System.EventHandler(this.PbFront_Click);
        // 
        // pbBack
        // 
        this.pbBack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.pbBack.Location = new System.Drawing.Point(182, 31);
        this.pbBack.Margin = new System.Windows.Forms.Padding(5);
        this.pbBack.Name = "pbBack";
        this.pbBack.Size = new System.Drawing.Size(160, 160);
        this.pbBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
        this.pbBack.TabIndex = 5;
        this.pbBack.TabStop = false;
        this.pbBack.Click += new System.EventHandler(this.PbBack_Click);
        // 
        // pbSelfe
        // 
        this.pbSelfe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.pbSelfe.Location = new System.Drawing.Point(352, 31);
        this.pbSelfe.Margin = new System.Windows.Forms.Padding(5);
        this.pbSelfe.Name = "pbSelfe";
        this.pbSelfe.Size = new System.Drawing.Size(160, 160);
        this.pbSelfe.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
        this.pbSelfe.TabIndex = 6;
        this.pbSelfe.TabStop = false;
        this.pbSelfe.Click += new System.EventHandler(this.PbSelfe_Click);
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(74, 10);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(37, 13);
        this.label1.TabIndex = 11;
        this.label1.Text = "Front:";
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(246, 10);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(33, 13);
        this.label2.TabIndex = 12;
        this.label2.Text = "Back:";
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(415, 10);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(35, 13);
        this.label3.TabIndex = 13;
        this.label3.Text = "Selfe:";
        // 
        // btnStart
        // 
        this.btnStart.Enabled = false;
        this.btnStart.Location = new System.Drawing.Point(178, 198);
        this.btnStart.Name = "btnStart";
        this.btnStart.Size = new System.Drawing.Size(82, 49);
        this.btnStart.TabIndex = 14;
        this.btnStart.Text = "Start";
        this.btnStart.UseVisualStyleBackColor = true;
        this.btnStart.Click += new System.EventHandler(this.BtnStart_Click);
        // 
        // btnStop
        // 
        this.btnStop.Enabled = false;
        this.btnStop.Location = new System.Drawing.Point(266, 198);
        this.btnStop.Name = "btnStop";
        this.btnStop.Size = new System.Drawing.Size(82, 49);
        this.btnStop.TabIndex = 15;
        this.btnStop.Text = "Stop";
        this.btnStop.UseVisualStyleBackColor = true;
        this.btnStop.Click += new System.EventHandler(this.BtnStop_Click);
        // 
        // MainForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(522, 259);
        this.Controls.Add(this.btnStop);
        this.Controls.Add(this.btnStart);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.pbSelfe);
        this.Controls.Add(this.pbBack);
        this.Controls.Add(this.pbFront);
        this.Name = "MainForm";
        this.ShowIcon = false;
        this.Text = "Coinbase Camera Spoofer";
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
        ((System.ComponentModel.ISupportInitialize)(this.pbFront)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.pbBack)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.pbSelfe)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.PictureBox pbFront;
    private System.Windows.Forms.PictureBox pbBack;
    private System.Windows.Forms.PictureBox pbSelfe;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button btnStart;
    private System.Windows.Forms.Button btnStop;
}