using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Компоновщик
{
    class Car : Component
    {
        private List<Component> components = new List<Component>();

        public void Add(Component component)
        {
            components.Add(component);
        }

        public void Remove(Component component)
        {
            components.Remove(component);
        }

        public new void ChangeSize(int width, int height)
        {
            foreach (Component component in components)
            {
                component.ChangeSize(width, height);
            }
        }

        public new void GetInfo()
        {
            MessageBox.Show("На данный момент в машине присутствуют эти элементы");
            foreach (Component component in components)
            {
                component.GetInfo();
            }
        }
    }
}
