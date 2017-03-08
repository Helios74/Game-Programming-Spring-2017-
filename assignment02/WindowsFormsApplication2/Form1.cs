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
        public static int lwidth;
        public static int lheight;
        public static Sprite canvas = new Sprite();
        Background Ring = new Background(Properties.Resources.ring,0);
        Pic Tyson = new Pic(Properties.Resources.tyson1,0);
        Pic Kaiser = new Pic(Properties.Resources.kaiser,180);
        public static int fps = 60;

        public Form1()
        {
            lwidth = ClientSize.Width;
            lheight = ClientSize.Height;
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
            UpdateT = new Thread(new ThreadStart(move));
            UpdateT.Start();
            RenderT = new Thread(new ThreadStart(run));
            RenderT.Start();
        }

        public void UpdateSize()
        {
            Ring.Width = ClientSize.Width;
            Ring.Height = ClientSize.Height;
            Tyson.CornerX += (ClientSize.Width - lwidth);
            Tyson.CornerY += (ClientSize.Height - lheight);
            Kaiser.CornerX += (ClientSize.Width - lwidth);
            Kaiser.CornerY += (ClientSize.Height-lheight);
            Ring.Width = ClientSize.Width;
            Ring.Height = ClientSize.Height;
            Tyson.Width = ClientSize.Width / 8;
            Tyson.Height = ClientSize.Height / 8;
            Kaiser.Width = ClientSize.Width / 8;
            Kaiser.Height = ClientSize.Height / 8;
            lheight = ClientSize.Height;
            lwidth = ClientSize.Width;
        }

        protected override void OnResize(EventArgs e)
        {
            UpdateSize();
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
                    canvas.act(form.ClientSize.Width, form.ClientSize.Height);
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
