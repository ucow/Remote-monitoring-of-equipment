 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Drawing.Drawing2D;
using System.Net.NetworkInformation;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;
using System.IO.Ports;

namespace 仪器设备智能监控软件V1._0
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        #region
        private IPAddress serverIP;//IP
        private int serverPort;//端口
        private IPEndPoint IpEndPort;//IP和端口
        private Socket clientSocket;//套接字
        Thread threadConnect = null;

        private int DX;//断续标志位
        private int LX;//连续标志位
        private int flag;//网络连接成功标志位
        private int aaa;//网络连接不成功标志位
        private int ONandOFF;//设备连接成功标志位
        Byte[] bytes = new Byte[1];//发送字节数组

        //定义画笔工具
        Pen pen;
        Pen pen1;
        Pen pen2;
        Pen pen3;
        Pen pen4;
        Pen pen5;
        Pen pen6;
        Pen pen7;
        private Pen wg1;
        //private Pen wg2;
        //private Pen wg3;     
        //private Pen wg4;

        //定义曲线初始X坐标
        private int x1 = 50;
        private int x2 = 50;
        private int x3 = 50;
        private int x4 = 50;
        private int x5 = 50;
        private int x6 = 50;
        private int x7 = 50;

        string[] tmp, tmp1;
        public static int picwidth5 = 682, picwidth2 = 682, picwidth1 = 682, picwidth4 = 682;
        public static int picheight5 = 150, picheight2 = 150, picheight1 = 150, picheight4 = 150;

        PictureBox p1;
        PictureBox p2;
        PictureBox p3;
        Label l1;
        Label l2;
        Label l3;

        // 定义位图
        Bitmap bm1 = new Bitmap(1108, 179);
        Bitmap bm2 = new Bitmap(1108, 179);
        Bitmap bm3 = new Bitmap(1108, 179);
        Bitmap bm4 = new Bitmap(1108, 179);
        Bitmap bm5 = new Bitmap(1108, 179);
        Bitmap bm6 = new Bitmap(1108, 179);
        Bitmap bm7 = new Bitmap(1108, 179);
        //定义画布
        private Graphics pic1;
        private Graphics pic2;
        private Graphics pic3;
        private Graphics pic4;
        private Graphics pic5;
        private Graphics pic6;
        private Graphics pic7;

        float light;                
        float humi;
        float windspeed;
        int[] value = new int[8];

        int oldFrmWidth;
        int oldFrmHeight;

        //定义存放IP地址和用户名的集合
        List<string> ipList = new List<string>();
        public string str1 = "", strHost = "";

        float ScaleX = 1;
        float ScaleY = 1;

        Graphics g;
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.Location = new Point(162,0);

            path = comboBox2.Text + "数据信息.txt";
            netConnect.Visible = true;
            portConnect.Visible = false;

            this.toolStripStatusLabel3.Alignment = ToolStripItemAlignment.Right;// toolStripStatusLabel3右对齐

            #region 添加lable控件和picturebox控件
            l1 = new Label();
            int x = label20.Location.X - pictureBox4.Location.X;
            int y = label20.Location.Y - pictureBox4.Location.Y;

            l1.Location = new Point(pictureBox4.Location.X + x, (pictureBox4.Location.Y + (pictureBox4.Height + 10)) + y - 10);
            l1.Text = "温度 0℃";
            l1.Font = label17.Font;
            l1.BackColor = label20.BackColor;
            l1.ForeColor = label20.ForeColor;
            tabPage1.Controls.Add(l1);

            p1 = new PictureBox();
            p1.Width = pictureBox4.Width;
            p1.Height = pictureBox4.Height;
            p1.BackColor = pictureBox4.BackColor;

            p1.Location = new Point(pictureBox4.Location.X, pictureBox4.Location.Y + pictureBox4.Height + 5);
            p1.Tag = p1.Left.ToString() + "," + p1.Top.ToString() + " "
                            + p1.Width.ToString() + "," + p1.Height.ToString();
            p1.Name = "p1";
            tabPage1.Controls.Add(p1);

            l2 = new Label();
            x = label20.Location.X - pictureBox4.Location.X;
            y = label20.Location.Y - pictureBox4.Location.Y;
            l2.Location = new Point(pictureBox4.Location.X + x, (pictureBox4.Location.Y + (pictureBox4.Height + 10) * 2) + y - 10);
            l2.Text = "湿度 0%";
            l2.Font = label17.Font;
            l2.BackColor = label20.BackColor;
            l2.ForeColor = label20.ForeColor;
            tabPage1.Controls.Add(l2);

            p2 = new PictureBox();
            p2.Width = p1.Width;
            p2.Height = p1.Height;
            p2.BackColor = pictureBox4.BackColor;
            p2.Location = new Point(p1.Location.X, p1.Location.Y + p1.Height + 5);
            p2.Tag = p2.Left.ToString() + "," + p2.Top.ToString() + " "
                            + p2.Width.ToString() + "," + p2.Height.ToString();
            p2.Name = "p2";
            tabPage1.Controls.Add(p2);

            l3 = new Label();
            x = label20.Location.X - pictureBox4.Location.X;
            y = label20.Location.Y - pictureBox4.Location.Y;
            l3.Location = new Point(pictureBox4.Location.X + x, (pictureBox4.Location.Y + (pictureBox4.Height + 10) * 3) + y - 10);
            l3.Text = "风速 0m/s";
            l3.Font = label17.Font;
            l3.BackColor = label20.BackColor;
            l3.ForeColor = label20.ForeColor;
            tabPage1.Controls.Add(l3);

            p3 = new PictureBox();
            p3.Width = p1.Width;
            p3.Height = p1.Height;
            p3.BackColor = pictureBox4.BackColor;
            p3.Location = new Point(p1.Location.X, p2.Location.Y + p2.Height + 5);
            p3.Tag = p3.Left.ToString() + "," + p3.Top.ToString() + " "
                            + p3.Width.ToString() + "," + p3.Height.ToString();
            p3.Name = "p3";
            tabPage1.Controls.Add(p3);
            #endregion
            ResizeInit(this);
            //oldFrmWidth = this.Width;
            //oldFrmHeight = this.Height;
            this.MinimumSize = this.Size;
            #region 定义画布
            pic1 = Graphics.FromImage(bm1);
            pic2 = Graphics.FromImage(bm2);
            pic3 = Graphics.FromImage(bm3);
            pic4 = Graphics.FromImage(bm4);
            pic5 = Graphics.FromImage(bm5);
            pic6 = Graphics.FromImage(bm6);
            pic7 = Graphics.FromImage(bm7);
            #endregion

            timer4.Enabled = true;
            timer4.Start();

            timer1.Start();

            #region 定义画笔
            pen = new Pen(Color.White, 2);
            pen1 = new Pen(Color.Red, 2);
            pen2 = new Pen(Color.Orange, 2);
            pen3 = new Pen(Color.Yellow, 2);
            pen4 = new Pen(Color.Green, 2);
            pen5 = new Pen(Color.LightGreen, 2);
            pen6 = new Pen(Color.Blue, 2);
            pen7 = new Pen(Color.Purple, 2);

            wg1 = new Pen(Color.Black, 1);
            wg1.DashStyle = (DashStyle)1;
            #endregion
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);

            //为tabpage中的控件添加双击时间
            foreach (Control c in tabPage1.Controls)
            {
                if (c is Label)
                {
                    //
                    c.DoubleClick += new System.EventHandler(c_DoubleClick);
                }
                else
                {
                    if (c is PictureBox)
                    {
                        //为每个PictureBox添加双击事件
                        c.DoubleClick += new System.EventHandler(c_DoubleClick_pen);
                    }
                }
            }

            //添加timer控件，避免窗体跳动
            System.Windows.Forms.Timer t1 = new System.Windows.Forms.Timer();
            t1.Enabled = true;
            t1.Interval = 1;
            t1.Tick += new EventHandler(t1_Tick);
        }

        void t1_Tick(object sender, EventArgs e)
        {

            if (tabPage1.AutoScrollPosition.Y == -200)
            {
                tabPage1.AutoScrollPosition = new Point(0, 473);
            }

            if (this.WindowState == FormWindowState.Maximized)
            {
                if (tabPage1.AutoScrollPosition.Y == -234)
                {
                    tabPage1.AutoScrollPosition = new Point(0, 532);
                }
            }
        }

        //窗体大小改变时为控件重新定义大小和位置
        private void ResizeInit(Form frm)
        {
            oldFrmWidth = this.Width;
            oldFrmHeight = this.Height;

            foreach (Control control in frm.Controls)
            {
                control.Tag = control.Left.ToString() + "," + control.Top.ToString() + ","
                            + control.Width.ToString() + "," + control.Height.ToString();
            }

            foreach (TabPage page in tabControl1.TabPages)
            {
                foreach (Control controls in page.Controls)
                {
                    controls.Tag = controls.Left.ToString() + "," + controls.Top.ToString() + ","
                           + controls.Width.ToString() + "," + controls.Height.ToString();
                }
            }
            foreach (Control group1 in groupBox1.Controls)
            {
                group1.Tag = group1.Left.ToString() + "," + group1.Top.ToString() + ","
                           + group1.Width.ToString() + "," + group1.Height.ToString();
            }
            foreach (Control group2 in groupBox2.Controls)
            {
                group2.Tag = group2.Left.ToString() + "," + group2.Top.ToString() + ","
                           + group2.Width.ToString() + "," + group2.Height.ToString();
                foreach (Control group3 in groupBox3.Controls)
                {
                    group3.Tag = group3.Left.ToString() + "," + group3.Top.ToString() + ","
                               + group3.Width.ToString() + "," + group3.Height.ToString();
                }
            }

        }

        //连接网络
        private void Connect()
        {
            try
            {
                clientSocket.Connect(IpEndPort);
                string str = "断开连接";

                //委托 连接成功后调用窗体控件
                Invoke(new ChangeTextDelegate(ChangeText), new object[] { str });
            }
            catch
            {
                //委托 连接失败后调用窗体控件
                Invoke(new ConnectDieDelegate(ChangEnable), new object[] { });

                MessageBox.Show("连接失败！请检查网络后重新上传。");
                aaa = 1;
            }
            if (clientSocket.Connected)
            {
                MessageBox.Show("连接成功", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                flag = 1;
            }
        }

        //网络连接成功后修改控件状态
        public delegate void ChangeTextDelegate(string str);
        void ChangeText(string str)
        {
            button5.Text = str;
            //button5.BackColor = Color.Orange;
            //label11.ForeColor = Color.DarkRed;
            label11.Text = "网络连接状态：连接成功";
        }

        //网络连接失败后修改控件状态
        public delegate void ConnectDieDelegate();
        void ChangEnable()
        {
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
            textBox1.Enabled = true;
        }

        //接受线程
        private void ReceiveThread(object x)
        {
            try
            {
                //定义数组 存放数据
                byte[] receiveByte = new byte[24];
                clientSocket.Receive(receiveByte, receiveByte.Length, SocketFlags.None);
                //数据处理
                ShowMessage(receiveByte);
            }
            catch
            {

            }
        }

        //接收数据后，进行数据处理
        delegate void ShowMessCallback(byte[] message);
        void ShowMessage(byte[] message)
        {
            if (this.InvokeRequired) this.Invoke(new ShowMessCallback(ShowMessage), new Object[] { message });
            else
            {
                //timer3.Enabled = true;
                //timer3.Start();
                for (int k = 0; k < 8; k++)//温度
                {
                    value[k] = message[k];
                }
                //风速 
                if (Int32.Parse(value[2].ToString("D2")) == 04)
                {
                    windspeed = Convert.ToInt32((value[3].ToString("x") + value[4].ToString("x")), 16) / 100;
                    l3.Text = "风速 " + windspeed.ToString() + "m/s";
                    label15.Text = l3.Text;
                }
                //光强
                if (Int32.Parse(value[2].ToString("D2")) == 01)
                {
                    light = Convert.ToInt32((value[3].ToString("x") + value[4].ToString("x") + value[5].ToString("x") + value[6].ToString("x")), 16);
                    label17.Text = "光照 " + light.ToString() + "Lux";
                    label3.Text = label17.Text;
                }
                //温度
                if (Int32.Parse(value[2].ToString("D2")) == 02)
                {
                    float num = Convert.ToInt32((value[3].ToString("x") + value[4].ToString("x")), 16);
                    if (num <= 32768)
                    {
                        temp = num;
                    }
                    else if (num > 32768)
                    {
                        temp = (32768 - num) / 10;
                    }
                    humi = Convert.ToInt32((value[5].ToString("x") + value[6].ToString("x")), 16) / 10;
                    l1.Text = "温度 " + temp.ToString() + "℃";
                    l2.Text = "湿度 " + humi.ToString() + "%";
                    label13.Text = l1.Text;
                    label14.Text = l2.Text;
                }

                //频率
                if (Int32.Parse(value[2].ToString("D2")) == 05)
                {
                    PLL = Convert.ToInt32((value[3].ToString("x") + value[4].ToString("x")), 16);
                    label20.Text = "频率 " + (PLL / 1000).ToString() + "KHz";
                    label12.Text = label20.Text;
                }

                //电压
                if (Int32.Parse(value[2].ToString("D2")) == 10)
                {

                    float s = Convert.ToInt32((value[3].ToString("x") + value[4].ToString("x")), 16);
                    DYY = s / 1023 * 5;
                    label19.Text = "电压 " + DYY.ToString("f2") + "V";
                    label10.Text = label19.Text;
                }

                //电流
                if (Int32.Parse(value[2].ToString("D2")) == 11)
                {
                    float s = Convert.ToInt32((value[3].ToString("x") + value[4].ToString("x")), 16);
                    DLL = s / 1023 * 5;
                    label18.Text = "电流 " + DLL.ToString("f2") + "A";
                    label9.Text = label18.Text;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (button5.Text == "连接设备")
            {
                try
                {
                    //获取IP
                    serverIP = IPAddress.Parse(textBox1.Text);
                    //获取端口号
                    serverPort = Int32.Parse(comboBox1.Text);
                    //初始化IPEndPort对象
                    IpEndPort = new IPEndPoint(serverIP, serverPort);
                    //初始化Socket对象
                    clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    //初始化连接线程
                    threadConnect = new Thread(new ThreadStart(Connect));
                    //开始线程
                    threadConnect.Start();

                    comboBox1.Enabled = false;
                    comboBox2.Enabled = false;
                    textBox1.Enabled = false;
                }
                catch (Exception)
                {
                    if (textBox1.Text == "" || comboBox1.Text == "")
                    {
                        if (textBox1.Text == "")
                            MessageBox.Show("ip无效，请重新选择！");
                        if (comboBox1.Text == "")
                            MessageBox.Show("端口号无效！请重新选择！");
                    }
                    else
                        MessageBox.Show("无对应的信号机连接，请检查ip地址！");
                }

            }
            else if (button5.Text == "断开连接")
            {
                if (ONandOFF != 0)
                {
                    MessageBox.Show("请先关闭设备!", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    //关闭Socket
                    clientSocket.Close();
                    //释放对象
                    clientSocket.Dispose();
                    //MessageBox.Show("断开连接成功");
                    flag = 0;
                    button5.Text = "连接设备";
                    button5.BackColor = Color.Transparent;
                    label11.ForeColor = Color.Black;
                    label11.Text = "网络连接状态：未连接";
                    ONandOFF = 1;
                    button4.BackColor = Color.WhiteSmoke;
                    label16.ForeColor = Color.Black;
                    label16.Text = "设备连接状态：未连接";
                    label17.Text = "光照 0Lux";
                    label18.Text = "电流 0A";
                    label19.Text = "电压 0V";
                    label20.Text = "频率 0KHz";
                    l1.Text = "温度 0℃";
                    l2.Text = "湿度 0%";
                    l3.Text = "风速 0m/s";


                    timer2.Enabled = false;


                    timer3.Stop();
                    timer3.Enabled = false;

                    timer4.Stop();
                    timer4.Enabled = false;

                    pic1.Clear(Color.DarkSlateGray);
                    pic2.Clear(Color.DarkSlateGray);
                    pic3.Clear(Color.DarkSlateGray);
                    pic4.Clear(Color.DarkSlateGray);
                    pic5.Clear(Color.DarkSlateGray);
                    pic6.Clear(Color.DarkSlateGray);
                    pic7.Clear(Color.DarkSlateGray);

                    progressBar1.Value = 0;

                    button1.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = true;


                    comboBox1.Enabled = true;
                    comboBox2.Enabled = true;
                    textBox1.Enabled = true;
                }
            }
        }

        //画表格
        private void timer1_Tick(object sender, EventArgs e)
        {
            pic1.DrawLine(pen, 50, 0, 50, pictureBox5.Height);//y
            pic1.DrawLine(pen, 50, pictureBox5.Height - 5, pictureBox5.Width, pictureBox5.Height - 5);//x

            pic1.DrawLine(wg1, 50, 50, pictureBox5.Width, 50);
            pic1.DrawLine(wg1, 50, 50 + 15 * ScaleY, pictureBox5.Width, 50 + 15 * ScaleY);
            pic1.DrawLine(wg1, 50, 50 + 30 * ScaleY, pictureBox5.Width, 50 + 30 * ScaleY);
            pic1.DrawLine(wg1, 50, 50 + 45 * ScaleY, pictureBox5.Width, 50 + 45 * ScaleY);
            pic1.DrawLine(wg1, 50, 50 + 60 * ScaleY, pictureBox5.Width, 50 + 60 * ScaleY);
            pic1.DrawLine(wg1, 50, 50 + 75 * ScaleY, pictureBox5.Width, 50 + 75 * ScaleY);
            int j = 0;
            for (int i = 6; i >= 0; i--)
            {
                pic1.DrawString((230 * (j - 1)).ToString(), new Font("宋体", 8), new SolidBrush(Color.White), new PointF(25, 50 + 15 * (i + 1) * ScaleY - 3));
                j++;
            }
            pic1.Save();
            pictureBox5.Image = bm1;

            pic2.DrawLine(pen, 50, 0, 50, pictureBox2.Height);//y
            pic2.DrawLine(pen, 50, pictureBox2.Height - 5, pictureBox2.Width, pictureBox2.Height - 5);//x

            pic2.DrawLine(wg1, 50, 50, pictureBox2.Width, 50);
            pic2.DrawLine(wg1, 50, 50 + 15 * ScaleY, pictureBox2.Width, 50 + 15 * ScaleY);
            pic2.DrawLine(wg1, 50, 50 + 30 * ScaleY, pictureBox2.Width, 50 + 30 * ScaleY);
            pic2.DrawLine(wg1, 50, 50 + 45 * ScaleY, pictureBox2.Width, 50 + 45 * ScaleY);
            pic2.DrawLine(wg1, 50, 50 + 60 * ScaleY, pictureBox2.Width, 50 + 60 * ScaleY);
            pic2.DrawLine(wg1, 50, 50 + 75 * ScaleY, pictureBox2.Width, 50 + 75 * ScaleY);

            int x = 1;
            for (int i = 6; i >= 0; i--)
            {
                pic2.DrawString((2 * (x - 1)).ToString(), new Font("宋体", 8), new SolidBrush(Color.White), new PointF(35, 50 + 15 * i * ScaleY));
                x++;
            }
            pic2.Save();
            pictureBox2.Image = bm2;

            pic3.DrawLine(pen, 50, 0, 50, pictureBox1.Height);//y
            pic3.DrawLine(pen, 50, pictureBox1.Height - 5, pictureBox1.Width, pictureBox1.Height - 5);//x

            pic3.DrawLine(wg1, 50, 50, pictureBox1.Width, 50);
            pic3.DrawLine(wg1, 50, 50 + 15 * ScaleY, pictureBox1.Width, 50 + 15 * ScaleY);
            pic3.DrawLine(wg1, 50, 50 + 30 * ScaleY, pictureBox1.Width, 50 + 30 * ScaleY);
            pic3.DrawLine(wg1, 50, 50 + 45 * ScaleY, pictureBox1.Width, 50 + 45 * ScaleY);
            pic3.DrawLine(wg1, 50, 50 + 60 * ScaleY, pictureBox1.Width, 50 + 60 * ScaleY);
            pic3.DrawLine(wg1, 50, 50 + 75 * ScaleY, pictureBox1.Width, 50 + 75 * ScaleY);
            int y = 1;
            for (int i = 6; i >= 0; i--)
            {
                pic3.DrawString((1.5 * (y - 1)).ToString(), new Font("宋体", 8), new SolidBrush(Color.White), new PointF(28, 50 + 15 * i * ScaleY));
                y++;
            }
            pic3.Save();
            pictureBox1.Image = bm3;

            pic4.DrawLine(pen, 50, 0, 50, pictureBox4.Height);//y
            pic4.DrawLine(pen, 50, pictureBox4.Height - 5, pictureBox4.Width, pictureBox4.Height - 5);//x

            pic4.DrawLine(wg1, 50, 50, pictureBox4.Width, 50);
            pic4.DrawLine(wg1, 50, 50 + 15 * ScaleY, pictureBox4.Width, 50 + 15 * ScaleY);
            pic4.DrawLine(wg1, 50, 50 + 30 * ScaleY, pictureBox4.Width, 50 + 30 * ScaleY);
            pic4.DrawLine(wg1, 50, 50 + 45 * ScaleY, pictureBox4.Width, 50 + 45 * ScaleY);
            pic4.DrawLine(wg1, 50, 50 + 60 * ScaleY, pictureBox4.Width, 50 + 60 * ScaleY);
            pic4.DrawLine(wg1, 50, 50 + 75 * ScaleY, pictureBox4.Width, 50 + 75 * ScaleY);

            int z = 1;
            for (int i = 6; i >= 0; i--)
            {
                pic4.DrawString((30000 / 1000 * (z - 1)).ToString(), new Font("宋体", 8), new SolidBrush(Color.White), new PointF(30, 50 + 15 * i * ScaleY));
                z++;
            }
            pic4.Save();
            pictureBox4.Image = bm4;

            pic5.DrawLine(pen, 50, 0, 50, p1.Height);//y
            pic5.DrawLine(pen, 50, p1.Height - 5, p1.Width, p1.Height - 5);//x

            pic5.DrawLine(wg1, 50, 50, p1.Width, 50);
            pic5.DrawLine(wg1, 50, 50 + 15 * ScaleY, p1.Width, 50 + 15 * ScaleY);
            pic5.DrawLine(wg1, 50, 50 + 30 * ScaleY, p1.Width, 50 + 30 * ScaleY);
            pic5.DrawLine(wg1, 50, 50 + 45 * ScaleY, p1.Width, 50 + 45 * ScaleY);
            pic5.DrawLine(wg1, 50, 50 + 60 * ScaleY, p1.Width, 50 + 60 * ScaleY);
            pic5.DrawLine(wg1, 50, 50 + 75 * ScaleY, p1.Width, 50 + 75 * ScaleY);

            for (int i = 3; i >= -3; i--)
            {
                pic5.DrawString((15 * i).ToString(), new Font("宋体", 8), new SolidBrush(Color.White), new PointF(30, 45 + 15 * (i + 3) * ScaleY));
            }
            pic5.Save();
            p1.Image = bm5;

            pic6.DrawLine(pen, 50, 0, 50, p2.Height);//y
            pic6.DrawLine(pen, 50, p2.Height - 5, p2.Width, p2.Height - 5);//x

            pic6.DrawLine(wg1, 50, 50, p2.Width, 50);
            pic6.DrawLine(wg1, 50, 50 + 15 * ScaleY, p2.Width, 50 + 15 * ScaleY);
            pic6.DrawLine(wg1, 50, 50 + 30 * ScaleY, p2.Width, 50 + 30 * ScaleY);
            pic6.DrawLine(wg1, 50, 50 + 45 * ScaleY, p2.Width, 50 + 45 * ScaleY);
            pic6.DrawLine(wg1, 50, 50 + 60 * ScaleY, p2.Width, 50 + 60 * ScaleY);
            pic6.DrawLine(wg1, 50, 50 + 75 * ScaleY, p2.Width, 50 + 75 * ScaleY);

            //for (int i = 0; i<= 5; i++)
            //{
            //    pic6.DrawString((15 * i).ToString(), new Font("宋体", 8), new SolidBrush(Color.White), new PointF(30, 15 * (i + 3) * ScaleY));
            //}
            pic6.Save();
            p2.Image = bm6;

            pic7.DrawLine(pen, 50, 0, 50, p3.Height);//y
            pic7.DrawLine(pen, 50, p3.Height - 5, p3.Width, p3.Height - 5);//x

            pic7.DrawLine(wg1, 50, 50, p3.Width, 50);
            pic7.DrawLine(wg1, 50, 50 + 15 * ScaleY, p3.Width, 50 + 15 * ScaleY);
            pic7.DrawLine(wg1, 50, 50 + 30 * ScaleY, p3.Width, 50 + 30 * ScaleY);
            pic7.DrawLine(wg1, 50, 50 + 45 * ScaleY, p3.Width, 50 + 45 * ScaleY);
            pic7.DrawLine(wg1, 50, 50 + 60 * ScaleY, p3.Width, 50 + 60 * ScaleY);
            pic7.DrawLine(wg1, 50, 50 + 75 * ScaleY, p3.Width, 50 + 75 * ScaleY);
            pic7.Save();
            p3.Image = bm7;

            DateTime dt = System.DateTime.Now;
            toolStripStatusLabel3.Text = dt.ToString();
            //label12.Text = tabPage1.AutoScrollPosition.ToString();
            //l3.Text = tabPage1.AutoScrollPosition.ToString();
            //label21.Text = l3.Text;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                if (ONandOFF == 0)//按下
                {
                    ONandOFF = 1;

                    //button4.BackColor = Color.Orange;
                    //label16.ForeColor = Color.DarkRed;
                    label16.Text = "设备连接状态：已连接";

                    //清空画布
                    pic1.Clear(pictureBox5.BackColor);
                    pic4.Clear(pictureBox4.BackColor);
                    pic2.Clear(pictureBox2.BackColor);
                    pic3.Clear(pictureBox1.BackColor);
                    pic5.Clear(p1.BackColor);
                    pic6.Clear(p2.BackColor);
                    pic7.Clear(p3.BackColor);

                }
                else if (ONandOFF == 1)//再按下
                {
                    ONandOFF = 0;

                    button4.BackColor = Color.WhiteSmoke;
                    label16.ForeColor = Color.Black;
                    label16.Text = "设备连接状态：未连接";

                    progressBar1.Value = 0;

                    gd = 0;
                    gd1 = 0;
                    timer2.Stop();
                    timer2.Enabled = false;

                    timer3.Stop();
                    timer3.Enabled = false;

                    timer4.Stop();
                    timer4.Enabled = false;

                    x1 = 50;
                    x2 = 50;
                    x3 = 50;
                    x4 = 50;
                    x5 = 50;
                    x6 = 50;
                    x7 = 50;

                    button1.BackColor = Color.Transparent;
                    button2.BackColor = Color.Transparent;
                    button3.BackColor = Color.Transparent;

                }
            }
            else
            {
                MessageBox.Show("请先正确连接设备！", "错误警告");
            }
        }
        int gd = 1;
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (flag == 1)
                {
                    if (ONandOFF == 1)//按下
                    {
                        progressBar1.Value += 25;

                        if (progressBar1.Value >= 100)
                        {
                            progressBar1.Value = 100;
                        }

                    }
                    gd = 0;
                }
            }
            catch (ArgumentOutOfRangeException)
            {

                MessageBox.Show("数值超过范围");
            }
        }
        int gd1 = 1;
        //增加频率
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (flag == 1)
                {
                    if (ONandOFF == 1)//按下，（背景变红，发送0x01）
                    {
                        progressBar1.Value -= 25;
                        if (progressBar1.Value <= 0)
                        {
                            progressBar1.Value = 0;
                        }
                    }
                    gd1 = 0;
                }
            }
            catch (ArgumentOutOfRangeException)
            {

                MessageBox.Show("数值超过范围");
            }
        }

        //添加频率
        private void button8_Click(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                if (ONandOFF == 1)//按下（发送0x06）
                {
                    LX = 1;

                    if (clientSocket.Connected == true)
                    {
                        bytes[0] = Byte.Parse("6");
                        clientSocket.Send(bytes, bytes.Length, 0);
                    }
                }
            }
            else
            {
                MessageBox.Show("请先正确连接设备！", "错误警告");
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                if (ONandOFF == 1)//按下（发送0x07）
                {
                    DX = 1;

                    if (clientSocket.Connected == true)
                    {
                        bytes[0] = Byte.Parse("7");
                        clientSocket.Send(bytes, bytes.Length, 0);
                    }
                }
            }
            else
            {
                MessageBox.Show("请先正确连接设备！", "错误警告");
            }
        }

        System.Threading.Timer timer = null;
        private void button1_Click(object sender, EventArgs e)
        {
            //1
            if (flag == 1 && ONandOFF == 1)
            {

                if (clientSocket.Connected == true)
                {

                    //button2.Enabled = false;
                    //button3.Enabled = false;

                    timer3.Start();
                    timer4.Enabled = true;
                    timer4.Start();

                    timer = new System.Threading.Timer(new TimerCallback(ReceiveThread), null, 0, 1);

                    timer2.Enabled = true;

                    for (int abd = 0; abd < 100000; abd++) ;

                    //timer2.Start();
                    //button1.BackColor = Color.Orange;
                    //button1.Enabled = false;


                }

            }
            else
            {
                MessageBox.Show("请先正确连接设备！", "错误警告");

            }
        }


        float cs1 = 0;
        float cs2 = 0;
        float cs3 = 0;
        float cs4 = 0;
        float cs5 = 0;
        float cs6 = 0;
        float cs7 = 0;

        //将处理好的数据画图显示
        
        private void Graphic()
        {
            if (light / 10 <= 100)
            {
                pic1.DrawLine(pen1, x1, (pictureBox5.Height - cs1 - 7) / ScaleY, x1 + 1, (pictureBox5.Height - light / 10 - 7) / ScaleY);
                cs1 = light / 10;
                pic1.Save();
                pictureBox5.Image = bm1;
                x1++;
            }
            else
            {
                pic1.DrawLine(pen1, x1, (pictureBox5.Height - cs1 - 7) / ScaleY, x1 + 1, (pictureBox5.Height - 100 - 7) / ScaleY);
                cs1 = 100;
                pic1.Save();
                pictureBox5.Image = bm1;
                x1++;
            }
            if (x1 >= pictureBox5.Width)
            {
                x1 = 50;
                pic1.Clear(Color.DarkSlateGray);
                label17.BackColor = Color.DarkSlateGray;
            }

            pic2.DrawLine(pen2, x2, (pictureBox2.Height - cs2 - 7) / ScaleY, x2 + 1, (pictureBox2.Height - DLL * 10 - 7) / ScaleY);
            cs2 = DLL * 10;
            pic2.Save();
            pictureBox2.Image = bm2;
            x2++;
            if (x2 >= pictureBox2.Width)
            {
                x2 = 50;
                pic2.Clear(Color.DarkSlateGray);
                label18.BackColor = Color.DarkSlateGray;
            }




            pic3.DrawLine(pen3, x3, (pictureBox1.Height - cs3 - 7) / ScaleY, x3 + 1, (pictureBox1.Height - DYY * 10 - 7) / ScaleY);
            cs3 = DYY * 10;
            pic3.Save();
            pictureBox1.Image = bm3;
            x3++;
            if (x3 >= pictureBox1.Width)
            {
                x3 = 50;
                pic3.Clear(Color.DarkSlateGray);
                label19.BackColor = Color.DarkSlateGray;
            }



            pic4.DrawLine(pen4, x4, (pictureBox4.Height - cs4 - 7) / ScaleY, x4 + 1, (pictureBox4.Height - PLL / 1500 - 7) / ScaleY);
            cs4 = PLL / 1500;
            pic4.Save();
            pictureBox4.Image = bm4;
            x4++;

            if (x4 >= pictureBox4.Width)
            {
                x4 = 50;
                pic4.Clear(Color.DarkSlateGray);
                label20.BackColor = Color.DarkSlateGray;
            }



            pic5.DrawLine(pen5, x5, ((p1.Height - cs5 - 7) - p1.Height / 3) / ScaleY, x5 + 1, ((p1.Height - temp - 7) - p1.Height / 3) / ScaleY);
            cs5 = temp;
            pic5.Save();
            p1.Image = bm5;
            x5++;

            if (x5 >= p1.Width)
            {
                x5 = 50;
                pic5.Clear(Color.DarkSlateGray);
                l1.BackColor = Color.DarkSlateGray;
            }

            pic6.DrawLine(pen6, x6, (p2.Height - cs6 - 7) / ScaleY, x6 + 1, (p2.Height - humi - 7) / ScaleY);
            cs6 = humi;
            pic6.Save();
            p2.Image = bm6;
            x6++;

            if (x6 >= p2.Width)
            {
                x6 = 50;
                pic6.Clear(Color.DarkSlateGray);
                l2.BackColor = Color.DarkSlateGray;
            }



            pic7.DrawLine(pen7, x7, (p3.Height - cs7 - 7) / ScaleY, x7 + 1, (p3.Height - windspeed - 7) / ScaleY);
            cs7 = windspeed;
            pic7.Save();
            p3.Image = bm7;
            x7++;

            if (x7 >= p3.Width)
            {
                x7 = 50;
                pic7.Clear(Color.DarkSlateGray);
                l3.BackColor = Color.DarkSlateGray;
            }
        }


        float temp = 0;
        float DYY = 0;
        float DLL = 0;
        float PLL = 0;

        string lights, humis, plls, dlls, dyys, temps, windspeeds;

        //存储数据，以备日后写入文件
        private void timer4_Tick(object sender, EventArgs e)
        {
            lights += light.ToString() + ",";
            humis += humi.ToString() + ",";
            plls += PLL.ToString() + ",";
            dlls += DLL.ToString() + ",";
            dyys += DYY.ToString() + ",";
            temps += temp.ToString() + ",";
            windspeeds += windspeed + ",";
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Graphic();
        }


        private void 功能设置_Enter(object sender, EventArgs e)
        {

        }

        //十六进制字符串转字节数组
        private static byte[] HexStrTobyte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2).Trim(), 16);
            return returnBytes;
        }
        int k = 0;

        // 发送信息
        string[] str = { "F0 A0 01 00 000000 0F", "F0 A0 05 00 000000 0F", "F0 A0 0A 00 000000 0F", "F0 A0 0B 00 000000 0F" };
        private void timer3_Tick(object sender, EventArgs e)
        {
            try
            {
                if (k == str.Length)
                {
                    k = 0;
                }
                bytes = HexStrTobyte(str[k]);
                int s = clientSocket.Send(bytes, 0, 8, SocketFlags.None);
                k++;
            }
            catch (Exception ex)
            {
                timer3.Stop();
                timer2.Stop();
                MessageBox.Show(ex.Message);
            }
        }
        int flag_start = 0;
        private void button6_Click(object sender, EventArgs e)
        {
            if (flag_start == 1)
            {
                flag_start = 0;

                button6.Text = "开始存储";
                path = comboBox2.Text + "数据信息.txt";
                writeText(path);
                timer4.Stop();
            }
            else if (flag_start == 0)
            {
                timer4.Start();
                button6.Text = "结束存储";
                MessageBox.Show("正在存储");
                flag_start = 1;
            }

        }

        //扫描指定IP段的设备
        public void ScanIp(object obj)
        {
            string[] strIP = obj.ToString().Split(' ');
            string[] startIP = strIP[0].Split('.');
            string[] endIP = strIP[1].Split('.');
            string ss = startIP[0] + "." + startIP[1] + "." + startIP[2] + ".";
            for (int i = Convert.ToInt32(startIP[3]); i < Convert.ToInt32(endIP[3]); i++)
            {
                try
                {
                    GetName(ss + i.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            //for (int i = 100; i < 200; i++)
            //{
            //    GetName("10.1.24." + i.ToString());
            //}
        }

        //获取设备名
        public void GetName(string IPStr)
        {
            byte[] bs = new byte[50] { 0x0, 0x00, 0x0, 0x10, 0x0, 0x1, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x20, 0x43, 0x4b, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x0, 0x0, 0x21, 0x0, 0x1 };
            byte[] Buf = new byte[500];
            byte[,] recv = new byte[18, 28];

            int receive;
            string[] domainuser = new string[2];
            domainuser[0] = "";
            domainuser[1] = "";
            try
            {
                IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
                EndPoint Remote = (EndPoint)sender;
                IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(IPStr), 137);
                Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 50);
                server.SendTo(bs, bs.Length, SocketFlags.None, ipep);
                receive = server.ReceiveFrom(Buf, ref Remote);
                server.Close();
                if (receive > 0)
                {
                    recv = new byte[18, (receive - 56) % 18];
                    for (int k = 0; k < (receive - 56) % 18; k++)
                    {
                        for (int j = 0; j < 18; j++)
                        {
                            recv[j, k] = Buf[57 + 18 * k + j];
                        }
                    }
                    for (int k = 0; k < (receive - 56) % 18; k++)
                    {
                        str1 = "";
                        if (System.Convert.ToString(recv[15, k], 16) == "0" && (System.Convert.ToString(recv[16, k], 16) == "4" || System.Convert.ToString(recv[16, k], 16) == "44"))
                        {
                            for (int j = 0; j < 15; j++)
                            {
                                str1 += System.Convert.ToChar(recv[j, k]).ToString();
                            }
                            strHost = str1.Trim(); ;
                            Invoke(new AddIPHostDelegate(AddIPHost), new object[] { strHost }); ;
                            ipList.Add(strHost + " " + IPStr);
                        }
                    }
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine("未获取或获取失败");//ex.Message);
            }
        }

        delegate void AddIPHostDelegate(string ss);

        private void label17_Click(object sender, EventArgs e)
        {

        }

        //将扫描的设备名添加显示
        public void AddIPHost(string ss)
        {
            comboBox2.Items.Add(ss);
            comboBox2.SelectedIndex = 0;
            string host = comboBox2.Text;
            foreach (string s in ipList)
            {
                string[] comMess = s.Split(' ');
                if (comMess[0] == host)
                {
                    textBox1.Text = comMess[1];
                }
            }
        }
        int ask = 0;

        void c_DoubleClick_pen(object sender, EventArgs e)
        {
            if (ONandOFF == 0)
            {
                if (new Selectform().ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    PictureBox box = (PictureBox)sender;
                    if (box.Name.ToString().Equals(pictureBox5.Name.ToString()))
                    {
                        if (Selectform.lineColor != Color.Empty)
                        {
                            pen1.Color = Selectform.lineColor;
                        }
                        //pictureBox5.BackColor = Selectform.backColor;
                        if (Selectform.backColor != Color.Empty)
                        {
                            pic1.Clear(Selectform.backColor);
                            box.BackColor = Selectform.backColor;
                            label17.BackColor = Selectform.backColor;
                        }
                        if (Selectform.labelColor != Color.Empty)
                        {
                            label17.ForeColor = Selectform.labelColor;

                        }
                        if (Selectform.labelFont != null)
                        {
                            label17.Font = Selectform.labelFont;

                        }

                        ask = 1;
                    }
                    if (box.Name.ToString().Equals(pictureBox2.Name.ToString()))
                    {
                        if (Selectform.lineColor != Color.Empty)
                        {
                            pen2.Color = Selectform.lineColor;
                        }
                        if (Selectform.backColor != Color.Empty)
                        {
                            box.BackColor = Selectform.backColor;
                            pic2.Clear(Selectform.backColor);
                            label18.BackColor = Selectform.backColor;
                        }
                        if (Selectform.labelColor != Color.Empty)
                        {
                            label18.ForeColor = Selectform.labelColor;

                        }
                        if (Selectform.labelFont != null)
                        {
                            label18.Font = Selectform.labelFont;

                        }
                        ask = 1;
                    }
                    if (box.Name.ToString().Equals(pictureBox1.Name.ToString()))
                    {
                        if (Selectform.lineColor != Color.Empty)
                        {
                            pen3.Color = Selectform.lineColor;

                        }
                        if (Selectform.backColor != Color.Empty)
                        {
                            box.BackColor = Selectform.backColor;
                            pic3.Clear(Selectform.backColor);
                            label19.BackColor = Selectform.backColor;
                        }
                        if (Selectform.labelColor != Color.Empty)
                        {
                            label19.ForeColor = Selectform.labelColor;

                        }
                        if (Selectform.labelFont != null)
                        {
                            label19.Font = Selectform.labelFont;

                        }
                        ask = 1;
                    }
                    if (box.Name.ToString().Equals(pictureBox4.Name.ToString()))
                    {
                        if (Selectform.lineColor != Color.Empty)
                        {
                            pen5.Color = Selectform.lineColor;
                        }
                        if (Selectform.backColor != Color.Empty)
                        {
                            box.BackColor = Selectform.backColor;

                            pic4.Clear(Selectform.backColor);
                            label20.BackColor = Selectform.backColor;
                        }
                        if (Selectform.labelColor != Color.Empty)
                        {
                            label20.ForeColor = Selectform.labelColor;

                        }
                        if (Selectform.labelFont != null)
                        {
                            label20.Font = Selectform.labelFont;

                        }
                        ask = 1;
                    }
                    if (box.Name.ToString().Equals(p1.Name.ToString()))
                    {
                        if (Selectform.lineColor != Color.Empty)
                        {
                            pen5.Color = Selectform.lineColor;
                        }
                        if (Selectform.backColor != Color.Empty)
                        {
                            box.BackColor = Selectform.backColor;

                            pic5.Clear(Selectform.backColor);
                            l1.BackColor = Selectform.backColor;
                        }
                        if (Selectform.labelColor != Color.Empty)
                        {
                            l1.ForeColor = Selectform.labelColor;

                        }
                        if (Selectform.labelFont != null)
                        {
                            l1.Font = Selectform.labelFont;

                        }
                        ask = 1;
                    }
                    if (box.Name.ToString().Equals(p2.Name.ToString()))
                    {
                        if (Selectform.lineColor != Color.Empty)
                        {
                            pen6.Color = Selectform.lineColor;
                        }
                        if (Selectform.backColor != Color.Empty)
                        {
                            box.BackColor = Selectform.backColor;

                            pic6.Clear(Selectform.backColor);
                            l2.BackColor = Selectform.backColor;
                        }
                        if (Selectform.labelColor != Color.Empty)
                        {
                            l2.ForeColor = Selectform.labelColor;

                        }
                        if (Selectform.labelFont != null)
                        {
                            l2.Font = Selectform.labelFont;

                        }
                        ask = 1;
                    }
                    if (box.Name.ToString().Equals(p3.Name.ToString()))
                    {
                        if (Selectform.lineColor != Color.Empty)
                        {
                            pen7.Color = Selectform.lineColor;

                        }
                        if (Selectform.backColor != Color.Empty)
                        {
                            box.BackColor = Selectform.backColor;
                            pic7.Clear(Selectform.backColor);
                            l3.BackColor = Selectform.backColor;
                        }
                        if (Selectform.labelColor != Color.Empty)
                        {
                            l3.ForeColor = Selectform.labelColor;

                        }
                        if (Selectform.labelFont != null)
                        {
                            l3.Font = Selectform.labelFont;

                        }
                        ask = 1;
                    }
                }
            }
            else
            {
                MessageBox.Show("请先关闭设备");
                return;
            }
        }

        int i = 0;
        void c_DoubleClick(object sender, EventArgs e)
        {
            //if (new Selectform().ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{ 
            //    foreach (Control c in tabPage1.Controls)
            //    {
            //        if (c is Label)
            //        {
            //            c.Font = Selectform.labelFont;
            //            c.ForeColor = Selectform.labelColor;
            //        }
            //    }
            //}
            //i++;
            //if (i == 2)
            //{
            //    i = 0;

            //    try
            //    {
            //        if (fontDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //        {
            //            //MessageBox.Show(fontDialog1.Font.Name.ToString());
            //            foreach (Control c in tabPage1.Controls)
            //            {
            //                if (c is Label)
            //                {
            //                    c.Font = fontDialog1.Font;
            //                }
            //            }
            //        }
            //    }
            //    catch (Exception)
            //    {
            //        MessageBox.Show("请重新选择-中文字体类型！");
            //    }

            //}
            //if (i == 1)
            //{
            //    if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //    {
            //        foreach (Control c in tabPage1.Controls)
            //        {
            //            if (c is Label)
            //            {
            //                c.ForeColor = colorDialog1.Color;
            //                //if (sender.Equals(c))
            //                //{
            //                //Label l = null;
            //                //l = (Label)c;
            //                //l.Font = fontDialog1.Font;
            //                //}
            //            }
            //        }
            //    }
            //}
        }

        //private void pictureBox2_DoubleClick(object sender, EventArgs e)
        //{
        //    if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    {
        //        pen1.Color = colorDialog1.Color;
        //    }
        //}

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string host = comboBox2.Text;
            foreach (string s in ipList)
            {
                string[] comMess = s.Split(' ');
                if (comMess[0] == host)
                {
                    textBox1.Text = comMess[1];
                }
            }
        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show(this.Location.ToString());
        }


        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            tabPage1.AutoScrollPosition = new Point(tabControl1.Location.X + tabControl1.Width, tabControl1.Location.Y - 25);

            //MessageBox.Show("BDDD");
            pic1.Clear(pictureBox5.BackColor);
            pic4.Clear(pictureBox4.BackColor);
            pic2.Clear(pictureBox2.BackColor);
            pic3.Clear(pictureBox1.BackColor);
            pic5.Clear(p1.BackColor);
            pic6.Clear(p2.BackColor);
            pic7.Clear(p3.BackColor);
            x1 = 50;
            x2 = 50;
            x3 = 50;
            x4 = 50;
            x5 = 50;
            x6 = 50;
            x7 = 50;

            picheight5 = pictureBox5.Height;
            picwidth5 = pictureBox5.Width;

            picwidth4 = pictureBox4.Width;
            picheight4 = pictureBox4.Height;

            picwidth1 = pictureBox1.Width;
            picheight1 = pictureBox1.Height;

            picwidth2 = pictureBox2.Width;
            picheight2 = pictureBox2.Height;



            Form f = (Form)sender;

            ScaleX = (float)f.Width / oldFrmWidth;
            ScaleY = (float)f.Height / oldFrmHeight;

            foreach (Control c in f.Controls)
            {
                tmp = c.Tag.ToString().Split(',');
                c.Left = (int)(Convert.ToInt16(tmp[0]) * ScaleX);
                c.Top = (int)(Convert.ToInt16(tmp[1]) * ScaleY);
                c.Width = (int)(Convert.ToInt16(tmp[2]) * ScaleX);
                c.Height = (int)(Convert.ToInt16(tmp[3]) * ScaleY);

            }
            foreach (TabPage page in tabControl1.TabPages)
            {
                foreach (Control controls1 in page.Controls)
                {
                    tmp1 = controls1.Tag.ToString().Split(',');
                    controls1.Left = (int)(Convert.ToInt16(tmp1[0]) * ScaleX);
                    controls1.Top = (int)(Convert.ToInt16(tmp1[1]) * ScaleY);
                    controls1.Width = (int)(Convert.ToInt16(tmp1[2]) * ScaleX);
                    controls1.Height = (int)(Convert.ToInt16(tmp1[3]) * ScaleY);

                }

            }
            foreach (Control goup1 in groupBox1.Controls)
            {
                string[] tmp2 = goup1.Tag.ToString().Split(',');
                goup1.Left = (int)(Convert.ToInt16(tmp2[0]) * ScaleX);
                goup1.Top = (int)(Convert.ToInt16(tmp2[1]) * ScaleY);
                goup1.Width = (int)(Convert.ToInt16(tmp2[2]) * ScaleX);
                goup1.Height = (int)(Convert.ToInt16(tmp2[3]) * ScaleY);
            }
            foreach (Control goup2 in groupBox2.Controls)
            {
                string[] tmp3 = goup2.Tag.ToString().Split(',');
                goup2.Left = (int)(Convert.ToInt16(tmp3[0]) * ScaleX);
                goup2.Top = (int)(Convert.ToInt16(tmp3[1]) * ScaleY);
                goup2.Width = (int)(Convert.ToInt16(tmp3[2]) * ScaleX);
                goup2.Height = (int)(Convert.ToInt16(tmp3[3]) * ScaleY);
            }
            foreach (Control goup3 in groupBox3.Controls)
            {
                string[] tmp4 = goup3.Tag.ToString().Split(',');
                goup3.Left = (int)(Convert.ToInt16(tmp4[0]) * ScaleX);
                goup3.Top = (int)(Convert.ToInt16(tmp4[1]) * ScaleY);
                goup3.Width = (int)(Convert.ToInt16(tmp4[2]) * ScaleX);
                goup3.Height = (int)(Convert.ToInt16(tmp4[3]) * ScaleY);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void 添加数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //MessageBox.Show("添加成功");
        }

        int mode = 0;
        private void 通讯方式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectMode cm = new ConnectMode();
            cm.Show();
            cm.Visible = false;
            if (cm.ShowDialog() == DialogResult.OK)
            {
                if (cm.ipandPort == null)
                {
                    mode = 0;
                    serialPort1 = cm.ser;
                    serialPort1.Open();
                    netConnect.Visible = false;
                    portConnect.Visible = true;
                    label1.Text = "连接方式：" + ConnectMode.connectMode;
                    tbPortName.Text = cm.ser.PortName.ToString();
                    tbBaudRate.Text = cm.ser.BaudRate.ToString();
                    MessageBox.Show("打开成功");
                    button5.Text = "断开连接";

                    flag = 1;
                }
                else if (cm.ser == null)
                {
                    mode = 1;
                    IpEndPort = cm.ipandPort;
                    clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    threadConnect = new Thread(new ThreadStart(Connect));
                    threadConnect.Start();
                    label1.Text = "连接方式：" + ConnectMode.connectMode;
                    netConnect.Visible = true;
                    portConnect.Visible = false;

                    textBox1.Text = cm.ipandPort.Address.ToString();
                    //tbPort.Text = cm.ipandPort.Port.ToString();
                    comboBox1.Text = cm.ipandPort.Port.ToString();
                    label11.Text = "网络连接状态：连接成功";
                }
            }

        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Version f1 = new Version();
            f1.Show();


        }

        private void 添加设备ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddDevice addfrom = new AddDevice();
            addfrom.Show();
            addfrom.Visible = false;
            if (addfrom.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string port = addfrom.DevicePort;
                ipList.Add("网络设备：" + addfrom.DeviceName + " " + addfrom.DeviceIP.Replace(" ", ""));
                comboBox2.Items.Add("网络设备：" + addfrom.DeviceName);
                comboBox2.SelectedIndex = 0;
                textBox1.Text = addfrom.DeviceIP;
                comboBox1.Items.Add(addfrom.DevicePort);
            }
        }

        private void 搜索设备ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 检查更新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("已是最新版本 v_1.0", "更新", MessageBoxButtons.OK);
        }

        int save = 0;
        private void 存储模式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (save == 0)
            {
                button6.Visible = true;
                查看历史记录ToolStripMenuItem.Visible = true;
                save = 1;

            }
            else if (save == 1)
            {
                button6.Visible = false;
                查看历史记录ToolStripMenuItem.Visible = false;
                save = 0;
            }
        }
        string path;
        private void button6_VisibleChanged(object sender, EventArgs e)
        {
            if (!button6.Visible)
            {
            }
            //string path7 = Environment.CurrentDirectory + "\\" + comboBox2.Text;
            //if (!Directory.Exists(path7))
            //{
            //    Directory.CreateDirectory(path7);
            //}
            //path = path7 + "\\" + "设备的连接信息.txt";
            //writeText(path);
        }

        void writeText(string path)
        {
            //向文件中写数据
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            sw.WriteLine("光照：" + lights);
            sw.WriteLine("电流：" + dlls);
            sw.WriteLine("电压：" + dyys);
            sw.WriteLine("频率：" + plls);
            sw.WriteLine("温度：" + temps);
            sw.WriteLine("湿度：" + humis);
            sw.WriteLine("风速：" + windspeeds);
            MessageBox.Show("写入成功");
            sw.Flush();
            sw.Close();
            lights = "";
            dlls = "";
            dyys = "";
            plls = "";
            temps = "";
            humis = "";
            windspeeds = "";
        }

        private void 查看历史记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HistoryFrom hf = new HistoryFrom();
            hf.Show();
        }
        public static string font_name_label;
        public static string font_name_menu;
        private void 选项ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //font_name_label = label17.Font.Name;
            //font_name_menu = menuStrip1.Font.Name;

            //select sl = new select();
            //sl.Show();
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Open open = new Open();
            open.Show();
        }

        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 另存文件对话框
            try
            {
                SaveFileDialog sv = new SaveFileDialog();
                sv.Filter = "文本文件|*.txt";
                if (sv.ShowDialog() == DialogResult.OK)
                {
                    File.Copy(path, sv.FileName, true);
                    path = sv.FileName;
                    //writeText(sv.FileName);
                    //StreamReader sr = new StreamReader(path);
                    //StreamWriter sw = new StreamWriter(sv.FileName, true);
                    //string line = null;
                    //while ((line = sr.ReadLine()) != null)
                    //{
                    //    sw.WriteLine(line);
                    //    sw.Flush();
                    //}
                }
            }
            catch (ArgumentNullException ex)
            {
                if (flag_start == 0)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                else
                {
                    MessageBox.Show("请先开启存储模式");
                    return;
                }
            }
        }

        private void 网络设备ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            SetIP setFrom = new SetIP();
            setFrom.Show();
            setFrom.Visible = false;
            if (setFrom.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string startIP = setFrom.startIP;
                string endIP = setFrom.endIP;
                Thread ScanIpThread = new Thread(new ParameterizedThreadStart(ScanIp));
                ScanIpThread.Start(startIP + " " + endIP);

            }
            portConnect.Visible = false;
            netConnect.Visible = true;

        }

        private void 串口设备ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            string[] portNames = SerialPort.GetPortNames();
            foreach (string s in portNames)
            {
                comboBox2.Items.Add(s);
            }
            netConnect.Visible = false;
            portConnect.Visible = true;

            comboBox2.SelectedIndex = 0;
        }

        private void 菜单文本ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 数据文本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void 曲线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pen1.Color = colorDialog1.Color;
                pen2.Color = colorDialog1.Color;
                pen3.Color = colorDialog1.Color;
                pen4.Color = colorDialog1.Color;
                pen5.Color = colorDialog1.Color;
                pen6.Color = colorDialog1.Color;
                pen7.Color = colorDialog1.Color;
            }
        }

        private void 字号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (ToolStripMenuItem ts in menuStrip1.Items)
                {
                    ts.Font = fontDialog1.Font;
                }
            }
        }

        private void sevaiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (ToolStripMenuItem ts in menuStrip1.Items)
                {
                    ts.ForeColor = colorDialog1.Color;
                }
            }
        }

        private void 字号ToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            if (fontDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                label17.Font = fontDialog1.Font;
                label18.Font = fontDialog1.Font;
                label19.Font = fontDialog1.Font;
                label20.Font = fontDialog1.Font;
                l1.Font = fontDialog1.Font;
                l2.Font = fontDialog1.Font;
                l3.Font = fontDialog1.Font;
            }
        }

        private void 色彩ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                label17.ForeColor = colorDialog1.Color;
                label18.ForeColor = colorDialog1.Color;
                label19.ForeColor = colorDialog1.Color;
                label20.ForeColor = colorDialog1.Color;
                l1.ForeColor = colorDialog1.Color;
                l2.ForeColor = colorDialog1.Color;
                l3.ForeColor = colorDialog1.Color;
            }
        }


        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 帮助ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Help h = new Help();
            h.Show();
        }

        private void 菜单文本ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

        }

        private void 参数文本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void 曲线背景ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pictureBox5.BackColor = colorDialog1.Color;
                pictureBox4.BackColor = colorDialog1.Color;
                pictureBox2.BackColor = colorDialog1.Color;
                pictureBox1.BackColor = colorDialog1.Color;
                p1.BackColor = colorDialog1.Color;
                p2.BackColor = colorDialog1.Color;
                p3.BackColor = colorDialog1.Color;
                label17.BackColor = colorDialog1.Color;
                label18.BackColor = colorDialog1.Color;
                label19.BackColor = colorDialog1.Color;
                label20.BackColor = colorDialog1.Color;
                l1.BackColor = colorDialog1.Color;
                l2.BackColor = colorDialog1.Color;
                l3.BackColor = colorDialog1.Color;
            }
        }

        private void 字体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                menuStrip1.Font = fontDialog1.Font;
            }
        }

        private void 颜色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (ToolStripMenuItem tsmi in menuStrip1.Items)
                {
                    tsmi.ForeColor = colorDialog1.Color;
                }
            }
        }


        private void 字体ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                label17.Font = fontDialog1.Font;
                label18.Font = fontDialog1.Font;
                label19.Font = fontDialog1.Font;
                label20.Font = fontDialog1.Font;
                l1.Font = fontDialog1.Font;
                l2.Font = fontDialog1.Font;
                l3.Font = fontDialog1.Font;
            }
        }

        private void 颜色ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                label17.ForeColor = colorDialog1.Color;
                label18.ForeColor = colorDialog1.Color;
                label19.ForeColor = colorDialog1.Color;
                label20.ForeColor = colorDialog1.Color;
                l1.ForeColor = colorDialog1.Color;
                l2.ForeColor = colorDialog1.Color;
                l3.ForeColor = colorDialog1.Color;
            }
        }

    }
}
