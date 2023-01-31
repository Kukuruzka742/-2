using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KURSOVA
{
    public class EventBlock : Block
    {
        //GroupBlocks EventGroupBlocks;
        public Panel panel3;
        int type;
        Form F;
        public Button flag;
        public bool timerPause;

        public EventBlock(Form RealForm, Panel F, int type) : base(F)
        {
            this.F = RealForm;
            color = Color.FromArgb(255, 191, 0);
            this.type = type;
            panel3 = new Panel();
            CreateEvent();
            grouped = true;
            timerPause = false;
        }

        public void Destroy(PictureBox Hero)
        {
            panel1.Dispose();
            panel2.Dispose();
            panel3.Dispose();
            switch (type)
            {
                case 1: flag.Click -= PlayMetod; break;
                case 2: F.KeyDown -= PlayKeyMetod; break;
                case 3: Hero.Click -= PlayMetod; break;
            }
        }

        public void CreateEvent()
        {
            switch (type)
            {
                case 1:
                    {
                        Image pic_img = Image.FromFile("GreenFlag.png");
                        FoolInside(new dynamic[] { "Коли ", pic_img, " натиснуто  " }, color);
                    }
                    break;
                case 2:
                    {
                        ComboBox comboBox = new ComboBox();
                        string[] buttons = new string[] { "Space", "Enter", "Up", "Down", "Left", "Right", "A", "S", "D", "W", "Z", "X", "C", "J", "K", "L" };
                        comboBox.Items.AddRange(buttons);
                        FoolInside(new dynamic[] { "Коли клавішу", comboBox, "натиснуто  " }, color);
                    }
                    break;
                case 3:
                    {
                        FoolInside(new dynamic[] { "Коли спрайт натиснуто  " }, color);
                    }
                    break;
            }

            for (int j = 0; j < list.Count(); j++)
            {
                if (list[j].GetType().ToString() == "System.Windows.Forms.Label")
                    ((Label)list[j]).MouseMove += mouseMove;
                if (list[j].GetType().ToString() == "System.Windows.Forms.PictureBox")
                    ((PictureBox)list[j]).MouseMove += mouseMove;
                if (list[j].GetType().ToString() == "System.Windows.Forms.ComboBox")
                    ((ComboBox)list[j]).MouseMove += mouseMove;
            }
            panel3.Size = new Size(100, 55);
            panel3.Location = new Point(panel1.Left, panel1.Top - panel1.Height / 2);
            panel3.BackColor = Color.FromArgb(255, 191, 0);
            SetRoundedShape(panel3, 55);
            form.Controls.Add(panel3);
            panel3.SendToBack();
            panel3.MouseClick += mouseEvents;
            panel3.MouseMove += mouseMove;
            panel2.MouseMove += mouseMove;
            panel1.MouseMove += mouseMove;
        }

        public new void mouseMove(object senser, MouseEventArgs s)
        {
            if (clicked && my_gb.blocks.Count() >= 1)
            {
                panel1.Location = new Point(Cursor.Position.X - panel1.Width / 2 - form.Left, Cursor.Position.Y - 40 - form.Top);
                panel2.Location = new Point(panel1.Left + 20, panel1.Top + panel1.Height / 3 + 13);
                panel3.Location = new Point(panel1.Left, panel1.Top - panel1.Height / 2);
                my_gb.Formation();
                panel1.BringToFront();
            }
        }

        public void GetEvents(PictureBox Hero)
        {
            switch (type)
            {
                case 1: flag.Click += PlayMetod; break;
                case 2: F.KeyDown += PlayKeyMetod; break;
                case 3: Hero.Click += PlayMetod; break;
            }
        }

        private void PlayKeyMetod(object sender, KeyEventArgs e)
        {
            KeysConverter kc = new KeysConverter();
            string Key = "Space";
            for (int j = 0; j < list.Count(); j++)
            {
                if (list[j].GetType().ToString() == "System.Windows.Forms.ComboBox")
                {
                    Key = ((ComboBox)list[j]).SelectedItem.ToString();
                    break;
                }
            }
            Keys key = (Keys)kc.ConvertFrom(Key);
            if (e.KeyCode == key)
            {
                for (int i = 1; i < my_gb.blocks.Count(); i++)
                {
                    if (timerPause == false)
                    {
                        if (my_gb.blocks[i].color == Color.FromArgb(76, 151, 255))
                            ((BlueBlock)my_gb.blocks[i]).PlayMetod();
                        if (my_gb.blocks[i].color == Color.FromArgb(119, 77, 203))
                            ((PurpleBlock)my_gb.blocks[i]).PlayMetod();
                        if (my_gb.blocks[i].color == Color.FromArgb(56, 148, 56))
                            ((GreenBlock)my_gb.blocks[i]).PlayMetod();
                        if (my_gb.blocks[i].color == Color.FromArgb(255, 171, 25))
                            ((CycleBlock)my_gb.blocks[i]).PlayMetod();
                    }
                }
            }
        }

        private void PlayMetod(object sender, EventArgs e)
        {
            for (int i = 1; i < my_gb.blocks.Count(); i++)
            {
                if (timerPause == false)
                {
                    if (my_gb.blocks[i].color == Color.FromArgb(76, 151, 255))
                        ((BlueBlock)my_gb.blocks[i]).PlayMetod();
                    if (my_gb.blocks[i].color == Color.FromArgb(119, 77, 203))
                        ((PurpleBlock)my_gb.blocks[i]).PlayMetod();
                    if (my_gb.blocks[i].color == Color.FromArgb(56, 148, 56))
                        ((GreenBlock)my_gb.blocks[i]).PlayMetod();
                    if (my_gb.blocks[i].color == Color.FromArgb(255, 171, 25))
                        ((CycleBlock)my_gb.blocks[i]).PlayMetod();
                } 
            }
        }
    }
}
