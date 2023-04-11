using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace диплом
{
    public class Word
    {
        public List<Label> labels = new List<Label>();
        public Panel panel;
        Point coord;
        bool horyzontal;
        string word;
        int[,] c;

        public Word(string w, bool h, Point c)
        {
            word = w;
            horyzontal = h;
            coord = c;
        }

        public void DrawWord(Form f)
        {
            CreateElements();
            f.Controls.Add(panel);
        }

        private void CreateElements()
        {
            char[] letter = new char[word.Length];
            letter = word.ToCharArray();

            Random random = new Random();

            panel = new Panel()
            {
                BackColor = SystemColors.GradientInactiveCaption,
                Location = new Point(coord.X * 54, coord.Y * 54)
            };

            for (int i = 0; i < letter.Length; i++)
            {
                Label label = new Label()
                {
                    Font = new Font("Arial Black", (float)24.2, FontStyle.Bold),
                    Size = new Size(54, 54),
                    Location = new Point(0, 0),
                    BackColor = Color.FromArgb(random.Next(100, 130), random.Next(100, 200), 255),
                    Text = Convert.ToString(letter[i])
                };
                label.ForeColor = label.BackColor;
                labels.Add(label);
            }
            panel.Top += (coord.Y) * 10;
            panel.Left += (coord.X) * 10;

            HoryzontalPanelAdd();
        }

        private void SaveColors()
        {
            c = new int[labels.Count, 2];

            for (int i = 0; i < labels.Count; i++)
            {
                c[i, 0] = labels[i].BackColor.R;
                c[i, 1] = labels[i].BackColor.G;
            }
        }

        public void ShowWord(Form f)
        {
            SaveColors();
            Timer t = new Timer()
            {
                Enabled = false,
                Interval = 50,
            };
            t.Tick += Show;
            t.Enabled = true;
        }

        private void Show(object sender, EventArgs e)
        {
            for (int i = 0; i < labels.Count; i++)
            {
                c[i, 0] += 5;
                c[i, 1] += 5;
            }
            for (int i = 0; i < labels.Count; i++)
            {
                labels[i].ForeColor = Color.FromArgb((c[i, 0] > 255) ? 255 : c[i, 0], (c[i, 1] > 255) ? 255 : c[i, 1], 255);
                if (labels[i].ForeColor == Color.FromArgb(255, 255, 255))
                {
                    ((Timer)(sender)).Enabled = false;
                }
            }
        }

        private void HoryzontalPanelAdd()
        {
            if (horyzontal)
            {
                panel.Width = labels.Count * 54 + 10 * (1 + labels.Count);
                panel.Height = 74;
                int x = 10;
                for (int i = 0; i < labels.Count; i++)
                {
                    panel.Controls.Add(labels[i]);
                    if (i == 0)
                        labels[i].Location = new Point(x, 10);
                    else
                    {
                        x += 64;
                        labels[i].Location = new Point(x, 10);
                    }
                }
            }
            else
            {
                panel.Height = labels.Count * 54 + 10 * (1 + labels.Count);
                panel.Width = 74;
                int y = 10;
                for (int i = 0; i < labels.Count; i++)
                {
                    panel.Controls.Add(labels[i]);
                    if (i == 0)
                        labels[i].Location = new Point(10, y);
                    else
                    {
                        y += 64;
                        labels[i].Location = new Point(10, y);
                    }
                }
            }
        }
    }
}
