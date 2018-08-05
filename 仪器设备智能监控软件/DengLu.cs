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
    public partial class DengLu : Form
    {
        public DengLu()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool isTrue = false;
            string username = this.textBox1.Text;
            string password = this.textBox2.Text;
            FileStream fs = new FileStream("用户信息.txt", FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] s = line.Split('=');
                if (s[0].ToString() == username && s[1].ToString() == password)
                {
                    isTrue = true;
                    MessageBox.Show("登陆成功");
                    using (MainForm form = new MainForm())
                    {
                        this.Hide();
                        form.ShowDialog();
                    }
                }

            }
            if (!isTrue)
            {
                MessageBox.Show("登录失败,用户名或密码错误", "ERROR");
            }
            fs.Close();
            sr.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (ZhuCe f = new ZhuCe())
            {
                this.Hide();
                f.ShowDialog();

            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            this.textBox1.Clear();
            this.textBox1.ForeColor = Color.Black;
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            this.textBox2.Clear();
            this.textBox2.ForeColor = Color.Black;
            this.textBox2.PasswordChar = Convert.ToChar("*");
        }

    }
}
