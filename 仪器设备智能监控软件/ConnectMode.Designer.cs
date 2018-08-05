namespace 仪器设备智能监控软件V1._0
{
    partial class ConnectMode
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectMode));
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.button7 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.portConnect = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.netConnect = new System.Windows.Forms.Panel();
            this.mTBIP = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.portConnect.SuspendLayout();
            this.netConnect.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(55, 36);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "请选择设备连接方式：";
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "600",
            "1200",
            "2400",
            "4800",
            "9600",
            "14400",
            "19200",
            "28800",
            "38400",
            "57600",
            "115200",
            "230400",
            "460800"});
            this.comboBox3.Location = new System.Drawing.Point(61, 77);
            this.comboBox3.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(103, 20);
            this.comboBox3.TabIndex = 11;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(89, 128);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(68, 27);
            this.button1.TabIndex = 4;
            this.button1.Text = "连接设备";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(6, 79);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 10;
            this.label8.Text = "波特率：";
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(10, 92);
            this.tbPort.Margin = new System.Windows.Forms.Padding(2);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(148, 21);
            this.tbPort.TabIndex = 3;
            this.tbPort.Text = "8233";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(61, 19);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(103, 20);
            this.comboBox2.TabIndex = 9;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(95, 117);
            this.button7.Margin = new System.Windows.Forms.Padding(2);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(68, 27);
            this.button7.TabIndex = 5;
            this.button7.Text = "连接设备";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(8, 70);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "端口号：";
            // 
            // portConnect
            // 
            this.portConnect.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("portConnect.BackgroundImage")));
            this.portConnect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.portConnect.Controls.Add(this.button7);
            this.portConnect.Controls.Add(this.comboBox3);
            this.portConnect.Controls.Add(this.label8);
            this.portConnect.Controls.Add(this.comboBox2);
            this.portConnect.Controls.Add(this.label7);
            this.portConnect.ForeColor = System.Drawing.Color.DarkBlue;
            this.portConnect.Location = new System.Drawing.Point(57, 90);
            this.portConnect.Margin = new System.Windows.Forms.Padding(2);
            this.portConnect.Name = "portConnect";
            this.portConnect.Size = new System.Drawing.Size(176, 157);
            this.portConnect.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(6, 22);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "端口号：";
            // 
            // netConnect
            // 
            this.netConnect.BackColor = System.Drawing.Color.Transparent;
            this.netConnect.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("netConnect.BackgroundImage")));
            this.netConnect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.netConnect.Controls.Add(this.button1);
            this.netConnect.Controls.Add(this.tbPort);
            this.netConnect.Controls.Add(this.label3);
            this.netConnect.Controls.Add(this.mTBIP);
            this.netConnect.Controls.Add(this.label2);
            this.netConnect.ForeColor = System.Drawing.Color.DarkBlue;
            this.netConnect.Location = new System.Drawing.Point(57, 90);
            this.netConnect.Margin = new System.Windows.Forms.Padding(2);
            this.netConnect.Name = "netConnect";
            this.netConnect.Size = new System.Drawing.Size(178, 161);
            this.netConnect.TabIndex = 10;
            // 
            // mTBIP
            // 
            this.mTBIP.Location = new System.Drawing.Point(10, 34);
            this.mTBIP.Margin = new System.Windows.Forms.Padding(2);
            this.mTBIP.Mask = "999.999.999.999";
            this.mTBIP.Name = "mTBIP";
            this.mTBIP.PromptChar = ' ';
            this.mTBIP.Size = new System.Drawing.Size(148, 21);
            this.mTBIP.TabIndex = 1;
            this.mTBIP.Text = "10 1  24 144";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(8, 12);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "IP地址：";
            // 
            // comboBox1
            // 
            this.comboBox1.ForeColor = System.Drawing.Color.DarkBlue;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "串口连接",
            "WIFI连接",
            "网络连接"});
            this.comboBox1.Location = new System.Drawing.Point(57, 57);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(176, 20);
            this.comboBox1.TabIndex = 7;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // ConnectMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(286, 282);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.netConnect);
            this.Controls.Add(this.portConnect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConnectMode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ConnectMode";
            this.Load += new System.EventHandler(this.ConnectMode_Load);
            this.portConnect.ResumeLayout(false);
            this.portConnect.PerformLayout();
            this.netConnect.ResumeLayout(false);
            this.netConnect.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel portConnect;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel netConnect;
        private System.Windows.Forms.MaskedTextBox mTBIP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}