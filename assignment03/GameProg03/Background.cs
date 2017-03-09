using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProg03
{
    public class Background:Sprite
    {
        public Background(Image image)
        {
            Image = image;
            this.X = 0f;
            this.Y = 0f;
        }

        public override void paint(Graphics g)
        {
            g.DrawImage(this.Image, new Rectangle((int)X, (int)Y, (int)Width, (int)Height));
        }
    }
}
