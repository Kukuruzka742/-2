using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace диплом
{
    public class CircleLetters 
    {
        private Form f;
        public Label[] letters;
        private int WidthElips, x, y;
        List<Label> CurrentLabels;

        public CircleLetters(Form f, Point point, int width)
        {
            this.f = f;
            f.Paint += new PaintEventHandler(Form_Paint);
            y = point.Y;
            x = point.X;
            WidthElips = width;
            CurrentLabels = new List<Label>();
        }

        private void Form_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Brushes.LightSkyBlue, 20);
            SolidBrush brush = new SolidBrush(Color.LightCyan);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.FillEllipse(brush, new Rectangle(x, y, WidthElips, WidthElips));
            g.DrawEllipse(pen, new Rectangle(x, y, WidthElips, WidthElips));
            CheckCurrents();
        }

        public Label[] CreateLetters(string letters)
        {
            Label[] labels = new Label[letters.Length];
            for (int i = 0; i < letters.Length; i++)
            {
                labels[i] = new Label()
                {
                    Text = Convert.ToString(letters.ToCharArray()[i]),
                    BackColor = Color.Transparent,
                    ForeColor = Color.LightSkyBlue
                };
                switch (letters.Length)
                {
                    case 4:
                        {
                            labels[i].Size = new Size(90, 90);
                            labels[i].Font = new Font("Arial Black", 60, FontStyle.Bold, GraphicsUnit.Pixel, 204);
                        }
                        break;
                    case 5:
                        {
                            labels[i].Size = new Size(60, 60);
                            labels[i].Font = new Font("Arial Black", 40, FontStyle.Bold, GraphicsUnit.Pixel, 204);
                        }
                        break;
                    case 6:
                        {
                            labels[i].Size = new Size(50, 50);
                            labels[i].Font = new Font("Arial Black", 35, FontStyle.Bold, GraphicsUnit.Pixel, 204);
                        }
                        break;
                }
            }
            for (int i = labels.Count() - 1; i >= 1; i--)
            {
                int j = new Random().Next(labels.Count());
                Label temp = labels[j];
                labels[j] = labels[i];
                labels[i] = temp;
            }
            return labels;
        }

        public void PlaceLabel(string l)
        {
            letters = CreateLetters(l);
            switch (letters.Count())
            {
                case 4:
                    {
                        Rectangle r = new Rectangle(x + Convert.ToInt32(WidthElips / 3.5) + 5, y + Convert.ToInt32(WidthElips / 3.5), Convert.ToInt32(WidthElips / 2.5), Convert.ToInt32(WidthElips / 2.5));
                        letters[0].Location = new Point(r.Left, r.Top);
                        letters[1].Location = new Point(r.Left, r.Bottom);
                        letters[2].Location = new Point(r.Right, r.Top);
                        letters[3].Location = new Point(r.Right, r.Bottom);
                    }
                    break;
                case 5:
                    {

                        Point[] p = PointPentagon(x + WidthElips / 2, y + WidthElips / 2, WidthElips / 2 - 50);
                        for (int i = 0; i < letters.Length; i++)
                            letters[i].Location = p[i];
                    }
                    break;
                case 6:
                    {
                        Point[] p = PointHexagon(x + WidthElips / 2 - 4, y + WidthElips / 2, WidthElips / 2 - 50);
                        for (int i = 0; i < letters.Length; i++)
                            letters[i].Location = p[i];
                    }
                    break;
            }
            for (int i = 0; i < letters.Length; i++)
            {
                letters[i].Left -= Convert.ToInt32(letters[i].Width / 2);
                letters[i].Top -= Convert.ToInt32(letters[i].Height / 2);
                /*letters[i].MouseMove += FormMouseMove;
                letters[i].MouseMove += label_MouseMove;
                letters[i].MouseDown += MouseDown;
                letters[i].MouseUp += MouseUp;*/
                f.Controls.Add(letters[i]);
                //f.MouseMove += FormMouseMove;
            }
        }

        private void MouseDown(object sender, MouseEventArgs e)
        {
            CurrentLabels.Add((Label)sender);
            CheckCurrents();
        }

        private void MouseUp(object sender, MouseEventArgs e)
        {
            CurrentLabels.Clear();
            CheckCurrents();
        }

        private void FormMouseMove(object sender, MouseEventArgs e)
        {
            CheckCurrents();
        }

        private void label_MouseMove(object sender, MouseEventArgs e)
        {
            if (!CurrentLabels.Contains((Label)sender))
            {
                if (CurrentLabels.Count() > 0)
                {
                    CurrentLabels.Add((Label)sender);

                }
                
            }
        }

        private void CheckCurrents()
        {
            for (int i = 0; i < letters.Length; i++)
            {
                if (CurrentLabels.Contains(letters[i]))
                {
                    letters[i].ForeColor = Color.Blue;
                }
                else
                {
                    letters[i].ForeColor = Color.Red;
                }  
            }
        }

        private void LabelLeaveCurrent()
        {
            throw new NotImplementedException();
        }

        private void LabelCurrent()
        {
            throw new NotImplementedException();
        }

        private Point[] PointPentagon(int x, int y, int radius)
        {
            Point[] points = new Point[5];
            double angle = Math.PI / 2.0;

            for (int i = 0; i < 5; i++)
            {
                points[i] = new Point((int)(x + radius * Math.Cos(angle)), (int)(y - radius * Math.Sin(angle)));
                angle += 2 * Math.PI / 5;
            }
            return points;
        }

        private Point[] PointHexagon(int x, int y, int radius)
        {
            Point[] points = new Point[6];
            double angle = Math.PI / 2.0;

            for (int i = 0; i < 6; i++)
            {
                points[i] = new Point((int)(x + radius * Math.Cos(angle)), (int)(y - radius * Math.Sin(angle)));
                angle += Math.PI / 3;
            }
            return points;
        }
    }
}
