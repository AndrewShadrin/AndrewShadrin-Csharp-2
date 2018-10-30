using System;
using System.Drawing;

namespace Asteroids
{
    /// <summary>
    /// Класс, который предоставляет функциональные возможности космического корабля
    /// </summary>
    class Ship : BaseObject, IDisposable
    {
        /// <summary>
        /// Хранит значение энергии корабля
        /// </summary>
        private int _energy = 100;

        /// <summary>
        /// Возвращает количество энергии корабля
        /// </summary>
        public int Energy => _energy;

        /// <summary>
        /// Выполняет понижение энергии корабля
        /// </summary>
        /// <param name="n">количество единиц энергии для вычитания</param>
        public void EnergyLow(int n)
        {
            _energy -= n;
        }

        /// <summary>
        /// Конструктор объекта "Корабль"
        /// </summary>
        /// <param name="pos">Позиция в мире</param>
        /// <param name="dir">Направление движения</param>
        /// <param name="size">Размер объекта</param>
        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            image= new Bitmap(Image.FromFile("ship.png"), Size);
        }

        /// <summary>
        /// Выполняет отрисовку объекта
        /// </summary>
        public override void Draw()
        {
            if (image == null)
            {
                Game.Buffer.Graphics.DrawEllipse(Pens.White, Pos.X, Pos.Y, Size.Width, Size.Height);
            }
            else
            {
                Game.Buffer.Graphics.DrawImage(image, Pos.X, Pos.Y, Size.Width, Size.Height);
            }
        }

        /// <summary>
        /// Выполняет изменение положения корабля
        /// </summary>
        public override void Update()
        {
        }

        /// <summary>
        /// Выполняет смену направления движения корабля вверх
        /// </summary>
        public void Up()
        {
            if (Pos.Y - Dir.Y > 0)
            {
                Pos.Y = Pos.Y - Dir.Y;
            }
            else Pos.Y = 0;
        }

        /// <summary>
        /// Выполняет смену направления движения корабля вниз
        /// </summary>
        public void Down()
        {
            if (Pos.Y + Dir.Y < Game.Height - Size.Height)
            {
                Pos.Y = Pos.Y + Dir.Y;
            }
            else Pos.Y = Game.Height - Size.Height;
        }

        /// <summary>
        /// Событие для сообщения о смерти корабля
        /// </summary>
        public static event Message MessageDie;

        /// <summary>
        /// Событие для записи сообщения в лог
        /// </summary>
        public event Action<string> WriteLog;

        /// <summary>
        /// Выполняет оповещение о смерти корабля
        /// </summary>
        public void Die()
        {
            WriteLog?.Invoke("Корабль подбит");
            MessageDie?.Invoke();
        }

        /// <summary>
        /// Освобождает все ресурсы, используемые объектом Ship
        /// </summary>
        void IDisposable.Dispose()
        {
            WriteLog?.Invoke("Ship уничтожен");
            image?.Dispose();
        }
    }
}
