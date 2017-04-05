using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SokubanAssignment04
{
    public class Program : Engine
    {
        public static string map = Properties.Resources.SokubanMaps;
        public static string[][] levels;
        public static int moves = 0;
        public static SlideSprite character;
        public static SlideSprite[,] goals;
        public static SlideSprite[,] walls;
        public static SlideSprite[,] blocks;
        public static SlideSprite winscreen = new SlideSprite(Properties.Resources.WinScreen, 0, 0);
        public static TextSprite movecount;
        public static TextSprite movetitle;
        public static TextSprite record;
        public static TextSprite recordtitle;
        public static int[] Widths;
        public static int[] Heights;
        public static int[] records;
        public static int wid;
        public static int hei;
        public static int x;
        public static int y;
        public static int onlevel = 0;
        public static bool win = false;

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                if (canMoveTo(x + 1, y, 1, 0)) x++;
                if (blocks[x, y] != null) moveBlock(x, y, 1, 0);
                moves += 1;
                movecount.Text = moves.ToString();
            }
            if (e.KeyCode == Keys.Left)
            {
                if (canMoveTo(x - 1, y, -1, 0)) x--;
                if (blocks[x, y] != null) moveBlock(x, y, -1, 0);
                moves += 1;
                movecount.Text = moves.ToString();
            }
            if (e.KeyCode == Keys.Up)
            {
                if (canMoveTo(x, y - 1, 0, -1)) y--;
                if (blocks[x, y] != null) moveBlock(x, y, 0, -1);
                moves += 1;
                movecount.Text = moves.ToString();
            }
            if (e.KeyCode == Keys.Down)
            {
                if (canMoveTo(x, y + 1, 0, 1)) y++;
                if (blocks[x, y] != null) moveBlock(x, y, 0, 1);
                moves += 1;
                movecount.Text = moves.ToString();
            }
            if (e.KeyCode == Keys.Enter && win == true)
            {
                Program.canvas.children.Clear();
                int val = onlevel % levels.Length;
                display(levels[val], Widths[val], Heights[val]);
                fixScale();
                moves = 0;
                movecount.Text = moves.ToString();
            }
            if (checkWin())
            {
                winscreen.Width = wid*100.0F;
                winscreen.Height = hei*100.0F;
                Program.canvas.add(winscreen);
                if(moves < records[onlevel%levels.Length])
                {
                    records[onlevel % levels.Length] = moves;
                }
                win = true;
                onlevel += 1;
            }
            character.TargetX = x * 100;
            character.TargetY = y * 100;
        }

        public void moveBlock(int i, int j, int dx, int dy)
        {
            blocks[i + dx, j + dy] = blocks[i, j];
            blocks[i, j] = null;
            blocks[i + dx, j + dy].TargetX = (i + dx) * 100;
            blocks[i + dx, j + dy].TargetY = (j + dy) * 100;
            if (goals[i + dx, j + dy] != null) blocks[i + dx, j + dy].Image = Properties.Resources.Win;
            else blocks[i + dx, j + dy].Image = Properties.Resources.PushBlock;
        }

        public Boolean canMoveTo(int i, int j, int dx, int dy)
        {
            if (walls[i, j] == null && blocks[i, j] == null) return true;
            if (walls[i, j] != null) return false;
            if (blocks[i, j] != null && blocks[i + dx, j + dy] == null && walls[i + dx, j + dy] == null) return true;
            return false;
        }

        public Boolean checkWin()
        {
            int width = goals.GetLength(0);
            int height = goals.GetLength(1);
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    if (goals[i, j] != null && blocks[i, j] == null)
                        return false;
                }
            }
            return true;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            fixScale();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            fixScale();
        }

        private void fixScale()
        {
            canvas.Scale = Math.Min(ClientSize.Width, ClientSize.Height) / (Math.Max(wid, hei) * 100.0F);
            canvas.X = (ClientSize.Width / 2) - (wid * 100.0F * canvas.Scale / 2);
            canvas.Y = (ClientSize.Height / 2) - (hei * 100.0F * canvas.Scale / 2);
        }

        public static void readin()
        {
            String[] all = map.Split(new string[] { "B" }, StringSplitOptions.RemoveEmptyEntries);
            int numlevels = all.Length;
            levels = new String[numlevels][];
            Widths = new int[numlevels];
            Heights = new int[numlevels];
            records = new int[numlevels];
            for (int i = 0; i < numlevels; i++)
            {
                String[] lines = all[i].Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                Widths[i] = lines[0].Length;
                Heights[i] = lines.Length;
                levels[i] = lines;
                records[i] = 1000;
            }
        }

        public static void display(string[] lines, int width, int height)
        {
            wid = width;
            hei = height;
            goals = new SlideSprite[width, height];
            walls = new SlideSprite[width, height];
            blocks = new SlideSprite[width, height];
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    if (lines[j][i] == 'g' || lines[j][i] == 'B')
                    {
                        goals[i, j] = new SlideSprite(Properties.Resources.GoalBlock, i * 100, j * 100);
                        Program.canvas.add(goals[i, j]);
                    }

                    if (lines[j][i] == 'w')
                    {
                        walls[i, j] = new SlideSprite(Properties.Resources.Wall, i * 100, j * 100);
                        Program.canvas.add(walls[i, j]);
                    }

                    if (lines[j][i] == 'b' || lines[j][i] == 'B')
                    {
                        blocks[i, j] = new SlideSprite(Properties.Resources.PushBlock, i * 100, j * 100);
                        if (lines[j][i] == 'B') blocks[i, j].Image = Properties.Resources.Win;
                    }
                    if (lines[j][i] == 'c')
                    {
                        character = new SlideSprite(Properties.Resources.person, i * 100, j * 100);
                        x = i;
                        y = j;
                    }
                }

            }
            for (int j = 0; j < height; j++)
                for (int i = 0; i < width; i++)
                {
                    if (blocks[i, j] != null) Program.canvas.add(blocks[i, j]);
                }
            Program.canvas.add(character);
            movecount = new TextSprite(moves.ToString(), 25, 25);
            Program.canvas.add(movecount);
            movetitle = new TextSprite("Moves", 25, 15);
            Program.canvas.add(movetitle);
            recordtitle = new TextSprite("Record", 60, 15);
            Program.canvas.add(recordtitle);
            if (records[onlevel%levels.Length] != 1000)
            {
                record = new TextSprite(records[onlevel%levels.Length].ToString(), 60, 25);
                Program.canvas.add(record);
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            readin();
            display(levels[onlevel], Widths[onlevel], Heights[onlevel]);
            Application.Run(new Program());
           
        }
    }
}
