using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Реалізація_патерну_Адаптер
{
    public partial class Form1 : Form
    {
        public Car car;
        public Boat boat;

        Random random = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            textBox1.Visible = true;
            progressBar1.Visible = true;
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            numericUpDown1.Visible = false;
            numericUpDown2.Visible = false;
            numericUpDown3.Visible = false;
            numericUpDown4.Visible = false;
            button1.Visible = false;
            
            pictureBox1.Width = (int)numericUpDown3.Value;
            pictureBox1.Height = (int)numericUpDown4.Value;

            car = new Car();

            car.pic = pictureBox1;
            car.speed = (int)numericUpDown2.Value;
            car.armchairs = (int)numericUpDown1.Value;

            car.SetSize();
            car.Info(textBox1);

            timer1.Interval = 5000 / car.speed;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value += 1;
            if (progressBar1.Value == 50)
            {
                if (car != null)
                {
                    BackgroundImage = Image.FromFile("forboat.jpg");
                    boat = Boat.CarToBoat(car);
                    boat.SetSize();
                    boat.Info(textBox1);
                    timer1.Interval = 5000 / boat.speed;
                    car = null;
                }
                else
                {
                    BackgroundImage = Image.FromFile("forcar.jpg");
                    car = Car.BoatToCar(boat);
                    car.SetSize();
                    car.Info(textBox1);
                    timer1.Interval = 5000 / car.speed;
                }
                progressBar1.Value = 0;
            }
        }
    }
}
