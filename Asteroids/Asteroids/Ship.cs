using System;
using System.Collections.Generic;
using System.Drawing;

namespace Asteroids
{
    class Ship : BaseObject
    {
        private int _energy = 100;
        public int Energy => _energy;

        public void EnergyLow(int n)
        {
            _energy -= n;
        }

        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public override void Draw()
        {
            if (image == null)
            {
                Game.Buffer.Graphics.DrawEllipse(Pens.White, Pos.X, Pos.Y, Size.Width, Size.Height);
            }
            else
            {
                Game.Buffer.Graphics.DrawImage(image, 0, 0, Size.Width, Size.Height);
            }
        }

        public override void Update()
        {
            
        }

        public void Up()
        {
            if (Pos.Y > 0) Pos.Y = Pos.Y - Dir.Y;
        }

        public void Down()
        {
            if (Pos.Y < Game.Height) Pos.Y = Pos.Y + Dir.Y;
        }

        public void Die()
        {
        }
    }
}
