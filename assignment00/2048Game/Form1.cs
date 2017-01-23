using System;
using System.Drawing;
using System.Windows.Forms;

namespace _2048Game
{
    public partial class Form1 : Form
    {
        int[,] board = new int[4, 4];
        int x;
        int y;
        int cellsize;
        int margin = 10;

        public Form1()
        {
            KeyPreview = true;
            InitializeComponent();
            DoubleBuffered = true;
            genrandom(2);
            UpdateSize();
        }

        public bool checkwin()
        {
            for(int j = 0; j < 4; j++)
            {
                for(int i = 0; i < 4; i++)
                {
                    if(board[i,j] == 2048)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool checklose()
        {
            bool full = checkFull();
            if(full == true)
            {
                for(int j = 1; j < 3; j ++)
                {
                    for(int i = 1; i < 3; i++)
                    {
                        if (board[i, j] == board[i + 1, j]) return false;
                        if (board[i, j] == board[i - 1, j]) return false;
                        if (board[i, j] == board[i, j + 1]) return false;
                        if (board[i, j] == board[i, j - 1]) return false;
                    }
                }
                for( int i = 1; i < 3; i++)
                {
                    if (board[0, i] == board[1, i]) return false;
                    if (board[0, i] == board[0, i + 1]) return false;
                    if (board[0, i] == board[0, i - 1]) return false;

                    if (board[3, i] == board[2, i]) return false;
                    if (board[3, i] == board[3, i + 1]) return false;
                    if (board[3, i] == board[3, i - 1]) return false;

                    if (board[i, 0] == board[i, 1]) return false;
                    if (board[i, 0] == board[i+1, 0]) return false;
                    if (board[i, 0] == board[i-1, 0]) return false;

                    if (board[i, 3] == board[i, 2]) return false;
                    if (board[i, 3] == board[i+1, 3]) return false;
                    if (board[i, 3] == board[i-1, 3]) return false;
                }
                if (board[0, 0] == board[1, 0]) return false;
                if (board[0, 0] == board[0, 1]) return false;

                if (board[0, 3] == board[1, 3]) return false;
                if (board[0, 3] == board[0, 2]) return false;

                if (board[3, 0] == board[2, 0]) return false;
                if (board[3, 0] == board[3, 1]) return false;

                if (board[3, 3] == board[3, 2]) return false;
                if (board[3, 3] == board[2, 3]) return false;

                return true;
            }
            return false;
        }

        public bool checkFull()
        {
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    if(board[i,j] == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void genrandom(int num)
        {
            Random rnd = new Random();
            bool full = checkFull();
            int tofind = num;
            int tox = 0;
            int toy = 0;
            if(full == true)
            {
                return;
            }
            while(tofind > 0)
            {
                tox = rnd.Next(0, 4);
                toy = rnd.Next(0, 4);
                if (board[tox, toy] == 0)
                {
                    board[tox, toy] = rnd.Next(1, 2) * 2;
                    tofind -= 1;
                }
            }
            
        }

        public void combine(int direction)
        {
            if (direction == 0)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int i = 3; i > 0; i--)
                    {
                        if (board[i, j] == board[i - 1, j])
                        {
                            board[i, j] = board[i, j] + board[i - 1, j];
                            board[i-1, j] = 0;
                            i -= 1;
                        }
                    }
                }
            }
            else if (direction == 1)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (board[i, j] == board[i + 1, j])
                        { 
                            board[i, j] = board[i, j] + board[i + 1, j];
                            board[i + 1, j] = 0;
                            i += 1;
                        }
                    }
                }
            }
            else if (direction == 2)
            {
                for(int i = 0; i < 4; i++)
                {
                    for(int j = 0; j < 3 ; j++)
                    {
                        if(board[i,j] == board[i,j+1])
                        {
                            board[i, j] = board[i, j] + board[i, j + 1];
                            board[i, j + 1] = 0;
                            j += 1;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 2; j > -1; j--)
                    {
                        if (board[i, j + 1] == board[i, j] && board[i,j] != 0)
                        {
                            board[i, j+1] = board[i, j] + board[i, j + 1];
                            board[i, j] = 0;
                            j -= 1;
                        }
                    }
                }
            }
        }

        public int nearest(int direc, int column, int row)
        {
            if(direc == 0)
            {
                for(int i = 3; i > column; i--)
                {
                    if (board[i, row] == 0)
                    {
                        return i;
                    }
                    else { }
                }
                return column;
            }
            else if(direc == 1)
            {
                for (int i = 0; i < column; i++)
                {
                    if (board[i, row] == 0)
                    {
                        return i;
                    }
                    else { }
                }
                return column;
            }
            else if(direc == 2)
            {
                for(int j = 0; j < row; j++)
                {
                    if (board[column, j] == 0)
                    {
                        return j;
                    }
                    else { }
                }
                return row;
            }
            else
            {
                for (int j = 3; j > row; j--)
                {
                    if (board[column, j] == 0)
                    {
                        return j;
                    }
                    else { }
                }
                return row;
            }
        }

        public void downLoop()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 3; j > -1; j--)
                {
                    if (board[i, j] != 0)
                    {
                        y = nearest(3, i, j);
                        if (y == j) { }
                        else
                        {
                            board[i, y] = board[i, j];
                            board[i, j] = 0;
                        }

                    }
                }
            }
        }

        public void rightLoop()
        {
            for (int j = 3; j > -1; j--)
            {
                for (int i = 3; i > -1; i--)
                {
                    if (board[i, j] != 0)
                    {
                        x = nearest(0, i, j);
                        if (x == i)
                        { }
                        else
                        {
                            board[x, j] = board[i, j];
                            board[i, j] = 0;
                        }
                    }
                }
            }
        }

        public void leftLoop()
        {
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (board[i, j] != 0)
                    {
                        x = nearest(1, i, j);
                        if (x == i)
                        { }
                        else
                        {
                            board[x, j] = board[i, j];
                            board[i, j] = 0;
                        }
                    }
                }
            }
        }

        public void uploop()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (board[i, j] != 0)
                    {
                        y = nearest(2, i, j);
                        if (y == j)
                        { }
                        else
                        {
                            board[i, y] = board[i, j];
                            board[i, j] = 0;
                        }
                    }
                }
            }
        }

        public void reset()
        {
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    board[i, j] = 0;
                }
            }
            genrandom(2);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Right)
            {
                rightLoop();
                combine(0);
                rightLoop();
                genrandom(1);
                Refresh();
                bool won = checkwin();
                bool lost = checklose();
                if(won == true)
                {
                    MessageBox.Show("You Won");
                    reset();
                }
                if(lost == true)
                {
                    MessageBox.Show("You lost");
                    reset();
                }
                Refresh();
                return true;
            }
            else if (keyData == Keys.Left)
            {
                leftLoop();
                combine(1);
                leftLoop();
                genrandom(1);
                Refresh();
                bool won = checkwin();
                bool lost = checklose();
                if(won == true)
                {
                    MessageBox.Show("You Won");
                }
                if (lost == true)
                {
                    MessageBox.Show("You lost");
                    reset();
                }
                Refresh();
                return true;
            }
            else if (keyData == Keys.Up)
            {
                uploop();
                combine(2);
                uploop();
                genrandom(1);
                Refresh();
                bool won = checkwin();
                bool lost = checklose();
                if(won == true)
                {
                    MessageBox.Show("You Won");
                    reset();
                }
                if (lost == true)
                {
                    MessageBox.Show("You lost");
                    reset();
                }
                Refresh();
                return true;
            }
            else if (keyData == Keys.Down)
            {
                downLoop();
                combine(3);
                downLoop();
                genrandom(1);
                Refresh();
                bool won = checkwin();
                bool lost = checklose();
                if(won == true)
                {
                    MessageBox.Show("You Won");
                    reset();
                }
                if (lost == true)
                {
                    MessageBox.Show("You lost");
                    reset();
                }
                Refresh();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void UpdateSize()
        {
            cellsize = (Math.Min(ClientSize.Width, ClientSize.Height) - 2 * margin) / 4;
            if (ClientSize.Width > ClientSize.Height)
            {
                x = (ClientSize.Width - 4 * cellsize) / 2;
                y = margin;
            }
            else
            {
                x = margin;
                y = (ClientSize.Height - 4 * cellsize) / 2;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateSize();
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)//renders state from given variables
        {
            UpdateSize();
            //base.onpaint(e) will draw buttons, before means it will be on top, after it will be under
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    System.Drawing.Font font = new System.Drawing.Font("Ubuntu", cellsize / 2 * 72 / 96);
                    Rectangle rect = new Rectangle(x + i * cellsize, y + j * cellsize, cellsize, cellsize);
                    if (board[i,j]!=0)
                    {
                        if(board[i,j] == 2)
                        {
                            e.Graphics.FillRectangle(Brushes.BurlyWood, rect);
                        }
                        else if(board[i,j] == 4)
                        {
                            e.Graphics.FillRectangle(Brushes.Brown, rect);
                        }
                        else if(board[i,j] == 8)
                        {
                            e.Graphics.FillRectangle(Brushes.Gold, rect);
                        }
                        else if(board[i,j] == 16)
                        {
                            e.Graphics.FillRectangle(Brushes.Gray, rect);
                        }
                        else if(board[i,j] == 32)
                        {
                            e.Graphics.FillRectangle(Brushes.Green, rect);
                        }
                        else if(board[i,j] == 64)
                        {
                            e.Graphics.FillRectangle(Brushes.GreenYellow, rect);
                        }
                        else if (board[i, j] == 128)
                        {
                            e.Graphics.FillRectangle(Brushes.Khaki, rect);
                        }
                        else if (board[i, j] == 256)
                        {
                            e.Graphics.FillRectangle(Brushes.LightSeaGreen, rect);
                        }
                        else if (board[i, j] == 512)
                        {
                            e.Graphics.FillRectangle(Brushes.LimeGreen , rect);
                        }
                        else if (board[i, j] == 1024)
                        {
                            e.Graphics.FillRectangle(Brushes.Moccasin, rect);
                        }
                        else if (board[i, j] == 2048)
                        {
                            e.Graphics.FillRectangle(Brushes.Plum, rect);
                        }
                        e.Graphics.DrawString(board[i, j].ToString(), font, Brushes.Black, x + i * cellsize, y + j * cellsize);
                    }
                    e.Graphics.DrawRectangle(Pens.Chocolate, rect);
                }

            }
        }
    }

}
