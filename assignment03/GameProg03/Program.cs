using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameProg03
{
    using System;
    using System.IO;
    using System.Media;
    using System.Windows.Forms;

    namespace FormExample
    {
        public class Program : Engine
        {
            TimeSpan frameTime = new TimeSpan(0,0,0,1,0);
            DateTime last;
            DateTime now;
            static Stream str = Properties.Resources.pacman_chomp;
            SoundPlayer snd = new SoundPlayer(str);
            public static SlideSprite pac;
            public static Background field;

            public void musicspace()
            {
                now = DateTime.Now;
                TimeSpan diff = now - last;
                if (diff.TotalMilliseconds >= frameTime.TotalMilliseconds)
                {
                    snd.Play();
                    last = DateTime.Now;
                }
            }

            protected override void OnKeyDown(KeyEventArgs e)
            {
                
                if (e.KeyCode == Keys.Left)
                {
                    pac.TargetX -= 30;
                    musicspace();
                }
                if (e.KeyCode == Keys.Right)
                {
                    pac.TargetX += 30;
                    musicspace();
                }
                if (e.KeyCode == Keys.Up)
                {
                    pac.TargetY -= 30;
                    musicspace();
                }
                if (e.KeyCode == Keys.Down)
                {
                    pac.TargetY += 30;
                    musicspace();
                }
            }

            /// <summary>
            /// The main entry point for the application.
            /// </summary>
            [STAThread]
            static void Main()
            {
                pac = new SlideSprite(Properties.Resources.newpac);
                field = new Background(Properties.Resources.Background);
                pac.TargetX = 100;
                pac.TargetY = 100;
                pac.Velocity = 5;
                Program.Background.add(field);
                Program.canvas.add(pac);
                Application.Run(new Program());
            }
        }
    }
}
