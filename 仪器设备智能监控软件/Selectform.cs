using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Text.RegularExpressions;

namespace 仪器设备智能监控软件V1._0
{
    public partial class Selectform : Form
    {
        public Selectform()
        {
            InitializeComponent();
        }
        Graphics pic;
        Pen pen = new Pen(Color.Red, 2);
        private void select_Load(object sender, EventArgs e)
        {

            pic = pictureBox1.CreateGraphics();
            //pictureBox1.BackColor = Color.DarkSlateGray;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        public static string Rgb216(int r, int g, int b)
        {
            return ColorTranslator.ToHtml(Color.FromArgb(r, g, b));
        }
        public static int font_index = 0;
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void select_Paint(object sender, PaintEventArgs e)
        {
            pic.DrawLine(pen, 50, 50, 350, 50);
        }
        public static Color backColor;
        public static Color lineColor;
        public static Font labelFont;
        public static Color labelColor;


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pictureBox1.BackColor = colorDialog1.Color;
                backColor = colorDialog1.Color;
                label1.BackColor = colorDialog1.Color;
            }
            this.Refresh();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pen.Color = colorDialog1.Color;
                lineColor = colorDialog1.Color;
            }
            this.Refresh();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (fontDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                label1.Font = fontDialog1.Font;
                labelFont = fontDialog1.Font;
            }
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                label1.ForeColor = colorDialog1.Color;
                labelColor = colorDialog1.Color;
            }
        }
    }
}
