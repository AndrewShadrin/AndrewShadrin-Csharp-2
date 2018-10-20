using System.Drawing;

namespace Asteroids
{
    class BaseObject
    {
        /// <summary>
        /// Начальная позиция
        /// </summary>
        protected Point Pos;
        
        /// <summary>
        /// Направление движения
        /// </summary>
        protected Point Dir;
        
        /// <summary>
        /// Размер
        /// </summary>
        protected Size Size;

        /// <summary>
        /// Хранит изображение объекта
        /// </summary>
        protected Image image;

        /// <summary>
        /// Конструктор объекта с заданными параметрами
        /// </summary>
        /// <param name="pos">начальная позиция</param>
        /// <param name="dir">направление движения</param>
        /// <param name="size">размер</param>
        public BaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }

        /// <summary>
        /// Метод отрисовки объекта
        /// </summary>
        public virtual void Draw()
        {
            if (image == null)
            {
                Game.Buffer.Graphics.DrawEllipse(Pens.White, Pos.X, Pos.Y, Size.Width, Size.Height);
            }
            else
            {
                Game.Buffer.Graphics.DrawImage(image, Pos);
            }
        }

        /// <summary>
        /// Выполняет обновление положения объекта согласно направлению движения. Реализует логику отскакивания объекта от границ.
        /// </summary>
        public virtual void Update()
        {
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            if (Pos.X <= 0) Dir.X = -Dir.X;
            if (Pos.X + Size.Width >= Game.Width) Dir.X = -Dir.X;
            if (Pos.Y <= 0) Dir.Y = -Dir.Y;
            if (Pos.Y + Size.Height >= Game.Height) Dir.Y = -Dir.Y;
        }
    }
}
