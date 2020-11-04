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
        Bitmap t_pic;
        string mode;
        int x1, y1;
        int xclick1, yclick1;
        int c_c;
        double width, height, scale;
        public Form1()
        {
            InitializeComponent();
            mode = "Карандаш";
            pic = new Bitmap(1366, 640);
            t_pic = new Bitmap(1366, 640);
            x1 = y1 = 0;
            c_c = 4;
            width = 1366;
            height = 640;
            scale = Math.Min(width / pic.Width, height / pic.Height);
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

        private void button2_Click(object sender, EventArgs e)
        {
            mode = "Прямоугольник";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mode = "Эллипс";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mode = "Карандаш";
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (mode == "Прямоугольник")
            {
                Graphics g;
                g = Graphics.FromImage(pic);
                Pen pencil;
                pencil = new Pen(button1.BackColor, trackBar1.Value);
                int caseSwitch = -1;
                if ((xclick1 > e.X) && (yclick1 > e.Y)) caseSwitch = 0;
                if ((xclick1 > e.X) && (yclick1 < e.Y)) caseSwitch = 1;
                if ((xclick1 < e.X) && (yclick1 > e.Y)) caseSwitch = 2;
                switch (caseSwitch) {
                    case 0:
                        g.DrawRectangle(pencil, e.X, e.Y, xclick1 - e.X, yclick1 - e.Y);
                        break;
                    case 1:
                        g.DrawRectangle(pencil, e.X, yclick1, xclick1 - e.X, e.Y - yclick1);
                        break;
                    case 2:
                        g.DrawRectangle(pencil, xclick1, e.Y, e.X - xclick1, yclick1 - e.Y);
                        break;
                    default:
                        g.DrawRectangle(pencil, xclick1, yclick1, e.X - xclick1, e.Y - yclick1);
                        break;
                }

            }
            if (mode == "Эллипс")
            {
                Graphics g;
                g = Graphics.FromImage(pic);
                Pen pencil;
                pencil = new Pen(button1.BackColor, trackBar1.Value);
                g.DrawEllipse(pencil, xclick1, yclick1, e.X - xclick1, e.Y - yclick1);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            mode = "Рука";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            mode = "Масштабирование";
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Версия программы 1.00\nАвтор: Луцик Александр Николаевич", "О программе");
        }

        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (mode == "Масштабирование")
            {
                var brush = new SolidBrush(Color.White);
                var s_pic = new Bitmap((int)width, (int)height);
                var s_g = Graphics.FromImage(s_pic);
                if ((e.Button == MouseButtons.Right) && (c_c > 0))
                {
                    scale /= 2;
                    c_c -= 1;
                }
                if ((e.Button == MouseButtons.Left) && (c_c < 8))
                {
                    scale *= 2;
                    c_c += 1;
                }
                if ((c_c >= 0) && (c_c <= 8))
                {
                    var scaleWidth = (int)(pic.Width * scale);
                    var scaleHeight = (int)(pic.Height * scale);
                    s_g.FillRectangle(brush, new RectangleF(0, 0, (float)(width), (float)(height)));
                    s_g.DrawImage(pic, ((int)width - scaleWidth) / 2, ((int)height - scaleHeight) / 2, scaleWidth, scaleHeight);
                    s_g.DrawImage(pic, ((int)width - scaleWidth) / 2, ((int)height - scaleHeight) / 2, scaleWidth, scaleHeight);
                    pictureBox1.Image = s_pic;
                }
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            xclick1 = e.X;
            yclick1 = e.Y;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mode != "Масштабирование")
            {
                Pen pencil;
                pencil = new Pen(button1.BackColor, trackBar1.Value);
                pencil.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                pencil.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                Graphics g;
                g = Graphics.FromImage(pic);
                Graphics t_g;
                t_g = Graphics.FromImage(t_pic);
                if (e.Button == MouseButtons.Left)
                {

                    if (mode == "Карандаш")
                    {
                        g.DrawLine(pencil, x1, y1, e.X, e.Y);
                        pictureBox1.Image = pic;
                    }
                    if (mode == "Прямоугольник")
                    {
                        t_g.Clear(Color.White);
                        int x = xclick1, y = yclick1;
                        if (x > e.X) x = e.X;
                        if (y > e.Y) y = e.Y;
                        t_g.DrawRectangle(pencil, x, y, Math.Abs(e.X - xclick1), Math.Abs(e.Y - yclick1));
                        t_g.DrawImage(pic, 0, 0);
                        pictureBox1.Image = t_pic;
                    }
                    if (mode == "Эллипс")
                    {
                        t_g.Clear(Color.White);
                        t_g.DrawEllipse(pencil, xclick1, yclick1, e.X - xclick1, e.Y - yclick1);
                        t_g.DrawImage(pic, 0, 0);
                        pictureBox1.Image = t_pic;
                    }
                }
                x1 = e.X;
                y1 = e.Y;
            }
        }
    }
}
