using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Компоновщик
{
    public partial class Form1 : Form
    {
        Car car;

        Body body;
        Engine engine;
        Chassis chassis;

        public Form1()
        {
            InitializeComponent();
            car = new Car();
            body = new Body();
            engine = new Engine();
            chassis = new Chassis();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                pictureBox2.BackgroundImage = body.image;
                car.Add(body);
            }
            else
            {
                pictureBox2.BackgroundImage = null;
                car.Remove(body);
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                pictureBox1.BackgroundImage = engine.image;
                car.Add(engine);
            }
            else
            {
                pictureBox1.BackgroundImage = null;
                car.Remove(engine);
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                pictureBox3.BackgroundImage = chassis.image;
                car.Add(chassis);
            }
            else
            {
                pictureBox3.BackgroundImage = null;
                car.Remove(chassis);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                car.ChangeSize(Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox1.Text));
                if (checkBox1.Checked)
                    pictureBox2.BackgroundImage = body.image;
                if (checkBox2.Checked)
                    pictureBox1.BackgroundImage = engine.image;
                if (checkBox3.Checked)
                    pictureBox3.BackgroundImage = chassis.image;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            car.GetInfo();
        }
    }
}
