using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 仪器设备智能监控软件V1._0
{
    public partial class AddDevice : Form
    {
        public AddDevice()
        {
            InitializeComponent();
        }
        public string DeviceName;
        public string DeviceIP;
        public string DevicePort;
        private void button1_Click(object sender, EventArgs e)
        {
            DeviceName = textBox2.Text;
            DevicePort = textBox1.Text;
            DeviceIP = mTBIP.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void maskedTextBox1_KeyDown(object sender, KeyEventArgs e)
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

        private void AddDevice_Load(object sender, EventArgs e)
        {
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
        }

        private void AddDevice_Load_1(object sender, EventArgs e)
        {

        }

    
    }
}
