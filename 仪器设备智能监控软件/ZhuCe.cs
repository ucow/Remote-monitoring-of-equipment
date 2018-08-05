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
    public partial class ZhuCe : Form
    {
        public ZhuCe()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string uesrname = this.textBox1.Text;
            string password = this.textBox2.Text;

            FileStream fs2 = new FileStream("用户信息.txt", FileMode.OpenOrCreate);
            File.SetAttributes("用户信息.txt", FileAttributes.System | FileAttributes.Hidden);
            StreamReader sr = new StreamReader(fs2);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] s = line.Split('=');
                if (s[0] == uesrname)
                {
                    MessageBox.Show("此用户名已被注册");
                    return;
                }
            }
            fs2.Close();
            sr.Close();
            FileStream fs1 = new FileStream("用户信息.txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs1);
            if (uesrname != "" && password != "")
            {
                sw.WriteLine(uesrname + "=" + password);
                sw.Flush();
                sw.Close();
                fs1.Close();
                MessageBox.Show("注册成功");
                using (DengLu f = new DengLu())
                {
                    this.Hide();
                    f.ShowDialog();

                }
            }
            else
            {
                MessageBox.Show("用户名或密码不能为空");
                sw.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (DengLu f = new DengLu())
            {
                this.Hide();
                f.ShowDialog();

            }
        }
    }
}
