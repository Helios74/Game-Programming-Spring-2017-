using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrameRate
{ 
    public partial class Form1 : Form
    {
        public static Form form;
        public static Thread thread;
        public static Thread thread2;
        public static int s = 100;
        public static int fps = 60;
        public int rotspeed = 10;
        public static double runningFPS = 30.0;
        Sprite main = new Sprite();
        Sprite corner = new Sprite();

        public Form1()
        {
            main.X = ClientSize.Width/2;
            main.Y = ClientSize.Height/2; 
            SpinRect corn = new SpinRect();
            corn.size = 40;
            corn.X = 20;
            corn.Y = 20;
            corn.color = Brushes.Black;
            corner.add(corn);
            InitializeComponent();
            DoubleBuffered = true;
            form = this;
            thread2 = new Thread(new ThreadStart(count));
            thread2.Start();
            thread = new Thread(new ThreadStart(run));
            thread.Start();
        }

        public static void count()
        {
            while (true)
            {
                s++;
                Thread.Sleep(50);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            main.X = ClientSize.Width / 2;
            main.Y = ClientSize.Height / 2;
            Refresh();
        }

        public static void run()
        {
            DateTime last = DateTime.Now;
            DateTime now = last;
            TimeSpan frameTime = new TimeSpan(10000000 / fps);
            while(true)
            {
                DateTime tem = DateTime.Now;
                runningFPS = 0.9 * runningFPS + 0.1 * (1000.0 / (tem - now).TotalMilliseconds);
                now = tem;
                TimeSpan diff = now - last;
                if(diff.TotalMilliseconds < frameTime.TotalMilliseconds)
                {
                    Thread.Sleep((frameTime - diff).Milliseconds);
                }
                last = DateTime.Now;
                form.Invoke(new MethodInvoker(form.Refresh));
            }
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Random rnd = new Random();
            if (keyData == Keys.Up)
            {
                Type brushesType = typeof(Brushes);
                PropertyInfo[] properties = brushesType.GetProperties();
                int random = rnd.Next(properties.Length);
                Brush result = (Brush)properties[random].GetValue(null, null);
                int xplace = rnd.Next(-ClientSize.Width, ClientSize.Width);
                int yplace = rnd.Next(-ClientSize.Height, ClientSize.Height);
                int size = rnd.Next(50, 200);
                SpinRect n = new SpinRect();
                n.size = size;
                n.X = xplace;
                n.Y = yplace;
                n.color = result;
                main.add(n);
                return true;
            }
            else if(keyData == Keys.Right)
            {
                rotspeed += 10;
                return true;
            }
            else if(keyData == Keys.Left)
            {
                rotspeed -= 10;
                return true;
            }
            else if (keyData == Keys.Down)
            {
                main.children.RemoveAt(0);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            thread.Abort();
            thread2.Abort();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            foreach(SpinRect j in main.children)
            {
                j.rot = 10*s;
            }
            foreach(SpinRect k in corner.children)
            {
                k.rot = rotspeed*s;
            }
            Console.WriteLine(s);
            main.rot = rotspeed * s;
            main.render(e.Graphics);
            corner.rot = -rotspeed * s;
            corner.render(e.Graphics);
            System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 16);
            System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
            e.Graphics.DrawString(runningFPS.ToString(), drawFont, drawBrush, ClientSize.Width-80, 20, drawFormat);

        }
    }
}
