using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment05
{
    public class Bullet : PhysicsSprite
    {
        
        public Bullet(Image image):base(image)
		{
            Scale = 0.25f; 
            Vx = 20;
            Motion = MotionModel.Kinematic;
            Mask = 7;
        }

        public Bullet(Image image, int x, int y):base(image, x ,y)
        {
            Scale = 0.25f;
            Vx = 20;
            Motion = MotionModel.Kinematic;
            Mask = 7;
        }

        public override void act()
        {
            //add and remove must be asynchronus, finish acting then kill
            foreach (CollideSprite k in this.getCollisions())
            {
                if (k.GetType() != typeof(Finish))
                {
                    k.Kill();
                    this.Kill();
                }
            }
            base.act();
            if (X > 2000 || X < -2000) this.Kill();
        }
    }
}
