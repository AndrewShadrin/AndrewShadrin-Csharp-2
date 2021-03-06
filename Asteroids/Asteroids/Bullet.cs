﻿using System;
using System.Drawing;

namespace Asteroids
{
    /// <summary>
    /// Класс, который предоставляет функциональные возможности "Снаряда"
    /// </summary>
    class Bullet : BaseObject, IDisposable
    {
        /// <summary>
        /// Конструктор объекта "пуля"
        /// </summary>
        /// <param name="pos">начальная позиция пули</param>
        /// <param name="dir">направление движения</param>
        /// <param name="size">размер</param>
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            image = new Bitmap(imageFire, Size);
        }

        /// <summary>
        /// Производит отрисовку объекта
        /// </summary>
        public override void Draw()
        {
            if (image == null)
            {
                Game.Buffer.Graphics.DrawRectangle(Pens.OrangeRed, Pos.X, Pos.Y, Size.Width, Size.Height);
            }
            else
            {
                Game.Buffer.Graphics.DrawImage(image, Pos.X, Pos.Y, Size.Width, Size.Height);
            }
        }

        /// <summary>
        /// Выполняет обновление положения объекта
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
        }

        /// <summary>
        /// Освобождает все ресурсы, используемые объектом Bullet
        /// </summary>
        void IDisposable.Dispose()
        {
            WriteLog?.Invoke("Снаряд уничтожен");
            image.Dispose();
        }

        /// <summary>
        /// Хранит картинку для класса
        /// </summary>
        static Image imageFire = Image.FromFile("fire.png");

        /// <summary>
        /// Событие для записи сообщения в лог
        /// </summary>
        public event Action<string> WriteLog;
    }
}
