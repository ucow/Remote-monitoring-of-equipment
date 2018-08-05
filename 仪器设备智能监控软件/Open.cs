using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace 仪器设备智能监控软件V1._0
{
    public partial class Open : Form
    {
        public Open()
        {
            InitializeComponent();
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.ScrollToCaret();
        }
        private void Open_Load(object sender, EventArgs e)
        {
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
            // 打开文件对话框
            OpenFileDialog o = new OpenFileDialog();//定义新的文件打开位置控件
            //o.ShowDialog();
            o.Filter = "文本文件|*.txt";//设置文件后缀的过滤
            if (o.ShowDialog() == DialogResult.OK)//如果有选择打开的文件夹
            {
                string str = "";
                StreamReader sr = File.OpenText(o.FileName);
                this.Text = o.FileName;
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    str += line + "\n";
                }
                richTextBox1.Text = str;
            }
        }


    }
}