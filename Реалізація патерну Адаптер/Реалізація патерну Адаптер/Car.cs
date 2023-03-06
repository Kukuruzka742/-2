using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Реалізація_патерну_Адаптер
{
    public class Car
    {
        public PictureBox pic;
        public int speed;
        public int armchairs;

        public void SetSize()
        {
            pic.BackgroundImage = Image.FromFile("car.png");
            if (armchairs > 5)
            {
                for (int i = 1; i < armchairs-5; i++)
                {
                    if (i % 2 == 0)
                    {
                        pic.Width += 50;
                    }
                }
            }
        }

        public void Info(TextBox t)
        {
            t.Text = "Скорость: " + speed + Environment.NewLine +
                "Кресла: " + armchairs + Environment.NewLine +
                "Ширина: " + pic.Width + Environment.NewLine +
                "Высота: " + pic.Height;
        }

        public static Car BoatToCar(Boat b)
        {
            Car car = new Car();
            car.speed = b.speed * 2;
            car.armchairs = b.armchairs;
            car.pic = b.pic;
            car.pic.Width = Convert.ToInt32(b.pic.Width / 2);
            car.pic.BackgroundImage = Image.FromFile("car.png");
            return car;
        }
    }

    public class Boat
    {
        public PictureBox pic;
        public int speed;
        public int armchairs;

        public void SetSize()
        {
            pic.BackgroundImage = Image.FromFile("kater.png");
            if (armchairs > 5)
            {
                for (int i = 1; i < armchairs - 5; i++)
                {
                    if (i % 2 == 0)
                    {
                        pic.Width += 50;
                    }
                }
            }
        }

        public static Boat CarToBoat(Car c)
        {
            Boat boat = new Boat();
            boat.speed = Convert.ToInt32(c.speed / 2);
            boat.pic = c.pic;
            boat.pic.Width = c.pic.Width * 2;
            boat.armchairs = c.armchairs;
            boat.pic.BackgroundImage = Image.FromFile("kater.png");
            return boat;
        }

        public void Info(TextBox t)
        {
            t.Text = "Скорость: " + speed + Environment.NewLine +
                "Кресла: " + armchairs + Environment.NewLine +
                "Ширина: " + pic.Width + Environment.NewLine +
                "Высота: " + pic.Height;
        }
    }
}
