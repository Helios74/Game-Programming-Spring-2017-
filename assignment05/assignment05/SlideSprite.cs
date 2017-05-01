using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment05
{
    public class SlideSprite : Sprite
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


        public virtual void act()
        {
            
        }

        public override void paint(Graphics g)
        {
            g.DrawImage(this.Image, new Rectangle(0, 0, (int)Width, (int)Height));
        }
    }
}
