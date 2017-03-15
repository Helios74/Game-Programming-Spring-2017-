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
        public float dist = 0;
        public SlideSprite(Image image)
        {
            this.Image = image;
            this.X = 0.5f;
            this.Y = 0.5f;
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
            /*
            if (TargetX > 1920 || TargetX < -1920) TargetX = Math.Sign(TargetX) * 1920;
            if (TargetY > 1080 || TargetY < -1080) TargetY = Math.Sign(TargetY) * 1080;
            float dx = TargetX - X;
            if (distx == 0) distx = dx;
            float dy = TargetY - Y;
            if (disty == 0) disty = dy;
            float xchange = Math.Abs(dx / distx);
            float ychange = Math.Abs(dy / disty);
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
            */
            float dx = TargetX - X;
            float dy = TargetY - Y;
            float d = (float)Math.Sqrt(dx * dx + dy * dy);
            if (dist == 0) dist = d;
            Console.WriteLine(TargetX);
            Console.WriteLine(TargetY);
            if(Math.Abs(d) > Velocity)
            {
                this.Rotation = (float)(Math.Atan(dy / dx) * (180 / Math.PI));
                X += dx/dist * velocity;
                Y += dy/dist * velocity;
            }
            else
            {
                X = TargetX;
                Y = TargetY;
                this.Rotation = 0;
                dist = 0;
            }
        }

        public override void paint(Graphics g)
        {
            g.DrawImage(this.Image, new Rectangle( (int)(0-Width/2),(int)(0-Height/2), (int)Width, (int)Height));
        }
    }
}
