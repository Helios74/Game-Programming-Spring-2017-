using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment05
{
    public class Enemy : PhysicsSprite
    {
        private Random r = new Random();
        private bool left = false;

        public Enemy() : base(Properties.Resources.enemy)
        {
            Vx = 4f;
        }

        public Enemy(int x, int y) : base(Properties.Resources.enemy, x, y)
        {
            Vx = 4f;
        }

        public void Shoot()//Special killing can be accomplished with masking
        {
            Bullet bullet = new Bullet(Properties.Resources.Bullet, (int)(X + Width * Scale * 1.1f), (int)(Y + Height * Scale / 2));
            if (left)
            {
                bullet.X = X - Width * Scale * 1.1f;
                bullet.Vx *= -1;
            }
            Engine.canvas.add(bullet);
        }

        public bool isWall()
        {
            X += Vx;
            if (getCollisions().Count > 0)
            {
                X -= Vx;
                return true;
            }
            X -= Vx;
            return false;
        }
        public void killCharacter()
        {
            X += Vx;
            List<CollideSprite> list = getCollisions();
            X -= Vx;
            foreach (CollideSprite s in list)
            {
                if (s.GetType() == typeof(Character)) s.Kill();
            }
        }

        public override void act()
        {
            base.act();
            killCharacter();
            if (isWall()) Vx *= -1;
            if (r.NextDouble() < .01) Shoot();
            if (Vx < 0) left = true;
            if (Vx > 0) left = false;
        }

    }
}
