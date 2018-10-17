using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Asteroid:BaseObject
    {
        public Asteroid(Point pos,Point dir,Size size):base(pos,dir,size)
        {
            this.image = new Bitmap(Image.FromFile("astero.png"), Size);

        }
    }
}
