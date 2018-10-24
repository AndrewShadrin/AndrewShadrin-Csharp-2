using System.Drawing;

namespace Asteroids
{
    /// <summary>
    /// Класс, который предоставляет функциональные возможности космического корабля
    /// </summary>
    class Ship : BaseObject
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

        public override void Update()
        {
            
        }

        /// <summary>
        /// Выполняет смещение корабля вверх
        /// </summary>
        public void Up()
        {
            if (Pos.Y > 0) Pos.Y = Pos.Y - Dir.Y;
        }

        /// <summary>
        /// Выполняет смещение корабля вниз
        /// </summary>
        public void Down()
        {
            if (Pos.Y < Game.Height) Pos.Y = Pos.Y + Dir.Y;
        }

        public void Die()
        {
        }
    }
}
