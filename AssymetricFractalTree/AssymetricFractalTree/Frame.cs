using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AssymetricFractalTree
{
    public partial class Frame : Form
    {
        private bool click;

        public Frame()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.Text = "Asymmetric Fractal tree";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.CenterToScreen();
            pictureBox.BorderStyle = BorderStyle.Fixed3D;
        }

        private void paintAsymmetricFT(Pen pen, Graphics g, float xo, float yo, double angle, double rightAngleVar, double leftAngleVar, int depth, float length)
        {
            if (depth > -1)
            {
                float xf = xo - (cos(degreeToRadian(angle)) * length * depth);
                float yf = yo - (sin(degreeToRadian(angle)) * length * depth);
                g.DrawLine(pen, xo, yo, xf, yf);
                paintAsymmetricFT(pen, g, xf, yf, angle + rightAngleVar, rightAngleVar, leftAngleVar, depth - 1, length);
                paintAsymmetricFT(pen, g, xf, yf, angle - leftAngleVar, rightAngleVar, leftAngleVar, depth - 1, length);
            }
        }

        private float cos(double angle)
        {
            return (float)Math.Cos(angle);
        }

        private float sin(double angle)
        {
            return (float)Math.Sin(angle);
        }

        private double degreeToRadian(double angle)
        {
            return (Math.PI * angle) / 180;
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (click)
            {
                float xo = (pictureBox.Width / 2) - 4;
                float yo = pictureBox.Height - Convert.ToSingle(5.4);
                double rightAngleVar = Convert.ToDouble(nud1.Value);
                double leftAngleVar = Convert.ToDouble(nud2.Value);
                int depth = Convert.ToInt32(nud3.Value);
                float length = Convert.ToSingle(tb1.Text);
                Pen pen = new Pen(Color.Black);
                paintAsymmetricFT(pen, e.Graphics, xo, yo, 90, rightAngleVar, leftAngleVar, depth, length);
                click = false;
                pen.Dispose();
            }
        }

        private void drawbt_Click(object sender, EventArgs e)
        {
            click = true;
            pictureBox.Refresh();
        }

        private void tb1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar) && !e.KeyChar.Equals(','))
            {
                e.Handled = true;
            }
        }
    }
}
