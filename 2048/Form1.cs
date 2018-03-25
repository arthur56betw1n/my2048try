using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2048
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Random random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
        public static int n = 4;
        Button[,] cell = new Button[n, n];
        int cellHeight = 90;
        int cellWidth = 90;
        int leftFrame = 20;
        int topFrame = 60;
        int netSize = 10;
        int num = 2;
        Button score = new Button();
        Button newgame = new Button();

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(187, 173, 160);

            for (int j = 0; j < n; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    cell[i, j] = new Button();
                    cell[i, j].Size = new Size(cellHeight, cellWidth);
                    cell[i, j].Text = "";
                    cell[i, j].Font = new Font("century gothic", 20, FontStyle.Bold);
                    cell[i, j].ForeColor = Color.FromArgb(119, 110, 101);
                    cell[i, j].FlatStyle = FlatStyle.Flat;
                    cell[i, j].FlatAppearance.BorderSize = 0;
                    cell[i, j].Left = leftFrame + i * (cell[i, j].Width + netSize);
                    cell[i, j].Top = topFrame + j * (cell[i, j].Height + netSize);

                    this.cell[i, j].PreviewKeyDown += new PreviewKeyDownEventHandler(this.Form1_PreviewKeyDown);
                    this.Controls.Add(cell[i, j]);
                }
            }
            this.ClientSize = new Size(n * (cellHeight + netSize) + 2 * leftFrame - netSize, n * (cellWidth + netSize) + 2 * topFrame - netSize);

            AddNum();
            AddNum();
            Painting();

            score.Left = leftFrame;
            score.Top = 10;
            score.Width = cellWidth * 2 + netSize;
            score.FlatStyle = FlatStyle.Flat;
            score.FlatAppearance.BorderSize = 0;
            score.BackColor = Color.FromArgb(151, 139, 128);
            score.Text = "0";
            score.AutoSize = true;
            score.Font = new Font("century gothic", 20, FontStyle.Bold);
            score.ForeColor = Color.White;
            this.score.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.Form1_PreviewKeyDown);
            this.Controls.Add(score);

            newgame.Left = leftFrame + cellWidth * 2 + 2 * netSize; ;
            newgame.Top = 10;
            newgame.Width = cellWidth * 2 + netSize;
            newgame.FlatStyle = FlatStyle.Flat;
            newgame.FlatAppearance.BorderSize = 0;
            newgame.BackColor = Color.FromArgb(151, 139, 128);
            newgame.Text = "new game";
            newgame.AutoSize = true;
            newgame.Font = new Font("century gothic", 20, FontStyle.Bold);
            newgame.ForeColor = Color.White;
            this.newgame.Click += new System.EventHandler(this.NewGame);
            this.newgame.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.Form1_PreviewKeyDown);
            this.Controls.Add(newgame);
        }

        private void Form1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            bool add = false;
            switch (e.KeyCode)
            {
                case Keys.Up:
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            for (int k = j + 1; k < n; k++)
                            {
                                if (cell[i, k].Text == "")
                                {
                                    continue;
                                }
                                else if (cell[i, k].Text == cell[i, j].Text)
                                {
                                    cell[i, j].Text = (Convert.ToInt32(cell[i, j].Text) * 2).ToString();
                                    score.Text = (Convert.ToInt32(score.Text) + Convert.ToInt32(cell[i, j].Text)).ToString();
                                    cell[i, k].Text = "";
                                    add = true;
                                    break;
                                }
                                else
                                {
                                    if (cell[i, j].Text == "" && cell[i, k].Text != "")
                                    {
                                        cell[i, j].Text = cell[i, k].Text;
                                        cell[i, k].Text = "";
                                        j--;
                                        add = true;
                                        break;
                                    }
                                    else if (cell[i, j].Text != "")
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    Painting();
                    break;
                case Keys.Down:
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = n - 1; j >= 0; j--)
                        {
                            for (int k = j - 1; k >= 0; k--)
                            {
                                if (cell[i, k].Text == "")
                                {
                                    continue;
                                }
                                else if (cell[i, k].Text == cell[i, j].Text)
                                {
                                    cell[i, j].Text = (Convert.ToInt32(cell[i, j].Text) * 2).ToString();
                                    score.Text = (Convert.ToInt32(score.Text) + Convert.ToInt32(cell[i, j].Text)).ToString();
                                    cell[i, k].Text = "";
                                    add = true;
                                    break;
                                }
                                else
                                {
                                    if (cell[i, j].Text == "" && cell[i, k].Text != "")
                                    {
                                        cell[i, j].Text = cell[i, k].Text;
                                        cell[i, k].Text = "";
                                        j++;
                                        add = true;
                                        break;
                                    }
                                    else if (cell[i, j].Text != "")
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    Painting();
                    break;
                case Keys.Right:
                    for (int j = 0; j < n; j++)
                    {
                        for (int i = n - 1; i >= 0; i--)
                        {
                            for (int k = i - 1; k >= 0; k--)
                            {
                                if (cell[k, j].Text == "")
                                {
                                    continue;
                                }
                                else if (cell[k, j].Text == cell[i, j].Text)
                                {
                                    cell[i, j].Text = (Convert.ToInt32(cell[i, j].Text) * 2).ToString();
                                    score.Text = (Convert.ToInt32(score.Text) + Convert.ToInt32(cell[i, j].Text)).ToString();
                                    cell[k, j].Text = "";
                                    add = true;
                                    break;
                                }
                                else
                                {
                                    if (cell[i, j].Text == "" && cell[k, j].Text != "")
                                    {
                                        cell[i, j].Text = cell[k, j].Text;
                                        cell[k, j].Text = "";
                                        i++;
                                        add = true;
                                        break;
                                    }
                                    else if (cell[i, j].Text != "")
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    Painting();
                    break;
                case Keys.Left:
                    for (int j = 0; j < n; j++)
                    {
                        for (int i = 0; i < n; i++)
                        {
                            for (int k = i + 1; k < n; k++)
                            {
                                if (cell[k, j].Text == "")
                                {
                                    continue;
                                }
                                else if (cell[k, j].Text == cell[i, j].Text)
                                {
                                    cell[i, j].Text = (Convert.ToInt32(cell[i, j].Text) * 2).ToString();
                                    score.Text = (Convert.ToInt32(score.Text) + Convert.ToInt32(cell[i, j].Text)).ToString();
                                    cell[k, j].Text = "";
                                    add = true;
                                    break;
                                }
                                else
                                {
                                    if (cell[i, j].Text == "" && cell[k, j].Text != "")
                                    {
                                        cell[i, j].Text = cell[k, j].Text;
                                        cell[k, j].Text = "";
                                        i--;
                                        add = true;
                                        break;
                                    }
                                    else if (cell[i, j].Text != "")
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    Painting();
                    break;
            }

            if (add)
            {
                AddNum();
            }

            GameOver();
        }

        private void AddNum()
        {
            Mark:
            int x = random.Next(0, n);
            int y = random.Next(0, n);

            if (cell[x, y].Text == "")
            {
                cell[x, y].Text = num.ToString();
                cell[x, y].Focus();
                cell[x, y].BackColor = Color.White;
            }
            else
            {
                goto Mark;
            }
        }

        private void NewGame(object sender, EventArgs e)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    cell[i, j].Text = "";
                }
            }
            score.Text = "0";
            AddNum();
            AddNum();
            Painting();
        }

        private void GameOver()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i - 1 >= 0)
                    {
                        if (cell[i - 1, j].Text == cell[i, j].Text)
                        {
                            return;
                        }
                    }

                    if (i + 1 < n)
                    {
                        if (cell[i + 1, j].Text == cell[i, j].Text)
                        {
                            return;
                        }
                    }

                    if (j - 1 >= 0)
                    {
                        if (cell[i, j - 1].Text == cell[i, j].Text)
                        {
                            return;
                        }
                    }

                    if (j + 1 < n)
                    {
                        if (cell[i, j + 1].Text == cell[i, j].Text)
                        {
                            return;
                        }
                    }

                    if (cell[i, j].Text == "")
                    {
                        return;
                    }
                }
            }

            MessageBox.Show("Game over! \nYou score: " + score.Text);
        }

        private void Painting()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    string s = cell[i, j].Text;
                    switch (s)
                    {
                        case "2":
                            cell[i, j].BackColor = Color.FromArgb(238, 228, 218);
                            cell[i, j].ForeColor = Color.FromArgb(119, 110, 101);
                            break;
                        case "4":
                            cell[i, j].BackColor = Color.FromArgb(237, 224, 200);
                            cell[i, j].ForeColor = Color.FromArgb(119, 110, 101);
                            break;
                        case "8":
                            cell[i, j].BackColor = Color.FromArgb(242, 177, 121);
                            cell[i, j].ForeColor = Color.White;
                            break;
                        case "16":
                            cell[i, j].BackColor = Color.FromArgb(245, 149, 99);
                            cell[i, j].ForeColor = Color.White;
                            break;
                        case "32":
                            cell[i, j].BackColor = Color.FromArgb(246, 124, 95);
                            cell[i, j].ForeColor = Color.White;
                            break;
                        case "64":
                            cell[i, j].BackColor = Color.FromArgb(246, 94, 59);
                            cell[i, j].ForeColor = Color.White;
                            break;
                        case "128":
                            cell[i, j].BackColor = Color.FromArgb(237, 207, 114);
                            cell[i, j].ForeColor = Color.White;
                            break;
                        case "256":
                            cell[i, j].BackColor = Color.FromArgb(237, 204, 97);
                            cell[i, j].ForeColor = Color.White;
                            break;
                        case "512":
                            cell[i, j].BackColor = Color.FromArgb(237, 200, 80);
                            cell[i, j].ForeColor = Color.White;
                            break;
                        case "1024":
                            cell[i, j].BackColor = Color.FromArgb(236, 196, 64);
                            cell[i, j].ForeColor = Color.White;
                            break;
                        case "2048":
                            cell[i, j].BackColor = Color.FromArgb(237, 193, 47);
                            cell[i, j].ForeColor = Color.White;
                            break;
                        case "":
                            cell[i, j].BackColor = Color.FromArgb(205, 193, 180);
                            cell[i, j].ForeColor = Color.FromArgb(119, 110, 101);
                            break;
                    }
                }
            }
        }
    }
}
