using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SokubanAssignment04
{
    public class TextSprite : Sprite
    {
        public TextSprite(String k, int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.text = k;
        }
        private String text;

        public String Text
        {
            get { return text; }
            set { text = value; }
        }

        public override void paint(Graphics g)
        {
            System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 16);
            System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
            g.DrawString(text, drawFont, drawBrush, X, Y, drawFormat);
        }

    }
}
