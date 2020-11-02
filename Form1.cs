using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyPaint
{
    public partial class Form1 : Form
    {
        Bitmap pic;
        int x1, y1;
        public Form1()
        {
            InitializeComponent();
            pic = new Bitmap(1000, 1000);
            x1 = y1 = 0;
            colorDialog1.FullOpen = true;
            colorDialog1.Color = this.button1.BackColor;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            this.button1.BackColor = colorDialog1.Color;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Pen pencil;
            pencil = new Pen(button1.BackColor, trackBar1.Value);
            pencil.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            pencil.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            Graphics g;
            g = Graphics.FromImage(pic);
            if (e.Button == MouseButtons.Left)
            {
                g.DrawLine(pencil, x1, y1, e.X, e.Y);
                pictureBox1.Image = pic;
            }
            x1 = e.X;
            y1 = e.Y;
        }
    }
}
