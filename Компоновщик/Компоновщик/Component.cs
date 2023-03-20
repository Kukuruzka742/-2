using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using System.Windows.Forms;

namespace Компоновщик
{
    abstract class Component
    {
        protected string name;
        public Bitmap image;

        public void ChangeSize(int width, int height)
        {
            image = new Bitmap(image, width, height);
        }

        public void GetInfo()
        {
            MessageBox.Show(name + ", имеет ширину " + image.Width + " и высоту " + image.Height);
        }
    }

    class Body : Component
    {
        public Body()
        {
            name = "Кузов";
            image = new Bitmap(Image.FromFile("кузов.png"), 70, 30);
        }
    }
    class Engine : Component
    {
        public Engine()
        {
            name = "Двигатель";
            image = new Bitmap(Image.FromFile("двигатель.png"), 60, 50);
        }
    }
    class Chassis : Component
    {
        public Chassis()
        {
            name = "Шасси";
            image = new Bitmap(Image.FromFile("шасси.png"), 70, 30);
        }
    }
}
