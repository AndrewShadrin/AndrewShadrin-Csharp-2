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
        }

        /// <summary>
        /// Производит отрисовку объекта
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawRectangle(Pens.OrangeRed, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        /// <summary>
        /// Выполняет обновление положения объекта
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
        }

        /// <summary>
        /// Выполняет очистку ресурсов объекта при удалении
        /// </summary>
        void IDisposable.Dispose()
        {
            this.image = null;
        }

    }
}
