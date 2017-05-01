using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment05
{
    public class Star: PhysicsSprite
    {
        public Star() : base(Properties.Resources.star)
        {
        }

        public Star(int x, int y): base(Properties.Resources.star, x, y)
        {
            X = x;
            Y = y;
        }
    }
}
