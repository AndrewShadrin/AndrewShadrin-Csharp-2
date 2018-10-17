﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Asteroids
{
    static class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        // Свойства
        // Ширина и высота игрового поля
        public static int Width { get; set; }
        public static int Height { get; set; }
        
        /// <summary>
        /// Массив игровых объектов
        /// </summary>
        public static List<BaseObject> _objs;

        static Game()
        {
        }

        /// <summary>
        /// Производит инициализацию формы и графических объектов
        /// </summary>
        /// <param name="form">форма для инициализации</param>
        public static void Init(Form form)
        {
            // Графическое устройство для вывода графики            
            Graphics g;
            // Предоставляет доступ к главному буферу графического контекста для текущего приложения
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            // Создаем объект (поверхность рисования) и связываем его с формой
            // Запоминаем размеры формы
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            // Связываем буфер в памяти с графическим объектом, чтобы рисовать в буфере
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Load();
        }

        /// <summary>
        /// Производит загрузку игровых объектов
        /// </summary>
        public static void Load()
        {
            Random rnd = new Random();
            _objs = new List<BaseObject>();
            for (int i = 0; i < 15; i++)
                _objs.Add(new Star(new Point(1020, rnd.Next(760)), new Point(-rnd.Next(20), 0), new Size(5, 5)));
            for (int i = 0; i < 30; i++)
                _objs.Add(new Asteroid(new Point(rnd.Next(1004), rnd.Next(748)), new Point(rnd.Next(1,30), rnd.Next(1,30)), new Size(20, 20)));
        }

        /// <summary>
        /// Отрисовывает графические объекты
        /// </summary>
        public static void Draw()
        {

            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
                obj.Draw();
            Buffer.Render();

        }

        /// <summary>
        /// Производит расчеты перемещений объектов
        /// </summary>
        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();
        }
    }
}