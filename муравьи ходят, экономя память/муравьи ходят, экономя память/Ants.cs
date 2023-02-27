using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace муравьи_ходят__экономя_память
{
    public class Param
    {
        public Image img;
        public Size size;
    }
    public class Bilder : Param
    {
        public Bilder()
        {
            size = new Size(20, 20);
            img = Image.FromFile("трудяга.png");
        }
    }
    public class Warrior : Param
    {
        public Warrior()
        {
            size = new Size(25, 25);
            img = Image.FromFile("воин.png");
        }
    }
    public class Queen : Param
    {
        public Queen()
        {
            size = new Size(40, 40);
            img = Image.FromFile("королева.png");
        }
    }
    public class Ants
    {          
        public Point xy;
        public Param p;

        public Ants(Point coords, Param ap)
        {
            p = ap;
            xy = coords;
        }
    }
}
