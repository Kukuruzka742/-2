using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace KURSOVA
{
    public class GroupBlocks
    {
        public List<Block> blocks = new List<Block>();

        public GroupBlocks(List<Block> list)
        {
            blocks = list;
        }
        public GroupBlocks(Block block)
        {
            blocks.Add(block);
            Formation();
        }

        public void Add(Block block)
        {
            if (block.grouped == false)
            {
                blocks.Add(block);
                Formation();
            }
        }

        public void Remove(Block block)
        {
            if (block.grouped != false)
            {
                blocks.Remove(block);
                Formation();
            }
        }
        public void Formation()
        {
            int X = blocks[0].panel1.Left;
            int Y = blocks[0].panel1.Top;
            for (int i = 0; i < blocks.Count(); i++)
            {
                blocks[i].panel1.Location = new Point(X, Y);
                blocks[i].panel2.Location = new Point(blocks[i].panel1.Left + 20, blocks[i].panel1.Top + blocks[i].panel1.Height / 3 + 13);
                Y += 36;

                blocks[i].panel2.BringToFront();
            }
        }
    }

    public class BlueBlock : Block
    {
        int type;
        PictureBox Hero;

        public BlueBlock(Panel forma, int type, PictureBox hero) : base(forma)
        {
            color = Color.FromArgb(76, 151, 255);
            this.type = type;
            Create();
            Hero = hero;
        }

        public void Create()
        {
            switch (type)
            {
                case 4:
                    {
                        FoolInside(new dynamic[] { "Змінити x на ", 10}, color);
                    }
                    break;
                case 5:
                    {
                        FoolInside(new dynamic[] { "Змінити y на ", 10 }, color);
                    }
                    break;
                case 6:
                    {
                        FoolInside(new dynamic[] { "Перейти на випадкову позицію   ",}, color);
                    }
                    break;
                case 7:
                    {
                        FoolInside(new dynamic[] { "Перемістити на x:", 5, "y:", 5 }, color);
                    }
                    break;
            }
        }



        public void PlayMetod()
        {
            switch (type)
            {
                case 4:
                    {
                        Hero.Left += Convert.ToInt32(list[1].Text);
                    }
                    break;
                case 5:
                    {
                        Hero.Top += Convert.ToInt32(list[1].Text);
                    }
                    break;
                case 6:
                    {
                        Random rnd = new Random();
                        Hero.Top = Convert.ToInt32(rnd.Next(((PictureBox)Hero.Parent).Height - Hero.Height));
                        Hero.Left = Convert.ToInt32(rnd.Next(((PictureBox)Hero.Parent).Width - Hero.Width));
                    }
                    break;
                case 7:
                    {
                        Hero.Left = Convert.ToInt32(list[1].Text);
                        Hero.Top = Convert.ToInt32(list[3].Text);
                    }
                    break;
            }
        }
    }

    public class PurpleBlock : Block
    {
        int type;
        PictureBox Hero;
        public PurpleBlock(Panel forma, int type, PictureBox hero) : base(forma)
        {
            color = Color.FromArgb(119, 77, 203);
            this.type = type;
            Create();
            Hero = hero;
        }

        public void Create()
        {
            switch (type)
            {
                case 8: FoolInside(new dynamic[] { "Збільшити в ширину на ", 10 }, color);  break;
                case 9: FoolInside(new dynamic[] { "Збільшити ріст на ", 10 }, color);  break;
                case 10: FoolInside(new dynamic[] { "Задати розмір, ширина:", 5, "висота:", 5 }, color); break;
                case 11:
                    {
                        ComboBox cm = new ComboBox();
                        string[] mass = new string[] { "0", "90", "180", "270" };
                        cm.Items.AddRange(mass);
                        FoolInside(new dynamic[] { "Повернутись на ", cm, " градусів по Х" }, color);
                    }
                    break;
                case 12:
                    {
                        ComboBox cm = new ComboBox();
                        string[] mass = new string[] { "0", "90", "180", "270" };
                        cm.Items.AddRange(mass);
                        FoolInside(new dynamic[] { "Повернутись на ", cm, " градусів по Y" }, color);
                    }
                    break;
                case 13: FoolInside(new dynamic[] { "Перемістити на задній план " }, color); break;
                case 14: FoolInside(new dynamic[] { "Перемістити на передній план " }, color); break;
                case 15: FoolInside(new dynamic[] { "Зробити невидимим  " }, color); break;
                case 16: FoolInside(new dynamic[] { "Зробити видимим  " }, color); break;
            }
        }

        public void PlayMetod()
        {
            switch (type)
            {
                case 8: Hero.Width += Convert.ToInt32(list[1].Text);Hero.Left -= Convert.ToInt32(list[1].Text) / 2; break;
                case 9: Hero.Height += Convert.ToInt32(list[1].Text);Hero.Top -= Convert.ToInt32(list[1].Text) / 2; break;
                case 10:
                    {
                        Hero.Width += Convert.ToInt32(list[1].Text);
                        Hero.Left -= Convert.ToInt32(list[1].Text) / 2;
                        Hero.Height += Convert.ToInt32(list[3].Text);
                        Hero.Top -= Convert.ToInt32(list[3].Text) / 2;
                    } 
                    break;
                case 11:
                    {
                        switch (list[1].Text)
                        {
                            case "0": break;
                            case "90": Hero.Image.RotateFlip(RotateFlipType.Rotate90FlipX); break;
                            case "180": Hero.Image.RotateFlip(RotateFlipType.Rotate180FlipX); break;
                            case "270": Hero.Image.RotateFlip(RotateFlipType.Rotate270FlipX); break;
                        }
                        Hero.Refresh();
                    }
                    break;
                case 12:
                    {
                        switch (list[1].Text)
                        {
                            case "0": break;
                            case "90": Hero.Image.RotateFlip(RotateFlipType.Rotate90FlipY); break;
                            case "180": Hero.Image.RotateFlip(RotateFlipType.Rotate180FlipY); break;
                            case "270": Hero.Image.RotateFlip(RotateFlipType.Rotate270FlipY); break;
                        }
                        Hero.Refresh();
                    }
                    break;
                case 13: Hero.SendToBack(); break;
                case 14: Hero.BringToFront(); break;
                case 15: Hero.Visible = false; break;
                case 16: Hero.Visible = true; break;
            }
        }
    }

    public class GreenBlock : Block
    {
        int type;
        PictureBox Hero;
        Color linecolor;
        Point Apoint;
        Point Bpoint;
        int penwidth;
        
        public GreenBlock(Panel forma, int type, PictureBox hero) : base(forma)
        {
            color = Color.FromArgb(56, 148, 56);
            linecolor = Color.Black;
            penwidth = 1;
            this.type = type;
            Create();
            Hero = hero;
        }

        private void Create()
        {
            switch (type)
            {
                case 17: FoolInside(new dynamic[] { "Встановити точку А " }, color); break;
                case 18: FoolInside(new dynamic[] { "Встановити точку В " }, color); break;
                case 19: FoolInside(new dynamic[] { "Зв'язати точки А та В " }, color); break;
                case 20:
                    {
                        ComboBox comboBox = new ComboBox();
                        string[] mass = new string[] { "Чорний", "Білий", "Червоний", "Синій", "Зелений" };
                        comboBox.Items.AddRange(mass);
                        FoolInside(new dynamic[] { "Встановити колір", comboBox}, color);
                    }
                    break;
                case 21: FoolInside(new dynamic[] { "Встановити ширину ", 5 }, color); break;
                case 22: FoolInside(new dynamic[] { "Очистити задній фон "}, color); break;
            }
        }

        public void PlayMetod()
        {
            switch (type)
            {
                case 17: Apoint = new Point(Hero.Left+Hero.Width/2, Hero.Top+Hero.Height/2); break;
                case 18: Bpoint = new Point(Hero.Left + Hero.Width / 2, Hero.Top + Hero.Height / 2); break;
                case 19:
                    {
                        for (int i = 1; i < my_gb.blocks.Count(); i++)
                        {
                            if (my_gb.blocks[i].GetType().ToString() == "KURSOVA.GreenBlock")
                            {
                                if (((GreenBlock)my_gb.blocks[i]).Apoint != null && ((GreenBlock)my_gb.blocks[i]).Apoint != new Point(0, 0))
                                    Apoint = ((GreenBlock)my_gb.blocks[i]).Apoint;
                                if (((GreenBlock)my_gb.blocks[i]).Bpoint != null && ((GreenBlock)my_gb.blocks[i]).Bpoint != new Point(0, 0))
                                    Bpoint = ((GreenBlock)my_gb.blocks[i]).Bpoint;
                                if (((GreenBlock)my_gb.blocks[i]).linecolor != Color.Black)
                                    linecolor = ((GreenBlock)my_gb.blocks[i]).linecolor;
                                if (((GreenBlock)my_gb.blocks[i]).penwidth != 1)
                                    penwidth = ((GreenBlock)my_gb.blocks[i]).penwidth;
                            } 
                        }
                        if (Apoint != null && Bpoint != null)
                        {
                            ((PictureBox)Hero.Parent).BackColor = Color.Transparent;
                            Graphics g = Graphics.FromImage(((PictureBox)Hero.Parent).BackgroundImage); 
                            g.PageUnit = GraphicsUnit.Millimeter;
                            g.DrawLine(new Pen(linecolor, penwidth), Apoint, Bpoint);
                            g.Dispose();
                        }
                        else MessageBox.Show("Вам потрібно задати дві точки для зв'язку");
                    }
                    break;
                case 20:
                    {
                        switch (list[1].Text)
                        {
                            case "Чорний": linecolor = Color.Black; break;
                            case "Білий": linecolor = Color.White; break;
                            case "Червоний": linecolor = Color.Red; break;
                            case "Синій": linecolor = Color.Blue; break;
                            case "Зелений": linecolor = Color.Green; break;
                        }
                    }
                    break;
                case 21: penwidth = Convert.ToInt32(list[1].Text); break;
                case 22: Hero.Parent.BackgroundImage = new Bitmap("WhiteBackground.png"); break;
            }
        }
    }

    public class CycleBlock : Block
    {
        int startposistion;
        int endposistion;
        public int type;
        PictureBox Hero;
        int sec;

        public CycleBlock(Panel forma, int type, PictureBox hero) : base(forma)
        {
            color = Color.FromArgb(255, 171, 25);
            this.type = type;
            Create();
            Hero = hero;
        }

        private void Create()
        {
            switch (type)
            {
                case 23: FoolInside(new dynamic[] { "Початок циклу, кількість повторень  ", 5, "кількість секунд на круг", 5}, color); break;
                case 24: FoolInside(new dynamic[] { "  Кінець циклу  " }, color); break;
                case 25: FoolInside(new dynamic[] { "Вивести повідомлення з таким текстом ", 10 }, color); break;
            }
        }

        public void PlayMetod()
        {
            switch (type)
            {
                case 23:
                    {
                        for (int i = 1; i < my_gb.blocks.Count(); i++)
                        {
                            if (my_gb.blocks[i].GetType().ToString() == "KURSOVA.CycleBlock")
                            {
                                if (((CycleBlock)my_gb.blocks[i]).type == 23 && Convert.ToInt32((my_gb.blocks[i].list[1].Text)) != 0)
                                    startposistion = i + 1;
                                if (((CycleBlock)my_gb.blocks[i]).type == 24 &&  i >= startposistion)
                                    endposistion =  i;
                            }
                        }
                        if (startposistion != 0 && endposistion != 0 && Convert.ToInt32(list[1].Text) > 0)
                        {
                            if (Convert.ToDouble(list[3].Text) == 0)
                            {
                                for (int k = 0; k < Convert.ToInt32(list[1].Text) - 1; k++)
                                {
                                    for (int i = startposistion; i < endposistion; i++)
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
                            }else
                            {
                                {
                                    System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                                    timer.Interval = Convert.ToInt32(Convert.ToDouble(list[3].Text) * 1000);
                                    sec = Convert.ToInt32(list[1].Text);
                                    timer.Tick += Timer_Tick;
                                    ((EventBlock)my_gb.blocks[0]).timerPause = true;
                                    timer.Enabled = true;
                                }
                            }
                            
                        }
                        else MessageBox.Show("Дані введено не правильно!");
                    }
                    break;
                case 24:
                    {

                    }
                    break;
                case 25: MessageBox.Show(list[1].Text); break;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            for (int i = startposistion; i < endposistion; i++)
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
            sec--;
            if (sec <= 0)
            {
                for (int i = endposistion; i < my_gb.blocks.Count(); i++)
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
                ((EventBlock)my_gb.blocks[0]).timerPause = false;
                ((System.Windows.Forms.Timer)sender).Enabled = false;
            }
        }
    }
}
