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
    public partial class SetIP : Form
    {
        public SetIP()
        {
            InitializeComponent();
        }




        public string startIP;
        public string endIP;
        private void button1_Click(object sender, EventArgs e)
        {
            startIP = mTBIP.Text.Replace(" ", "");
            endIP = maskedTextBox1.Text.Replace(" ", "");
            string[] start = startIP.Split('.');
            string[] end = endIP.Split('.');
            if (Convert.ToInt64(start[3]) < Convert.ToInt64(end[3]))
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                MessageBox.Show("不符合规范");
                return;
            }
        }

        private void maskedTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Decimal)
            {
                int pos = maskedTextBox1.SelectionStart;
                int max = (maskedTextBox1.MaskedTextProvider.Length - maskedTextBox1.MaskedTextProvider.EditPositionCount);
                int nextfield = 0;
                for (int i = 0; i < maskedTextBox1.MaskedTextProvider.Length; i++)
                {
                    if (!maskedTextBox1.MaskedTextProvider.IsEditPosition(i) && (pos + max) >= i)
                    {
                        nextfield = i;
                    }
                }
                nextfield += 1;
                maskedTextBox1.SelectionStart = nextfield;
            }
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
    }
}
