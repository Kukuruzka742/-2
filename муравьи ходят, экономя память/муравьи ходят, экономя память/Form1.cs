using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace муравьи_ходят__экономя_память
{
    public partial class Form1 : Form
    {
        Graphics g;
        Random random;

        List<Ants> ants = new List<Ants>();
        Param [] Params = new Param [3];

        public Form1()
        {
            InitializeComponent();
            g = this.CreateGraphics();
            random = new Random();
            Params[0] = new Warrior();
            Params[1] = new Bilder();
            Params[2] = new Queen();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            timer1.Enabled = true;
        }

        public void AntPaint()
        {
            for (int i = 0; i < ants.Count(); i++)
            {
                ants[i].xy.Y += random.Next(-10, 10);
                ants[i].xy.X += 20;

                g.DrawImage(ants[i].p.img, new Rectangle(ants[i].xy, ants[i].p.size));
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ants.Count() == 500)
            {
                timer1.Enabled = false;
            }

            Point StartPosition = new Point(0, random.Next(Height/2 - 20, Height / 2 + 20));
            Ants NewAnt = new Ants(StartPosition, Params[random.Next(0, 3)]);

            ants.Add(NewAnt);
            g.Clear(SystemColors.Control);
            AntPaint();
        }
    }
}
