using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SokubanAssignment04
{
    public class SlideSprite:Sprite
    {
        public float distx = 0;
        public float disty = 0;
        public SlideSprite(Image image, int newx, int newy)
        {
            this.Image = image;
            this.X = (float)newx;
            this.Y = (float)newy;
            this.TargetX = (float)newx;
            this.TargetY = (float)newy;
            this.Width = 100;
            this.Height = 100;
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

        private float velocity = 30;

        public float Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public override void act()
        {
            float dx = TargetX - X;
            if (distx == 0) distx = dx;
            float dy = TargetY - Y;
            if (disty == 0) disty = dy;
            float xchange = Math.Abs(dx / distx);
            float ychange = Math.Abs(dy / disty);
            if (Math.Abs(dx) > Velocity) X += Math.Sign(dx) * (xchange * velocity);
            else
            {
                X = TargetX;
                distx = 0;
            }
            if (Math.Abs(dy) > Velocity) Y += Math.Sign(dy) * (ychange * velocity);
            else
            {
                Y = TargetY;
                disty = 0;
            }
        }

        public override void paint(Graphics g)
        {
            g.DrawImage(this.Image, new Rectangle(0, 0, (int)Width, (int)Height));
        }
    }
}