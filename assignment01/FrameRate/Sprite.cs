using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameRate
{
    class Sprite
    {
        private float scale = 0;

        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        private float x = 0;

        public float X
        {
            get { return x; }
            set { x = value; }
        }

        private float y = 0;

        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        public float rot = 0;

        public float Rot
        {
            get { return rot; }
            set { rot = value; }
        }

        public float size = 0;

        public float Size
        {
            get { return size; }
            set { size = value; }
        }

        public Brush color = Brushes.Transparent;
        public Brush Color
        {
            get { return color; }
            set { color = value; }
        }


        public List<Sprite> children = new List<Sprite>();

        public void render(Graphics g)
        {
            Matrix original = g.Transform.Clone();
            g.TranslateTransform(x, y);
            g.RotateTransform(rot);
            paint(g);
            foreach (Sprite s in children)
            {
                s.render(g);
            }
            g.Transform = original;
        }

        public virtual void paint(Graphics g)//link between you and something you are drawing on
        {//virtual so you never have to fill it in
        }

        public void add(Sprite s)
        {
            children.Add(s);
        }
    }
}
