//Physics
//static type, gravity type - affected only by gravity, kinematics type - Motion but no dynamics
//Gravity Type - work on act functions so that gravity is manipulable
using System;
using System.Drawing;
namespace assignment05
{
    public class PhysicsSprite : CollideSprite
    {
        //change x to double
        //three states: fixed: ignore all, kinematics: ignore accel, grav: take all into account
        public enum MotionModel { Dynamic, Static, Kinematic };//put in general namespace

        //want new physics for all objects, for now just keep simplified platformer
        //Modifies velocity, constant
        float gx = 0;
        float gy = 1f;

        //Modifies location
        float vx = 0;
        float vy = 0;

        //can have thruster model to accelerate as you move, constants
        float ax = 0;
        float ay = 0;

        MotionModel motion = MotionModel.Dynamic;

        public float Gx { get { return gx; } set { gx = value; } }
        public float Gy { get { return gy; } set { gy = value; } }
        public float Ax { get { return ax; } set { ax = value; } }
        public float Ay { get { return ay; } set { ay = value; } }
        public float Vx { get { return vx; } set { vx = value; } }
        public float Vy { get { return vy; } set { vy = value; } }
        public MotionModel Motion { get { return motion; } set { motion = value; } }

        public PhysicsSprite(Image image) : base(image)
        {
        }

        public PhysicsSprite(Image image, int x, int y): base(image, x, y)
        {
            X = x;
            Y = y;
        }

        public bool onGround()
        {
            Y += 10;//triggers collision
            if (this.getCollisions().Count > 0)
            {
                Y -= 10;
                return true;
            }
            Y -= 10;
            return false;
        }

        public override void act()
        {
            base.act();
            if (motion.Equals(MotionModel.Static)) return;
            X += Vx;
            if (!motion.Equals(MotionModel.Static))
            {
                if (this.getCollisions().Count > 0)
                {
                    if(!(this is Bullet)) X -= Vx;
                    Vx = 0;
                }
            }
            Y += Vy;
            if (!motion.Equals(MotionModel.Static))
            {
                if (this.getCollisions().Count > 0)
                {
                    if(!(this is Bullet))Y -= Vy;
                    Vy = 0;
                }
            }
            if (motion.Equals(MotionModel.Kinematic)) return;
            Vx += Gx + Ax;
            Vy += Gy + Ay;
        }
        
    }
}

   