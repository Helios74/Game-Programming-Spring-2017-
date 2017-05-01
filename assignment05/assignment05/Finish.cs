using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment05
{
    public class Finish: PhysicsSprite
    {
        
        public Finish() : base(Properties.Resources.finishline)
        {
            Mask = 8;
        }

        public Finish(int x, int y) : base(Properties.Resources.finishline, x, y)
        {
            Mask = 8;
        }
    }
}
