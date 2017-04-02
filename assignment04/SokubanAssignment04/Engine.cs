using System;
using System.Threading;
using System.Windows.Forms;

namespace SokubanAssignment04
{
    public partial class Engine : Form
    {
        public static Engine engine;
        public static Thread UpdateT;
        public static Thread RenderT;
        public static Sprite canvas = new Sprite();
        public static int fps = 30;

        public Engine()
        {
            InitializeComponent();
            DoubleBuffered = true;
            engine = this;
            UpdateT = new Thread(new ThreadStart(move));
            UpdateT.Start();
            RenderT = new Thread(new ThreadStart(run));
            RenderT.Start();
        }

        protected override void OnResize(EventArgs e)
        {
            Refresh();
        }

        public static void move()
        {
            while (true)
            {
                DateTime last = DateTime.Now;
                DateTime now = last;
                TimeSpan frameTime = new TimeSpan(10000000 / fps);
                while (true)
                {
                    DateTime tem = DateTime.Now;
                    now = tem;
                    TimeSpan diff = now - last;
                    if (diff.TotalMilliseconds < frameTime.TotalMilliseconds)
                    {
                        Thread.Sleep((frameTime - diff).Milliseconds);
                    }
                    last = DateTime.Now;
                    canvas.act();
                }
            }
        }

        public void run()
        {
            while (true)
            {
                DateTime last = DateTime.Now;
                DateTime now = last;
                TimeSpan frameTime = new TimeSpan(10000000 / fps);
                while (true)
                {
                    DateTime tem = DateTime.Now;
                    now = tem;
                    TimeSpan diff = now - last;
                    if (diff.TotalMilliseconds < frameTime.TotalMilliseconds)
                    {
                        Thread.Sleep((frameTime - diff).Milliseconds);
                    }
                    last = DateTime.Now;
                    engine.Invoke(new MethodInvoker(engine.Refresh));
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
