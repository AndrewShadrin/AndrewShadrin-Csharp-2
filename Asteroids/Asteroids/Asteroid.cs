using System.Drawing;

namespace Asteroids
{
    class Asteroid:BaseObject
    {
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
        }

        /// <summary>
        /// Выполняет пересчет положения объекта
        /// </summary>
        public override void Update()
        {
            base.Update();
            Angle += AngleSpeed;
        }

        /// <summary>
        /// Выполняет отрисовку объекта
        /// </summary>
        public override void Draw()
        {
            // как-то с вращением пока не получается
            Graphics graphicsContainer = Graphics.FromImage(image);
            graphicsContainer.RotateTransform(Angle);
            Game.Buffer.Graphics.DrawImage(image, Pos);
        }
    }
}
