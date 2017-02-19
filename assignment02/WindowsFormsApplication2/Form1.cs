using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public static Form form;
        public static Thread UpdateT;
        public static Thread RenderT;
        public static List<Sprite> sprites = new List<Sprite>();
        public static Sprite canvas = new Sprite();
        Pic Ring = new Pic(Properties.Resources.ring,0);
        Pic Tyson = new Pic(Properties.Resources.tyson1,0);
        Pic Kaiser = new Pic(Properties.Resources.kaiser,180);
        public static int fps = 60;
        public static double runningFPS = 60.0;

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            form = this;
            Ring.CornerX = 0;
            Ring.CornerY = 0;
            Tyson.CornerX = ClientSize.Width / 2;
            Tyson.CornerY = ClientSize.Height / 4;
            Kaiser.CornerX = ClientSize.Width / 2;
            Kaiser.CornerY = 3 * ClientSize.Height / 4;
            Ring.Width = ClientSize.Width;
            Ring.Height = ClientSize.Height;
            Tyson.Width = ClientSize.Width / 8;
            Tyson.Height = ClientSize.Height / 8;
            Kaiser.Width = ClientSize.Width / 8;
            Kaiser.Height = ClientSize.Height / 8;
            canvas.add(Ring);
            canvas.add(Tyson);
            canvas.add(Kaiser);
            sprites.Add(Tyson);
            sprites.Add(Kaiser);
            UpdateT = new Thread(new ThreadStart(move));
            UpdateT.Start();
            RenderT = new Thread(new ThreadStart(run));
            RenderT.Start();
        }

        public void UpdateSize()
        {
            Ring.Width = ClientSize.Width;
            Ring.Height = ClientSize.Height;
            Tyson.CornerX = ClientSize.Width / 2;
            Tyson.CornerY = ClientSize.Height / 4;
            Kaiser.CornerX = ClientSize.Width / 2;
            Kaiser.CornerY = 3 * ClientSize.Height / 4;
            Ring.Width = ClientSize.Width;
            Ring.Height = ClientSize.Height;
            Tyson.Width = ClientSize.Width / 8;
            Tyson.Height = ClientSize.Height / 8;
            Kaiser.Width = ClientSize.Width / 8;
            Kaiser.Height = ClientSize.Height / 8;
        }

        protected override void OnResize(EventArgs e)
        {
            UpdateSize();
            Refresh();
        }

        public static void move()
        {
            while(true)
            {
                foreach (Pic k in sprites)
                {
                    k.act(form.ClientSize.Width, form.ClientSize.Height);
                }
            }
        }

        public void run()
        {
            while(true)
            {
                DateTime last = DateTime.Now;
                DateTime now = last;
                TimeSpan frameTime = new TimeSpan(10000000 / fps);
                while (true)
                {
                    DateTime tem = DateTime.Now;
                    runningFPS = 0.9 * runningFPS + 0.1 * (1000.0 / (tem - now).TotalMilliseconds);
                    now = tem;
                    TimeSpan diff = now - last;
                    if (diff.TotalMilliseconds < frameTime.TotalMilliseconds)
                    {
                        Thread.Sleep((frameTime - diff).Milliseconds);
                    }
                    last = DateTime.Now;
                    form.Invoke(new MethodInvoker(form.Refresh));
                }
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            UpdateT.Abort();
            RenderT.Abort();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            canvas.render(e.Graphics);
        }
    }
}
