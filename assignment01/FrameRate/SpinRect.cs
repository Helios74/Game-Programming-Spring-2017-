using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FrameRate
{
    class SpinRect:Sprite
    {

        public override void paint(Graphics g)
        {
            Rectangle rect = new Rectangle((int)X, (int)Y, (int)size, (int)size);
            Rectangle nrect = new Rectangle((int)X, (int)Y, (int)size, (int)size);
            g.DrawRectangle(Pens.Black, nrect);
            g.FillRectangle(color, nrect);
            g.TranslateTransform(X+size/2,Y+size/2);
            g.RotateTransform(rot);
            g.TranslateTransform(-(X+size/2), -(Y+size/2));
            g.DrawRectangle(Pens.Black, rect);
            g.FillRectangle(color, rect);
            g.ResetTransform();
        }
    }
}
