using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace assignment05
{
    public class Program: Engine
    {
        public static Character main;
        public static Finish flag;
        bool right, left, up = false;
        public static int[] Widths;
        public static int[] Heights;
        public static string[][] levels;
        public static string map = Properties.Resources.levels2;
        public static SlideSprite WinScreen = new SlideSprite(Properties.Resources.WinScreen,0,0);
        public static int wid;
        public static int hei;
        public static PhysicsSprite[,] walls;
        public static Star[,] stars;
        public static Enemy[,] enemies;
        public static int onlevel = 0;
        public static TextSprite starcount;
        public static float initialX;
        public bool win = false;
        public static SlideSprite background = new SlideSprite(Properties.Resources.background,0,0);

        public bool finmap(Character c)
        {
            c.X += c.Vx;
            c.Y += c.Vy;
            List<CollideSprite> list = c.getCollisions();
            c.X -= c.Vx;
            c.Y -= c.Vy;
            foreach (CollideSprite s in list)
            {
                if (s.GetType() == typeof(Finish))
                {
                    s.Kill();
                    return true;
                }
            }
            return false;
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
            canvas.Scale = Math.Min(ClientSize.Width, ClientSize.Height) / (Math.Max(hei * 100.0F, wid * 100.0F));
            canvas.X = (ClientSize.Width / 2) - (wid* 100.0F * canvas.Scale / 2);
            canvas.Y = (ClientSize.Height / 2) - (hei * 100.0F * canvas.Scale / 2);
            background.Height = ClientSize.Height;
            background.Width = ClientSize.Width;
        }

        public void updateVelocities(PhysicsSprite k)
        {
            if (left && !right) k.Vx = -10;
            else if (right && !left) k.Vx = 10;
            else k.Vx = 0;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                right = true;
            }
            if (e.KeyCode == Keys.Left)
            {
                left = true;
            }
            if (e.KeyCode == Keys.Up)
            { 
                if (!up && main.onGround())
                {
                    main.Vy = -20;
                }
                up = true;
            }
            if (e.KeyCode == Keys.Space)
            {
                main.Shoot();
            }
            if(finmap(main) == true)
            {
                WinScreen.Width = this.ClientSize.Width;
                WinScreen.Height = this.ClientSize.Height;
                Program.canvas.add(WinScreen);
                win = true;
                onlevel += 1;
            }
            if (e.KeyCode == Keys.Enter && win == true)
            {
                Program.canvas.children.Clear();
                int val = onlevel % levels.Length;
                display(levels[val], Widths[val], Heights[val]);
                fixScale();
                main.points = 0;
            }
            updateVelocities(main);
            if (starcount.Text != main.points.ToString())
            {
                starcount.Text = main.points.ToString();
                Console.WriteLine("Getting");
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                right = false;
            }
            if (e.KeyCode == Keys.Left)
            {
                left = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                up = false;
            }
            updateVelocities(main);
        }

        public static void readin()
        {
            String[] all = map.Split(new string[] { "B" }, StringSplitOptions.RemoveEmptyEntries);
            int numlevels = all.Length;
            levels = new String[numlevels][];
            Widths = new int[numlevels];
            Heights = new int[numlevels];
            for (int i = 0; i < numlevels; i++)
            {
                String[] lines = all[i].Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                Widths[i] = lines[0].Length;
                Heights[i] = lines.Length;
                levels[i] = lines;
            }
        }

        public static void display(string[] lines, int width, int height)
        {
            Program.canvas.add(background);
            wid = width;
            hei = height;
            stars = new Star[width, height];
            walls = new PhysicsSprite[width, height];
            enemies = new Enemy[width, height];
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    if (lines[j][i] == 'e')
                    {
                        enemies[i, j] = new Enemy(i * 100, j * 100);
                        Program.canvas.add(enemies[i, j]);
                    }

                    if (lines[j][i] == 'w')
                    {
                        walls[i, j] = new PhysicsSprite(Properties.Resources.jumpblock, i * 100, j * 100);
                        walls[i, j].Motion = PhysicsSprite.MotionModel.Static;
                        Program.canvas.add(walls[i, j]);
                    }

                    if (lines[j][i] == 's')
                    {
                        stars[i, j] = new Star(i * 100, j * 100);
                        stars[i, j].Motion = PhysicsSprite.MotionModel.Static;
                        Program.canvas.add(stars[i, j]);
                    }
                    if (lines[j][i] == 'c')
                    {
                        main = new Character(i * 100, j * 100);
                        initialX =  i * 100.0F*Program.canvas.Scale;
                        main.Mask = 15;
                    }
                    if(lines[j][i] == 'f')
                    { 
                        flag = new Finish(i * 100, j * 100);
                        flag.Motion = PhysicsSprite.MotionModel.Static;
                    }
                }

            }
            Program.canvas.add(main);
            Program.canvas.add(flag);
            starcount = new TextSprite(main.points.ToString(), 25, 25);
            Program.canvas.add(starcount);
        }

        [STAThread]
        static void Main()
        {
            Program.canvas.add(background);
            readin();
            display(levels[onlevel], Widths[onlevel], Heights[onlevel]);
            Application.Run(new Program());
        }
    }
}
