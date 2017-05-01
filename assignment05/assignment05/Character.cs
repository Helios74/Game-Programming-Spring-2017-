using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment05
{
    public class Character : PhysicsSprite
    {
        private bool left = false;
        public int points = 0;

        public Character() : base(Properties.Resources.main)
        {
            
        }

        public Character(int x, int y) : base(Properties.Resources.main, x, y)
        {

        }

        public void Shoot()
        {
            Bullet bullet = new Bullet(Properties.Resources.Bullet, (int)(X + Width * Scale * 1.1f), (int)(Y + (Height * Scale)/2 ));
            if (left)
            {
                bullet.X = X - Width * Scale * 1.1f;
                bullet.Vx *= -1;
            }
            Program.canvas.add(bullet);
        }

        public void getStar()
        {
            X += Vx;
            Y += Vy;
            List<CollideSprite> list = getCollisions();
            X -= Vx;
            Y -= Vy;
            foreach (CollideSprite s in list)
            {
                if (s.GetType() == typeof(Star))
                {
                    s.Kill();
                    points += 1;
                }
            }
        }

        public override void act()
        {
            getStar();
            base.act();
            if (Vx < 0) left = true;
            if (Vx > 0) left = false;
        }
    }
}
