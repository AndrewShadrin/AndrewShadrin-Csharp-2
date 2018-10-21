using System;
using System.Drawing;

namespace Asteroids
{
    class Asteroid:BaseObject,IDisposable
    {
        public int Power { get; set; }

        /// <summary>
        /// Угол поворота объекта вокруг своей оси
        /// </summary>
        public int Angle { get; set; }

        /// <summary>
        /// Скорость поворота объекта вокруг своей оси
        /// </summary>
        public int AngleSpeed { get; set; }

        /// <summary>
        /// Конструктор объекта
        /// </summary>
        /// <param name="pos">Позиция в мире</param>
        /// <param name="dir">Направление движения</param>
        /// <param name="size">Размер объекта</param>
        public Asteroid(Point pos,Point dir,Size size):base(pos,dir,size)
        {
            this.image = new Bitmap(Image.FromFile("astero.png"), Size);
            AngleSpeed = 20;
            Angle = 0;
            Power = 1;
        }

        /// <summary>
        /// Выполняет пересчет положения объекта
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            if (Pos.X <= 0) Dir.X = -Dir.X;
            if (Pos.X + Size.Width >= Game.Width) Dir.X = -Dir.X;
            if (Pos.Y <= 0) Dir.Y = -Dir.Y;
            if (Pos.Y + Size.Height >= Game.Height) Dir.Y = -Dir.Y;
            Angle += AngleSpeed;
        }

        /// <summary>
        /// Выполняет отрисовку объекта
        /// </summary>
        public override void Draw()
        {
            // как-то с вращением пока не получается
            //Graphics graphicsContainer = Graphics.FromImage(image);
            //graphicsContainer.RotateTransform(Angle);
            //Game.Buffer.Graphics.DrawImage(image, Pos);
            if (image == null)
            {
                Game.Buffer.Graphics.DrawEllipse(Pens.White, Pos.X, Pos.Y, Size.Width, Size.Height);
            }
            else
            {
                Game.Buffer.Graphics.DrawImage(image, Pos);
            }
        }


        void IDisposable.Dispose()
        {
            this.image = null;
        }

    }
}
