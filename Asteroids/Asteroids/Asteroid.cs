using System;
using System.Drawing;

namespace Asteroids
{
    /// <summary>
    /// Класс, который предоставляет функциональные возможности "Астероида"
    /// </summary>
    class Asteroid : BaseObject, IDisposable, IComparable<Asteroid>
    {
        /// <summary>
        /// Хранит силу астероида
        /// </summary>
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
        /// Генератор псевдослучайных чисел
        /// </summary>
        Random rnd = new Random();
        
        /// <summary>
        /// Хранит картинку для класса астероид
        /// </summary>
        static Image imageAsteroid = Image.FromFile("astero.png");

        /// <summary>
        /// Конструктор объекта
        /// </summary>
        /// <param name="pos">Позиция в мире</param>
        /// <param name="dir">Направление движения</param>
        /// <param name="size">Размер объекта</param>
        public Asteroid(Point pos,Point dir,Size size):base(pos,dir,size)
        {
            this.image = new Bitmap(imageAsteroid, Size);
            AngleSpeed = rnd.Next(-10,10);
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
            if (image == null)
            {
                Game.Buffer.Graphics.DrawEllipse(Pens.White, Pos.X, Pos.Y, Size.Width, Size.Height);
            }
            else
            {
                Game.Buffer.Graphics.ResetTransform();
                Game.Buffer.Graphics.TranslateTransform(Pos.X + Size.Width / 2, Pos.Y + Size.Height / 2);
                Game.Buffer.Graphics.RotateTransform(Angle);
                Game.Buffer.Graphics.TranslateTransform(-Size.Width/2, -Size.Height/2);
                Game.Buffer.Graphics.DrawImage(image,0,0, Size.Width, Size.Height);
                Game.Buffer.Graphics.ResetTransform();
            }
        }

        /// <summary>
        /// Выполняет очистку ресурсов объекта при удалении
        /// </summary>
        void IDisposable.Dispose()
        {
            WriteLog?.Invoke("Астероид уничтожен");
            image = null;
        }

        /// <summary>
        /// Сравнивает астероид с текущим объектом
        /// </summary>
        /// <param name="obj">астероид для сравнения</param>
        /// <returns>1 если сила больше, -1 если сила меньше</returns>
        int IComparable<Asteroid>.CompareTo(Asteroid obj)
        {
            if (Power > obj.Power)
                return 1;
            if (Power < obj.Power)
                return -1;
            return 0;
        }

        /// <summary>
        /// Событие для записи сообщения в лог
        /// </summary>
        public event Action<string> WriteLog;
    }
}
