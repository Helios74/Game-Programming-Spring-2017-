using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    public class Pic:Sprite
    {
        public float theta;
        public bool reverse = false;
        public int Offset;

        float cornerX;

        public float CornerX
        {
            get { return cornerX; }
            set { cornerX = value; }
        }

        float cornerY;

        public float CornerY
        {
            get { return cornerY; }
            set { cornerY = value; }
        }

        public Pic(Image image, int offset)
        {
            this.Image = image;
            this.cornerX = .5f;
            this.cornerY = .5f;
            this.Offset = offset;
        }

        public override void paint(Graphics g)
        {
            g.DrawImage(this.Image, new Rectangle((int)CornerX, (int)CornerY, (int)Width, (int)Height));
        }

        public override void act(int length, int width)
        {
            if(reverse == false)
            {
                theta += 0.000001F;
            }
            else
            {
                theta -= 0.000001F;
            }
            if(Math.Abs((Math.PI/2)-theta) < 0.01)
            {
                reverse = true;
            }
            else if(Math.Abs(0 - theta) < 0.01)
            {
                reverse = false;
            }
            CornerX = width/2+(float)(50 * Math.Cos(theta+Offset));
            CornerY = length/2+(float)(50 * Math.Sin(theta+Offset));
        }
    }
}
