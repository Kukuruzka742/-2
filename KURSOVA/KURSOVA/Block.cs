using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KURSOVA
{
    public abstract class Block
    {
        public List<dynamic> list;

        public GroupBlocks my_gb;

        public Panel panel1, panel2;
        public Panel form;
        public Color color;
        public bool clicked, grouped, exit;

        public Block(Panel forma)
        {
            form = forma;
            list = new List<dynamic>();
            grouped = false;
            clicked = false;
            exit = false;
            panel1 = new Panel();
            panel2 = new Panel();
            
            my_gb = new GroupBlocks(this);
        }

        #region Creation

        public void FoolInside(dynamic[] mass, Color color)
        {
            int width = 5;
            for (int i = 0; i <= mass.Count(); i++)
            {
                if (i != 0)
                {
                    width += list[i - 1].Width + 5;
                }
                if (mass.Count() == i)
                {
                    break;
                }
                switch (mass[i].GetType().ToString())
                {
                    case "System.String":
                        {
                            Label label = new Label();
                            label.AutoSize = true;
                            label.Text = mass[i];
                            label.Width = label.Text.Length * 6;
                            label.Location = new Point(width, 11);
                            label.ForeColor = Color.White;
                            label.Font = new Font(label.Font, label.Font.Style | FontStyle.Bold);
                            label.MouseMove += new MouseEventHandler(mouseMove);
                            label.MouseClick += new MouseEventHandler(mouseEvents);
                            list.Add(label);
                            if (label.Text.Length > 10)
                            {
                                width += 10;
                            }
                            if (label.Text.Length < 5)
                            {
                                width += 5;
                            }
                        }
                        break;
                    case "System.Int32":
                        {
                            TextBox text = new TextBox
                            {
                                Text = "0",
                                TextAlign = HorizontalAlignment.Center,
                                Size = new Size(mass[i] * 5, 34),
                                Location = new Point(width, 9),
                                BorderStyle = BorderStyle.Fixed3D
                            };
                            text.MaxLength = 4;
                            text.MouseMove += new MouseEventHandler(mouseMove);


                            if (list[0].Text == "Вивести повідомлення з таким текстом ")
                            {
                                text.KeyPress += textBox2_KeyPress;
                                text.Text = "";
                                text.MaxLength = 100;
                            }
                            else
                            {
                                text.KeyPress += textBox1_KeyPress;
                            }

                            SetRoundedShape(text, 15);
                            list.Add(text);
                        }
                        break;
                    case "System.Drawing.Bitmap":
                        {
                            PictureBox pictureBox = new PictureBox
                            {
                                Image = mass[i],
                                Size = new Size(20, 20),
                                SizeMode = PictureBoxSizeMode.StretchImage,
                                BackColor = Color.Transparent,
                                Location = new Point(width + 6, 7)
                            };
                            pictureBox.MouseMove += new MouseEventHandler(mouseMove);
                            pictureBox.MouseClick += new MouseEventHandler(mouseEvents);
                            list.Add(pictureBox);
                        }
                        break;
                    case "System.Windows.Forms.ComboBox":
                        {
                            ComboBox comboBox = new ComboBox();
                            for (int e = 0; e < mass[i].Items.Count; e++)
                            {
                                comboBox.Items.Add(mass[i].Items[e]);
                            }
                            comboBox.BackColor = color;
                            comboBox.ForeColor = Color.White;
                            comboBox.SelectedItem = mass[i].Items[0];
                            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                            comboBox.Size = new Size(60, 20);
                            comboBox.Location = new Point(width + 2, 7);
                            comboBox.TabStop = false;
                            comboBox.MouseMove += new MouseEventHandler(mouseMove);
                            comboBox.MouseClick += new MouseEventHandler(mouseEvents);
                            
                            comboBox.KeyDown += (s, e) =>
                            {
                                e.Handled = true;
                            };
                            comboBox.KeyPress += (s, e) =>
                            {
                                e.Handled = true;
                            };
                            comboBox.KeyUp += (s, e) =>
                            {
                                e.Handled = true;
                            };
                            list.Add(comboBox);
                        }
                        break;
                }
            }
            Create(width, color);
            for (int i = 0; i < list.Count(); i++)
            {
                panel1.Controls.Add(list[i]);
                list[i].BringToFront();
            }
        }

        public void Create(int width, Color color)
        {
            panel1.Location = new Point(form.Width/2 - width/2,10);
            panel1.Size = new System.Drawing.Size(width, 36);
            panel1.BackColor = color;
            panel1.MouseClick += new MouseEventHandler(mouseEvents);
            panel1.MouseMove += new MouseEventHandler(mouseMove);
            form.MouseMove += new MouseEventHandler(mouseMove);
            SetRoundedShape(panel1, 10);

            panel2.Size = new System.Drawing.Size(84, 16);
            panel2.Location = new Point(panel1.Left + 20, panel1.Top + panel1.Height/3 + 13);
            panel2.BackColor = color;
            SetRoundedShape(panel2, 10);

            form.Controls.Add(panel2);
            panel2.BringToFront();
            form.Controls.Add(panel1);
            panel1.BringToFront();


        }
        #endregion

        #region Functions

        public void AllColision(GroupBlocks list)
        {
            Exit();
            if (clicked)
            {
                my_gb.Formation();
            }
            else
                panel2.BringToFront();
            for (int i = 0; i < list.blocks.Count(); i++)
            {
                if (Collision(this, list.blocks[i]))
                {
                    if (!clicked && !list.blocks[i].clicked && grouped == false && form == list.blocks[i].form
                        && panel1.Top > list.blocks[i].panel1.Top && panel1.Top - list.blocks[i].panel1.Top < 28)
                    {
                        
                        PlaceToAddBlock(list, i);
                    }
                }
            }
        }

        public void Destroy()
        {
            panel1.Dispose();
            panel2.Dispose();
        }

        public void Exit()
        {
            for (int i = 0; i < my_gb.blocks.Count(); i++)
            {
                if (my_gb.blocks[i].exit && i == my_gb.blocks.Count() - 1)
                {
                    Block block = my_gb.blocks[i];
                    my_gb.blocks.Remove(block);
                    my_gb = new GroupBlocks(my_gb.blocks);
                    block.my_gb = new GroupBlocks(block);
                    exit = false;
                }
                else
                {
                    if (my_gb.blocks[i].exit)
                    {
                        List<Block> NEWblocks = new List<Block>();
                        int finish = my_gb.blocks.Count();
                        for (int j = i; j < finish; j++)
                        {
                            Block block = my_gb.blocks[j];
                            NEWblocks.Add(block);
                        }
                        for (int n = i; n < finish; n++)
                        {
                            my_gb.blocks.Remove(my_gb.blocks[my_gb.blocks.Count() - 1]);
                        }
                        my_gb = new GroupBlocks(my_gb.blocks);
                        for (int k = 0; k < NEWblocks.Count(); k++)
                        {
                            NEWblocks[k].my_gb = new GroupBlocks(NEWblocks);
                        }
                        exit = false;
                    }
                }
            }
        }

        public void AddOneBlock(GroupBlocks list, int i)
        {
            panel1.MouseClick += second;
            for (int j = 0; j < this.list.Count(); j++)
            {
                if (this.list[j].GetType().ToString() == "System.Windows.Forms.Label")
                    ((Label)this.list[j]).MouseClick += second;
                if (this.list[j].GetType().ToString() == "System.Windows.Forms.PictureBox")
                    ((PictureBox)this.list[j]).MouseClick += second;
                if (this.list[j].GetType().ToString() == "System.Windows.Forms.ComboBox")
                    ((ComboBox)this.list[j]).MouseClick += second;
            }
            list.blocks.Insert(i + 1, this);
            my_gb = list;
            grouped = true;
            list.Formation();
        }

        public void AddGroupBlocks(GroupBlocks list)
        {
            for (int i = 0; i < my_gb.blocks.Count; i++)
            {
                my_gb.blocks[i].panel1.MouseClick += my_gb.blocks[i].second;
                for (int j = 0; j < my_gb.blocks[i].list.Count; j++)
                {
                    if (my_gb.blocks[i].list[j].GetType().ToString() == "System.Windows.Forms.Label")
                        ((Label)my_gb.blocks[i].list[j]).MouseClick += my_gb.blocks[i].second;
                    if (my_gb.blocks[i].list[j].GetType().ToString() == "System.Windows.Forms.PictureBox")
                        ((PictureBox)my_gb.blocks[i].list[j]).MouseClick += my_gb.blocks[i].second;
                    if (my_gb.blocks[i].list[j].GetType().ToString() == "System.Windows.Forms.ComboBox")
                        ((ComboBox)my_gb.blocks[i].list[j]).MouseClick += my_gb.blocks[i].second;
                }
                my_gb.blocks[i].grouped = false;
                list.Add(my_gb.blocks[i]);
                my_gb.blocks[i].grouped = true;
                list.Formation();
            }
            for (int i = 1; i < list.blocks.Count; i++)
            {
                list.blocks[i].my_gb = list;
            }
        }

        public void AddGroupBlocksNotToEnd(GroupBlocks list, int h)
        {
            for (int i = 0; i < my_gb.blocks.Count; i++)
            {
                h++;
                my_gb.blocks[i].panel1.MouseClick += my_gb.blocks[i].second;
                for (int j = 0; j < my_gb.blocks[i].list.Count; j++)
                {
                    if (my_gb.blocks[i].list[j].GetType().ToString() == "System.Windows.Forms.Label")
                        ((Label)my_gb.blocks[i].list[j]).MouseClick += my_gb.blocks[i].second;
                    if (my_gb.blocks[i].list[j].GetType().ToString() == "System.Windows.Forms.PictureBox")
                        ((PictureBox)my_gb.blocks[i].list[j]).MouseClick += my_gb.blocks[i].second;
                    if (my_gb.blocks[i].list[j].GetType().ToString() == "System.Windows.Forms.ComboBox")
                        ((ComboBox)my_gb.blocks[i].list[j]).MouseClick += my_gb.blocks[i].second;
                }
                my_gb.blocks[i].grouped = false;
                list.blocks.Insert(h, my_gb.blocks[i]);
                my_gb.blocks[i].grouped = true;
                list.Formation();
            }
            for (int i = 1; i < list.blocks.Count; i++)
            {
                list.blocks[i].my_gb = list;
            }
        }

        public void PlaceToAddBlock(GroupBlocks list, int i)
        {
            if (my_gb.blocks.Count() == 1)
            {
                AddOneBlock(list, i);
            }
            else
            {
                if (i != list.blocks.Count() - 1)
                {
                    AddGroupBlocksNotToEnd(list, i);
                }
                else
                {
                    AddGroupBlocks(list);
                }
            }
        }
        #endregion

        #region Events

        public void mouseMove(object senser, MouseEventArgs s)
        {
            if (clicked && my_gb.blocks.Count() >= 1)
            {
                panel1.Location = new Point(Cursor.Position.X - panel1.Width / 2 - form.Left, Cursor.Position.Y - 40 - form.Top);
                panel2.Location = new Point(panel1.Left + 20, panel1.Top + panel1.Height / 3 + 13);
                my_gb.Formation();
                panel1.BringToFront();
            }
        }

        public void mouseEvents(object senser, MouseEventArgs s)
        {
            clicked = !clicked;
            for (int i = 0; i < my_gb.blocks.Count(); i++)
            {
                my_gb.blocks[i].panel1.BringToFront();
                my_gb.blocks[i].panel2.BringToFront();
            }
            panel2.BringToFront();
        }

        public void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 59) && e.KeyChar != 8 && e.KeyChar != 45 && e.KeyChar != 44 && e.KeyChar != Convert.ToChar(Keys.Enter))
                e.Handled = true;
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                form.Focus();
            }
        }

        public void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                form.Focus();
            }
        }

        public void second(object senser, MouseEventArgs s)
        {
            for (int j = 0; j < this.list.Count(); j++)
            {
                if (this.list[j].GetType().ToString() == "System.Windows.Forms.Label")
                    ((Label)this.list[j]).MouseClick -= second;
                if (this.list[j].GetType().ToString() == "System.Windows.Forms.PictureBox")
                    ((PictureBox)this.list[j]).MouseClick -= second;
                if (this.list[j].GetType().ToString() == "System.Windows.Forms.ComboBox")
                    ((ComboBox)this.list[j]).MouseClick -= second;
            }
            panel1.BringToFront();
            panel2.BringToFront();
            panel1.MouseClick -= second;
            grouped = false;
            clicked = true;
            exit = true;
        }
        #endregion

        #region Private functions

        public bool Collision(Block b1, Block b2)
        {
            float diag1 = (float)Math.Sqrt(b1.panel1.Width * b1.panel1.Width + b1.panel1.Height * b1.panel1.Height);
            float diag2 = (float)Math.Sqrt(b2.panel1.Width * b2.panel1.Width + b2.panel1.Height * b2.panel1.Height);
            double R1 = diag1 / 2;
            double R2 = diag2 / 2;
            float Ox1 = b1.panel1.Left + b1.panel1.Width / 2, Oy1 = b1.panel1.Top + b1.panel1.Height / 2;
            float Ox2 = b2.panel1.Left + b2.panel1.Width / 2, Oy2 = b2.panel1.Top + b2.panel1.Height / 2;
            float d = (float)Math.Sqrt((Ox1 - Ox2) * (Ox1 - Ox2) + (Oy1 - Oy2) * (Oy1 - Oy2));
            if (d < R1 + R2)
                return true;
            else
            {
                return false;
            }

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
        #endregion
    }
}
