using System.Drawing;

namespace Asteroids
{
    abstract class BaseObject:ICollision
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
        protected BaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }

        public Rectangle Rect => new Rectangle(Pos, Size);

        public bool Collision(ICollision obj)
        {
            return obj.Rect.IntersectsWith(this.Rect);
        }

        /// <summary>
        /// Метод отрисовки объекта
        /// </summary>
        public abstract void Draw();

        /// <summary>
        /// Выполняет обновление положения объекта согласно направлению движения. Реализует логику отскакивания объекта от границ.
        /// </summary>
        public abstract void Update();
    }
}
