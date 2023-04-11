using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace диплом
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            textBox1.Visible = false;

            LevelCreator.DrawMap(this, Convert.ToInt32(textBox1.Text));
            for (int i = 0; i < LevelCreator.WordComponent.Count(); i++)
            {
                LevelCreator.WordComponent[i].ShowWord(this);
            }
            this.Refresh();
        }
    }
}
