using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment05
{
    public class CollideSprite : Sprite
    {//come up with a better solution than this
        private static List<CollideSprite> sprites = new List<CollideSprite>();

        int mask = 1;
        public int Mask
        {
            get { return mask; }
            set { mask = value; }
        }
        //Define a destructor to remove both collide and canvas element
        public CollideSprite(Image image, int x, int y)
        {
            this.Image = image;
            sprites.Add(this);
            X = (float)x;
            Y = (float)y;
            TargetX = (float)x;
            TargetY = (float)y;
            this.Height = 100;
            this.Width = 100;
        }

        public CollideSprite(Image image)
        {
            sprites.Add(this);
            this.Image = image;
        }

        public override void Kill()
        {
            base.Kill();
            sprites.Remove(this);//still a concurrent modification problem
        }

        public List<CollideSprite> getCollisions()
        {
            List<CollideSprite> output = new List<CollideSprite>();
            double cx = X + Scale * Width / 2;
            double cy = Y + Scale * Height / 2;
            double rx = Scale * Width / 2;
            double ry = Scale * Height / 2;
            foreach (CollideSprite k in sprites)
            {
                if (k == this) continue;
                if ((mask & k.mask) == 0) continue;
                double r2x = k.Scale * k.Width / 2;
                double r2y = k.Scale * k.Height / 2;
                double c2x = k.X + r2x;
                double c2y = k.Y + r2y;
                if (Math.Abs(cx - c2x) >= rx + r2x) continue;
                if (Math.Abs(cy - c2y) >= ry + r2y) continue;
                output.Add(k);
            }
            return output;
        }

        private float targetx;

        public float TargetX
        {
            get { return targetx; }
            set { targetx = value; }
        }

        private float targety;

        public float TargetY
        {
            get { return targety; }
            set { targety = value; }
        }

        public override void act()
        {
          
        }

        public override void paint(Graphics g)
        {
            g.DrawImage(this.Image, new Rectangle(0, 0, (int)Width, (int)Height));
        }
    }
}
