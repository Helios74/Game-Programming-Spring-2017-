using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    class Background:Sprite
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

        public Background(Image image, int offset)
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
            foreach (Sprite s in children)
            {
                s.act(length, width);
            }
        }
    }
}

