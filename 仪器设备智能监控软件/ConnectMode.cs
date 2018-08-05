using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace 仪器设备智能监控软件V1._0
{
    public partial class ConnectMode : Form
    {
        public ConnectMode()
        {
            InitializeComponent();
        }

        public IPEndPoint ipandPort;
        public static string connectMode;
        public SerialPort ser;

        private void ConnectMode_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 2 || comboBox1.SelectedIndex == 1)
            {
                //netConnect.Show();
                //portConnect.Hide();
                netConnect.Visible = true;
                portConnect.Visible = false;

            }
            if (comboBox1.SelectedIndex == 0)
            {
                //netConnect.Hide();
                //portConnect.Show()
                netConnect.Visible = false;
                portConnect.Visible = true; ;
                comboBox2.Items.Clear();
                string[] port = SerialPort.GetPortNames();
                for (int i = 0; i < port.Length; i++)
                {
                    comboBox2.Items.Add(port[i]);
                }
                comboBox3.SelectedIndex = 0;
            }
        }
        //public static int wl = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            IPAddress ip = IPAddress.Parse(mTBIP.Text.Replace(" ", ""));
            int port = Int32.Parse(tbPort.Text);
            IPEndPoint IpEndPort = new IPEndPoint(ip, port);
            if (IpEndPort != null)
            {
                ipandPort = IpEndPort;
                connectMode = comboBox1.Text;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            this.Close();
            //wl = 1;
        }

        private void mTBIP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Decimal)
            {
                int pos = mTBIP.SelectionStart;
                int max = (mTBIP.MaskedTextProvider.Length - mTBIP.MaskedTextProvider.EditPositionCount);
                int nextfield = 0;
                for (int i = 0; i < mTBIP.MaskedTextProvider.Length; i++)
                {
                    if (!mTBIP.MaskedTextProvider.IsEditPosition(i) && (pos + max) >= i)
                    {
                        nextfield = i;
                    }
                }
                nextfield += 1;
                mTBIP.SelectionStart = nextfield;
            }
        }

        //public  static int ck = 0;
        private void button7_Click(object sender, EventArgs e)
        {
            string[] portNames = SerialPort.GetPortNames();
            foreach (string s in portNames)
            {
                comboBox2.Items.Add(s);
            }
            if (comboBox2.Text != "" && comboBox3.Text != "")
            {
                ser = new SerialPort();
                ser.PortName = comboBox2.Text;
                ser.BaudRate = Convert.ToInt32(comboBox3.Text);
                if (ser != null)
                {
                    connectMode = comboBox1.Text;
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    //ck = 1;
                }
            }
        }

    }
}
