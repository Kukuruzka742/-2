using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Прокси
{
    public class RestaurantProxy : Restaurant
    {
        private Restaurant restaurant;

        public RestaurantProxy(User user) : base(user)
        {
        }

        public override Menu GetMenu()
        {
            if (restaurant == null)
            {
                restaurant = new Restaurant(currentUser);
                cachedMenu = restaurant.GetMenu();

                if (currentUser.CanDrinkAlcohol)
                {
                    cachedMenu.AddAlcohol();
                }
            }
            return restaurant.cachedMenu;
        }     
    }

    public class Restaurant
    {
        public Menu cachedMenu;
        public User currentUser;

        public Restaurant(User user)
        {
            currentUser = user;
        }

        public virtual Menu GetMenu()
        {
            Menu menu = new Menu();

            return menu;
        }

        public void MakeAnOrder(CheckBox[] c)
        {
            string order = "Ваш полный заказ на имя " + currentUser.Name + ":\n\n";
            foreach (CheckBox checkBox in c)
            {
                if (checkBox.Checked)
                {
                    cachedMenu.menus[checkBox.Text]++;
                    order += checkBox.Text + "\n";
                }
            }
            MessageBox.Show(order);
            ((Form1)((Panel)c[0].Parent).Parent).Close();
        }
    }

    public class Menu
    {
        public Dictionary<string, int> menus = new Dictionary<string, int>();

        public Menu()
        {
            menus.Add("Пица", 0);
            menus.Add("Макароны", 0);
            menus.Add("Плов", 0);
            menus.Add("Тартар", 0);
            menus.Add("Салат", 0);
            menus.Add("Лангустины", 0);
            menus.Add("Кокос", 0);
            menus.Add("Карагуя", 0);
            menus.Add("Мороженное", 0);
            menus.Add("Вода", 0);
            menus.Add("Колла", 0);
            menus.Add("ШвЭпс", 0);
        }

        public void AddAlcohol()
        {
            menus.Add("Вино", 0);
            menus.Add("Пиво", 0);
            menus.Add("Водка", 0);
            menus.Add("Жесть на пляже", 0);
            menus.Add("В52", 0);
        }
    }

    public class User
    {
        public string Name;
        int Age;
        public bool CanDrinkAlcohol;

        public User(string n, int a)
        {
            Name = n;
            Age = a;
            CanDrinkAlcohol = (Age >= 18);
        }
    }
}
