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
    public partial class HistoryFrom : Form
    {
        public HistoryFrom()
        {
            InitializeComponent();
        }
        string sPath;
        private void HistoryFrom_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(sPath))
            {
                Directory.CreateDirectory(sPath);
            }
            StreamReader sr = new StreamReader("数据信息.txt");
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                textBox1.AppendText(line + "\n");
            }
        }
    }
}
