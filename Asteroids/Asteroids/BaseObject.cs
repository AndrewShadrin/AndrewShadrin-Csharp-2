using System.Drawing;

namespace Asteroids
{
    /// <summary>
    /// Описывает базовый класс объектов
    /// </summary>
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

        /// <summary>
        /// Возвращает прямоугольную область расположения объекта
        /// </summary>
        public Rectangle Rect => new Rectangle(Pos, Size);

        /// <summary>
        /// Определяет, было ли пересечение текущего объекта с другим объектом
        /// </summary>
        /// <param name="obj">объект - цель</param>
        /// <returns>Истина, если есть пересечение</returns>
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

        public delegate void Message();
    }
}
