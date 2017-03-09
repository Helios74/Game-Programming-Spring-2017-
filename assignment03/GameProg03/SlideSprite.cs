using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProg03
{
    public class SlideSprite:Sprite
    {
        public float distx = 0;
        public float disty = 0;
        public SlideSprite(Image image)
        {
            this.Image = image;
            this.X = 0f;
            this.Y = 0f;
            this.Width = 100;
            this.Height = 100;
        }

        private float targetx = 0;

        public float TargetX
        {
            get { return targetx; }
            set { targetx = value; }
        }

        private float targety = 0;

        public float TargetY
        {
            get { return targety; }
            set { targety = value; }
        }

        private float velocity;

        public float Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public override void act()
        {
            Console.WriteLine(TargetY);
            Console.WriteLine(TargetX);
            if (TargetX > 1920 || TargetX < -1920) TargetX = Math.Sign(TargetX) * 1920;
            if (TargetY > 1080 || TargetY < -1080) TargetY = Math.Sign(TargetY) * 1080;
            float dx = TargetX - X;
            if (distx == 0) distx = dx;
            float dy = TargetY - Y;
            if (disty == 0) disty = dy;
            float xchange = dx / distx;
            float ychange = dy / disty;
            if (Math.Abs(dx) > Velocity) X += Math.Sign(dx) * (xchange*velocity);
            else
            {
                X = TargetX;
                distx = 0;
            }
            if (Math.Abs(dy) > Velocity) Y += Math.Sign(dy) * (ychange*velocity);
            else
            {
                Y = TargetY;
                disty = 0;
            }
        }

        public override void paint(Graphics g)
        {
            g.DrawImage(this.Image, new Rectangle((int)X, (int)Y, (int)Width, (int)Height));
        }
    }
}
