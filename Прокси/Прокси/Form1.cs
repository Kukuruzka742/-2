using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Прокси
{
    public partial class Form1 : Form
    {
        User user;
        RestaurantProxy restaurantProxy;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
            if (f2.DialogResult == DialogResult.OK)
            {
                user = new User(f2.textBox1.Text, Convert.ToInt32(f2.textBox2.Text));
                restaurantProxy = new RestaurantProxy(user);

                if (!restaurantProxy.currentUser.CanDrinkAlcohol)
                {
                    label4.Visible = false;
                    checkBox13.Visible = false;
                    checkBox14.Visible = false;
                    checkBox15.Visible = false;
                    checkBox16.Visible = false;
                    checkBox17.Visible = false;
                }
                MessageBox.Show("Добро пожаловать " + restaurantProxy.currentUser.Name + ", пожалуйста сделайте свой заказ");
                restaurantProxy.GetMenu();
                restaurantProxy.GetMenu();
            }
            else
                Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<CheckBox> checkBoxes = new List<CheckBox>();

            foreach (Control control in panel1.Controls)
            {
                if (control is CheckBox checkBox)
                {
                    checkBoxes.Add(checkBox);
                }
            }

            restaurantProxy.MakeAnOrder(checkBoxes.ToArray());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
