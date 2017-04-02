using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SokubanAssignment04
{
    public class Program:Engine
    {
        public static string map = Properties.Resources.SokubanMaps;
        public static SlideSprite character;
        public static SlideSprite[,] goals;
        public static SlideSprite[,] walls;
        public static SlideSprite[,] blocks;
        public static int x;
        public static int y;
        
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                if (canMoveTo(x + 1, y, 1, 0)) x++;
                if (blocks[x, y] != null) moveBlock(x, y, 1, 0);
            }
            if (e.KeyCode == Keys.Left)
            {
                if (canMoveTo(x - 1, y, -1, 0)) x--;
                if (blocks[x, y] != null) moveBlock(x, y, -1, 0);
            }
            if (e.KeyCode == Keys.Up)
            {
                if (canMoveTo(x, y - 1, 0, -1)) y--;
                if (blocks[x, y] != null) moveBlock(x, y, 0, -1);
            }
            if (e.KeyCode == Keys.Down)
            {
                if (canMoveTo(x, y + 1, 0, 1)) y++;
                if (blocks[x, y] != null) moveBlock(x, y, 0, 1);
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
        /*
        
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //fixScale();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            fixScale();
        }

        private void fixScale()
        {
            canvas.Scale = Math.Min(ClientSize.Width, ClientSize.Height) / (Width*1.0F);
            //more code here
        }
        */

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            String[] lines = map.Split('\n');
            int width = 5;
            int height = 5;
            /*
            string[] All = map.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            List<string> Lines = new List<string>();
            Boolean start = false;
            foreach(string line in All)
            { 
                if (start == true && line.Equals("Done") == false) Lines.Add(line);
                if (line.Equals("Level")) start = true;
                else if (line.Equals("Done")) start = false;
            }
            String[] lines = Lines.ToArray();
            int width = lines[0].Length;
            int height = lines.Length;
            */
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
            Application.Run(new Program());
        }
    }
}
