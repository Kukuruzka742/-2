using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace KURSOVA
{
    public partial class Form1 : Form
    {
        List<Block> list = new List<Block>();
        PictureBox CurrentSprite;
        public int width;
        public Dictionary<Button, dynamic[]> SpritesCod = new Dictionary<Button, dynamic[]>();

        public Form1()
        {
            InitializeComponent();
        }
        
        void up(Block ev)
        {
            ev.panel1.BringToFront();
            ev.panel2.BringToFront();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int Y = 10;
            width = 0;
            string path = @"Blocks\";
            for (int i = 0; i < Directory.GetFiles(path, "*.png").Length; i++)
            {
                Button but = new Button();
                CreateFlatButton(but);
                but.BackgroundImage = Image.FromFile(path + i.ToString() + ".png");
                but.BackgroundImageLayout = ImageLayout.Zoom;
                but.Size = new Size(Convert.ToInt32(but.BackgroundImage.Width / 1.5), Convert.ToInt32(but.BackgroundImage.Height / 1.5));
                but.Location = new Point(0, Y);
                Y += but.BackgroundImage.Height / 2 + 25;
                panel1.Controls.Add(but);
                if (but.Width > width)
                    width = but.Width;
                GiveTypeForBlock(but, i + 1);

            }
            #region Растановка основных панелей
            width += -100;
            panel1.Width = width;
            panel1.Height = Height - 40;
            if (Width < 2000)
            {
                Pkoding.Location = new Point(width, 0);
                Pkoding.Size = new Size(700, Height - 40);
            }
            else
            {
                Pkoding.Location = new Point(width, 0);
                Pkoding.Size = new Size(900, Height - 40);
            }
            scene.Location = new Point(Pkoding.Left + Pkoding.Width, 0);
            scene.Size = new Size(Width - (Pkoding.Left + Pkoding.Width) - 15, Height/2);
            Sprites.Location = new Point(Pkoding.Left + Pkoding.Width, Height - 110 - 70);
            Sprites.Size = new Size(Width - (Pkoding.Left + Pkoding.Width) - 15, 70);
            Backgrounds.Location = new Point(Pkoding.Left + Pkoding.Width, Height - 110);
            Backgrounds.Size = new Size(Width - (Pkoding.Left + Pkoding.Width) - 15, 70);
            scene.BackgroundImage = new Bitmap("WhiteBackground.png");
            flag.Left = Pkoding.Left + Pkoding.Width + 15;
            flag.BringToFront();
            flag.BackColor = Color.White;
            Characteristics.Location = new Point(scene.Left, scene.Top + scene.Height + 15);
            Characteristics.Size = new Size(Width - (Pkoding.Left + Pkoding.Width) - 15, Height - scene.Height - 215);
            SetRoundedShape(pictureBox1, 30);
            SetRoundedShape(pictureBox2, 30);
            SetRoundedShape(pictureBox3, 40);
            SetRoundedShape(Characteristics, 50);
            SetRoundedShape(scene, 50);
            SetRoundedShape(Pkoding, 50);
            SetRoundedShape(panel1, 50);
            SetRoundedShape(Sprites, 50);
            SetRoundedShape(Backgrounds, 50);
            #endregion
        }

        void GiveTypeForBlock(Button button, int type)
        {
            button.Click += delegate (object s, EventArgs v)
            {
                if (CurrentSprite != null)
                {
                    if (type < 4)
                    {
                        EventBlock eve = new EventBlock(this, Pkoding, type);
                        if (type == 1) eve.flag = flag;
                        eve.GetEvents(CurrentSprite);
                        up(eve);
                        eve.panel1.MouseMove += mouse;
                        list.Add(eve);
                    }
                    if (type > 3 && type < 8)
                    {
                        BlueBlock bbl = new BlueBlock(Pkoding, type, CurrentSprite);
                        up(bbl);
                        bbl.panel1.MouseMove += mouse;
                        list.Add(bbl);
                    }
                    if (type > 7 && type < 17)
                    {
                        PurpleBlock pbl = new PurpleBlock(Pkoding, type, CurrentSprite);
                        up(pbl);
                        pbl.panel1.MouseMove += mouse;
                        list.Add(pbl);
                    }
                    if (type > 16 && type < 23)
                    {
                        GreenBlock gbl = new GreenBlock(Pkoding, type, CurrentSprite);
                        up(gbl);
                        gbl.panel1.MouseMove += mouse;
                        list.Add(gbl);
                    }
                    if (type > 22 && type < 27)
                    {
                        CycleBlock cbl = new CycleBlock(Pkoding, type, CurrentSprite);
                        up(cbl);
                        cbl.panel1.MouseMove += mouse;
                        list.Add(cbl);
                    }
                    Pkoding.Focus();
                }
                else MessageBox.Show("Ви не можете додати блоки поки не створювати персонажа(Натисніть на +)");
            };
        }

        public List<Button> SpriteList = new List<Button>();
        public List<Button> DeleteButtons = new List<Button>();

        private void button6_Click(object sender, EventArgs e)
        {
            string fileName;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "png|*.png";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Characteristics.Visible = true;
                fileName = openFileDialog1.FileName;
        //создаю кнопку возле плюсика( планирую этой же кнопкой переключчаться между спрайтами)
                Button but = new Button();
                but.Size = new Size(50, 50); 
                CreateFlatButton(but);
                but.BackgroundImage = new Bitmap(fileName);
                but.BackgroundImageLayout = ImageLayout.Stretch;
                but.Click += But_Click;
                Button delBut = new Button();
                delBut.Size = new Size(15, 15);
                delBut.BackgroundImageLayout = ImageLayout.Zoom;
                delBut.BackgroundImage = Image.FromFile("DeleteButton.png");
                delBut.BackColor = Color.FromArgb(82, 163, 214);
                but.Controls.Add(delBut);

                CreateFlatButton(delBut);
                
                DeleteButtons.Add(delBut);
                SetRoundedShape(but, 10);
                Sprites.Controls.Add(but);
                SpriteList.Add(but);
        //Создаю картинку на другой картинке этоже и станет моим окном приложения скретча
                PictureBox sp = new PictureBox();
                sp.ImageLocation = fileName;
                sp.Image = new Bitmap(fileName);
                sp.SizeMode = PictureBoxSizeMode.StretchImage;
                sp.Size = new Size(50, 50);
                sp.BackColor = Color.Transparent;
                sp.Location = new Point(scene.Width / 2 - sp.Width / 2, scene.Height / 2 - sp.Height / 2);
                sp.MouseDown += Sp_MouseDown;
                sp.MouseMove += Sp_MouseMove;
                sp.MouseLeave += (s, t) => Cursor = Cursors.Default;
                sp.MouseUp += Sp_MouseUp;
                scene.Controls.Add(sp);
                sp.BringToFront();

                //тут создаеться панель в которой будут блоки исключительно для своего спрайта
                Panel codpanel = new Panel();
                codpanel.Location = new Point(width, 0);
                codpanel.Size = Pkoding.Size;
                codpanel.AutoScroll = true;
                codpanel.BackColor = Color.FromArgb(215, 228, 242);
                Controls.Add(codpanel);
                codpanel.BringToFront();
                SetRoundedShape(codpanel, 50);
                codpanel.MouseMove += mouse;

                PictureBox delzone = new PictureBox();
                delzone.Location = new Point(0, codpanel.Height - 117);
                delzone.Size = new Size(codpanel.Width * 2, 100);
                delzone.BackColor = Color.FromArgb(201, 160, 220);
                Label l = new Label();
                string space = "       ";
                for (int i = 0; i < 3; i++)
                    space += space;
                l.Text = "Зона для видалення блоків" + space + "Зона для видалення блоків" + space + "Зона для видалення блоків";
                l.AutoSize = false;
                l.Width = delzone.Width;
                l.Dock = DockStyle.Fill;
                l.TextAlign = ContentAlignment.MiddleCenter;
                l.ForeColor = Color.FromArgb(138, 43, 226);
                l.Font = new Font(label1.Font, label1.Font.Style | FontStyle.Bold);
                delzone.Controls.Add(l);
                codpanel.Controls.Add(delzone);
                Pkoding = codpanel;
                CurrentSprite = sp;
                FoolCharakteristic();
                SpritesCod.Add(but, new dynamic[] { codpanel, sp , delBut}); // здесь в словаре связываю кнопку переключатель и панель  
            }
            for (int i = 0; i < SpriteList.Count(); i++)
            {
                if (i == 0)
                {
                    SpriteList[0].Location = new Point(70, 10);
                    DeleteButtons[0].Location = new Point(35, 35);
                    deletebut(0);
                }
                else
                {
                    SpriteList[i].Location = new Point((i + 1) * 50 + (i + 1) * 20, 10);
                    DeleteButtons[i].Location = new Point(35, 35);
                    deletebut(i);
                }
            }
            if (SpriteList.Count() >= 7)
            {
                Sprites.Location = new Point(Pkoding.Left + Pkoding.Width, Height - 110 - 90);
                Sprites.Size = new Size(Width - (Pkoding.Left + Pkoding.Width) - 15, 90);
                SetRoundedShape(Sprites, 50);
            }
            else
            {
                Sprites.Location = new Point(Pkoding.Left + Pkoding.Width, Height - 110 - 70);
                Sprites.Size = new Size(Width - (Pkoding.Left + Pkoding.Width) - 15, 70);
                SetRoundedShape(Sprites, 50);
            }
        }

        void FoolCharakteristic()
        {
            SpritName.Text = Path.GetFileName(CurrentSprite.ImageLocation);
            textBox1.Text = CurrentSprite.ImageLocation;
            label11.Text = CurrentSprite.Height.ToString();
            label12.Text = CurrentSprite.Width.ToString();
        }

        void FoolCharakteristic(PictureBox pic)
        {
            SpritName.Text = Path.GetFileName(pic.ImageLocation);
            textBox1.Text = pic.ImageLocation;
            label11.Text = pic.Height.ToString();
            label12.Text = pic.Width.ToString();
            if (pic.Visible == true)
            {
                label8.Text = "Так";
                pictureBox3.BackgroundImage = Image.FromFile("Yes.png");
            }
            else
            {
                label8.Text = "Ні";
                pictureBox3.BackgroundImage = Image.FromFile("No.png");
            }
            Characteristics.Visible = true;
        }

        private void Sp_MouseDown(object sender, MouseEventArgs e)
        {
            ((PictureBox)sender).BringToFront();
            if ((e.X <= 2 || e.X >= ((PictureBox)sender).Width - 3) || (e.Y <= 2 || e.Y >= ((PictureBox)sender).Height - 3))
            {
                if (e.X <= 2)
                {
                    ((PictureBox)sender).Tag = 2;
                }
                if (e.X >= ((PictureBox)sender).Width - 3)
                {
                    ((PictureBox)sender).Tag = 3;
                }
                if (e.Y <= 2)
                {
                    ((PictureBox)sender).Tag = 4;
                }
                if (e.Y >= ((PictureBox)sender).Height - 3)
                {
                    ((PictureBox)sender).Tag = 5;
                }
            }
            else
                ((PictureBox)sender).Tag = 1;
            FoolCharakteristic(((PictureBox)sender));
        }

        private void Sp_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X <= 2 ||e.X >= ((PictureBox)sender).Width - 3 ||e.Y <= 2 || e.Y >= ((PictureBox)sender).Height - 3)
            {
                Cursor = Cursors.Hand;
            }
            else Cursor = Cursors.Default;
            switch (Convert.ToInt32(((PictureBox)sender).Tag))
            {
                case 1:
                    {
                        ((PictureBox)sender).Location = new Point(Cursor.Position.X - scene.Left - ((PictureBox)sender).Width / 2, Cursor.Position.Y - 23 - scene.Top - ((PictureBox)sender).Height / 2);
                    }
                    break;
                case 2:
                    {
                        if (e.X <= 2)
                        {
                            ((PictureBox)sender).Width += 2;
                            ((PictureBox)sender).Left -= 2;
                        }
                        else
                        {
                            ((PictureBox)sender).Width -= 2;
                            ((PictureBox)sender).Left += 2;
                        }
                    }
                    break;
                case 3:
                    {
                        if (e.X >= ((PictureBox)sender).Width - 3)
                            ((PictureBox)sender).Width += 2;
                        else ((PictureBox)sender).Width -= 2;
                    }
                    break;
                case 4:
                    {
                        if (e.Y <= 2)
                        {
                            ((PictureBox)sender).Height += 2;
                            ((PictureBox)sender).Top -= 2;
                        }
                        else
                        {
                            ((PictureBox)sender).Height -= 2;
                            ((PictureBox)sender).Top += 2;
                        }  
                    }
                    break;
                case 5:
                    {
                        if (e.Y >= ((PictureBox)sender).Height - 3)
                            ((PictureBox)sender).Height += 2;
                        else ((PictureBox)sender).Height -= 2;
                    }
                    break;
            }
            label1.Text = (((PictureBox)sender).Left + ((PictureBox)sender).Width /2).ToString();
            label2.Text = (((PictureBox)sender).Top  + ((PictureBox)sender).Height /2).ToString();
            FoolCharakteristic(((PictureBox)sender));
            foreach (Button key in SpritesCod.Keys)
            {
                if (SpritesCod[key][1] == ((PictureBox)sender))
                {
                    But_Click((object)key, e);
                }
            }
        }

        private void Sp_MouseUp(object sender, MouseEventArgs e)
        {
            FoolCharakteristic(((PictureBox)sender));
            ((PictureBox)sender).Tag = 0;
        }

        private void deletebut(int q)
        {
            Button delbut = DeleteButtons[q];
            delbut.Click += delegate (object s, EventArgs v)
            {
                foreach (Button key in SpritesCod.Keys)
                {
                    if (SpritesCod[key][2] == delbut)
                    {
                        Characteristics.Visible = false;
                        Button but = key;
                        Panel panel = SpritesCod[but][0];
                        PictureBox img = SpritesCod[but][1];
                        DeleteButtons.Remove(delbut);
                        SpriteList.Remove(but);
                        SpritesCod.Remove(but);
                        delbut.Dispose();
                        but.Dispose();
                        img.Dispose();
                        panel.Dispose();
                        for (int i = 0; i < SpriteList.Count(); i++)
                        {
                            if (i == 0)
                            {
                                SpriteList[0].Location = new Point(70, 10);
                                DeleteButtons[0].Location = new Point(35, 35);
                                deletebut(0);
                            }
                            else
                            {
                                SpriteList[i].Location = new Point((i + 1) * 50 + (i + 1) * 20, 10);
                                DeleteButtons[i].Location = new Point(35, 35);
                                deletebut(i);
                            }
                        }
                        if (SpriteList.Count() >= 7)
                        {
                            Sprites.Location = new Point(Pkoding.Left + Pkoding.Width, Height - 110 - 90);
                            Sprites.Size = new Size(Width - (Pkoding.Left + Pkoding.Width) - 15, 90);
                            SetRoundedShape(Sprites, 50);
                        }
                        else
                        {
                            Sprites.Location = new Point(Pkoding.Left + Pkoding.Width, Height - 110 - 70);
                            Sprites.Size = new Size(Width - (Pkoding.Left + Pkoding.Width) - 15, 70);
                            SetRoundedShape(Sprites, 50);
                        }
                        break;
                    }
                }
            };
        }

        private void But_Click(object sender, EventArgs e)
        {
            Pkoding = SpritesCod[((Button)sender)][0];// тут типа присвоение чтобы блоки понимали куда им создаваться
            CurrentSprite = SpritesCod[((Button)sender)][1];
            Pkoding.BringToFront();
            FoolCharakteristic(CurrentSprite);
        }

        public void CreateFlatButton(Button but)
        {
            but.FlatStyle = FlatStyle.Flat;
            but.FlatAppearance.BorderSize = 0;
            but.FlatAppearance.MouseDownBackColor = Color.Transparent;
            but.FlatAppearance.MouseOverBackColor = Color.Transparent;
        }

        public void SetRoundedShape(Control control, int radius)
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddLine(radius, 0, control.Width - radius, 0);
            path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
            path.AddLine(control.Width, radius, control.Width, control.Height - radius);
            path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
            path.AddLine(control.Width - radius, control.Height, radius, control.Height);
            path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
            path.AddLine(0, control.Height - radius, 0, radius);
            path.AddArc(0, 0, radius, radius, 180, 90);
            control.Region = new Region(path);
        }

        #region Background

        List<Button> ButBackground = new List<Button>();
        List<Button> DelBackground = new List<Button>();

        private void button7_Click(object sender, EventArgs e)      // добавление задних фонов
        {
            string fileName;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "png|*.png";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog1.FileName;
                //создаю кнопку возле плюсика( планирую этой же кнопкой переключчаться между спрайтами)
                Button but = new Button();
                but.Size = new Size(100, 50);
                CreateFlatButton(but);
                but.BackgroundImage = new Bitmap(fileName);
                but.BackgroundImageLayout = ImageLayout.Stretch;
                but.Click += But_BackgroundClick;
                Button delBut = new Button();
                delBut.Size = new Size(15, 15);
                delBut.BackgroundImageLayout = ImageLayout.Zoom;
                delBut.BackgroundImage = Image.FromFile("DeleteButton.png");
                delBut.BackColor = Color.Red;
                but.Controls.Add(delBut);

                CreateFlatButton(delBut);

                DelBackground.Add(delBut);
                SetRoundedShape(but, 10);
                Backgrounds.Controls.Add(but);
                ButBackground.Add(but);
                scene.BackgroundImage = but.BackgroundImage;
                if (SpriteList.Count == 0)
                {
                    Characteristics.Visible = false;
                }
            }
            for (int i = 0; i < ButBackground.Count(); i++)
            {
                if (i == 0)
                {
                    ButBackground[0].Location = new Point(120, 10);
                    DelBackground[0].Location = new Point(85, 35);
                    deletebutBack(0);
                }
                else
                {
                    ButBackground[i].Location = new Point((i+1) * 100 + 20 * (i+1), 10);
                    DelBackground[i].Location = new Point(85, 35);
                    deletebutBack(i);
                }
                ButBackground[i].Left -= 50;
            }
            
        }

        private void deletebutBack(int v)                           // удаление задних фонов
        {
            Button dbut = DelBackground[v];
            dbut.Click += delegate (object s, EventArgs l)
            {
                Button b = ButBackground[v];
                ButBackground.RemoveAt(v);
                DelBackground.RemoveAt(v);
                b.Dispose();
                dbut.Dispose();
                scene.BackgroundImage = new Bitmap("WhiteBackground.png");
                scene.BackColor = Color.White;
                for (int i = 0; i < ButBackground.Count(); i++)
                {
                    if (i == 0)
                    {
                        ButBackground[0].Location = new Point(120, 10);
                        DelBackground[0].Location = new Point(85, 35);
                        deletebutBack(0);
                    }
                    else
                    {
                        ButBackground[i].Location = new Point((i + 1) * 100 + 20 * (i + 1), 10);
                        DelBackground[i].Location = new Point(85, 35);
                        deletebutBack(i);
                    }
                    ButBackground[i].Left -= 50;
                }
            };
        }      

        private void But_BackgroundClick(object sender, EventArgs e)
        {
            scene.BackgroundImage = ((Button)sender).BackgroundImage;
        }

        #endregion

        private bool DeleteCollision(Block Block)
        {
            if (Block.panel1.Top + Block.panel1.Height >= Pkoding.Height - 117)
            {
                switch (Block.color.ToArgb())
                {
                    case -16640:
                        {
                            list.Remove(((EventBlock)Block));
                            ((EventBlock)Block).Destroy(CurrentSprite);
                            Block = null;
                        }
                        break;
                    default:
                        {
                            list.Remove(Block);
                            Block.Destroy();
                            Block = null;   
                        }
                        break;
                }
                return true;
            }
            return false;
        }

        private void mouse(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < list.Count(); i++)
            {
                if (DeleteCollision(list[i]))
                    break;

                for (int j = 0; j < list.Count(); j++)
                {
                    if (i != j)
                    {
                        list[i].AllColision(list[j].my_gb);
                    }
                }
            }
            if (CurrentSprite != null)
                FoolCharakteristic();
        }

        private void flag_Click(object sender, EventArgs e)
        {
            if (CurrentSprite != null)
                FoolCharakteristic();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (CurrentSprite != null)
                FoolCharakteristic();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (CurrentSprite.Visible == true)
            {
                label8.Text = "Ні";
                pictureBox3.BackgroundImage = Image.FromFile("No.png");
                CurrentSprite.Visible = false;
            }
            else
            {
                label8.Text = "Так";
                pictureBox3.BackgroundImage = Image.FromFile("Yes.png");
                CurrentSprite.Visible = true;
            }
        }
    }
}
