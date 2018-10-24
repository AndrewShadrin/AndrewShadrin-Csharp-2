using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Asteroids
{
    static class Game
    {
        private static BufferedGraphicsContext _context;

        /// <summary>
        /// Графический буфер для вывода графики
        /// </summary>
        public static BufferedGraphics Buffer;

        /// <summary>
        /// Ширина игрового поля
        /// </summary>
        public static int Width { get; set; }

        /// <summary>
        /// Высота игрового поля
        /// </summary>
        public static int Height { get; set; }

        /// <summary>
        /// Космический корабль игрока
        /// </summary>
        private static Ship _ship = new Ship(new Point(10, 400), new Point(5, 5), new Size(10, 10));

        /// <summary>
        /// Массив игровых объектов
        /// </summary>
        public static List<BaseObject> background;
        
        /// <summary>
        /// Массив игровых объектов
        /// </summary>
        public static List<BaseObject> asteroids;
        
        /// <summary>
        /// Массив игровых объектов
        /// </summary>
        public static List<BaseObject> bullets;

        /// <summary>
        /// Генератор псевдослучайных чисел
        /// </summary>
        static Random rnd = new Random();

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
        }

        /// <summary>
        /// Производит очистку ресурсов перед закрытием формы игры
        /// </summary>
        internal static void EndGame()
        {
            background.Clear();
            asteroids.Clear();
            bullets.Clear();
            _ship = null;
            GC.Collect();
        }

        /// <summary>
        /// Выполняет инициализацию списков объектов
        /// </summary>
        private static void InitListOfObjects()
        {
            background = new List<BaseObject>();
            asteroids = new List<BaseObject>();
            bullets = new List<BaseObject>();
        }

        /// <summary>
        /// Производит загрузку игровых объектов
        /// </summary>
        public static void LoadGame()
        {
            InitListOfObjects();
            for (int i = 0; i < 15; i++) background.Add(new Star(new Point(Width-6, rnd.Next(Height-6)), new Point(-rnd.Next(20), 0), new Size(5, 5)));
            for (int i = 0; i < 30; i++) asteroids.Add(new Asteroid(new Point(rnd.Next(Width-31), rnd.Next(Height-31)), new Point(rnd.Next(-10, 10), rnd.Next(-10, 10)), new Size(50, 50)));
            bullets.Add(new Bullet(new Point(0, 200), new Point(5, 0), new Size(4, 1)));
            _ship = new Ship(new Point(20, 300),new Point(20,20), new Size(50, 26));
        }

        /// <summary>
        /// Производит загрузку объектов заставки
        /// </summary>
        public static void LoadSplash()
        {
            InitListOfObjects();
            for (int i = 0; i < 15; i++) background.Add(new Star(new Point(Width - 6, rnd.Next(Height - 6)), new Point(-rnd.Next(20), 0), new Size(5, 5)));
            for (int i = 0; i < 10; i++) asteroids.Add(new Asteroid(new Point(rnd.Next(Width-81), rnd.Next(Height-81)), new Point(rnd.Next(-5, 5), rnd.Next(-5, 5)), new Size(80, 80)));
            // добавим заголовок
            background.Add(new Inscription(new Point(300, 300), new Point(0, 0), "Астероиды", new Font("Times New Roman", 72, FontStyle.Bold, GraphicsUnit.Pixel), Brushes.Blue));
            // добавим авторство
            background.Add(new Inscription(new Point(700, 700), new Point(0, 0), "Разработал Шадрин Андрей", new Font("Times New Roman", 24, FontStyle.Bold, GraphicsUnit.Pixel), Brushes.YellowGreen));
            _ship = null;
        }

        /// <summary>
        /// Отрисовывает графические объекты
        /// </summary>
        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in background) obj.Draw();
            foreach (BaseObject obj in asteroids) obj.Draw();
            foreach (BaseObject obj in bullets) obj.Draw();
            _ship?.Draw();
            if (_ship != null)
                Buffer.Graphics.DrawString("Energy:" + _ship.Energy, SystemFonts.DefaultFont, Brushes.White, 0, 0);
            Buffer.Render();
        }

        /// <summary>
        /// Выполняет пересчет положения объектов
        /// </summary>
        public static void Update()
        {
            List<BaseObject> toDelete = new List<BaseObject>();
            foreach (BaseObject obj in background) obj.Update();
            foreach (Bullet obj in bullets) obj.Update();
            foreach (Asteroid asteroid in asteroids)
            {
                asteroid.Update();
                foreach (Bullet bullet in bullets)
                {
                    if (!toDelete.Contains(bullet) && !toDelete.Contains(asteroid) && bullet.Collision(asteroid))
                    {
                        System.Media.SystemSounds.Hand.Play();
                        toDelete.Add(asteroid);
                        toDelete.Add(bullet);
                    }
                }
                if (_ship!=null && _ship.Collision(asteroid))
                {
                    _ship?.EnergyLow(rnd.Next(1, 10));
                    System.Media.SystemSounds.Asterisk.Play();
                    if (_ship.Energy <= 0) _ship?.Die();
                }
            }
            // удаляем подбитые астероиды и добавляем новые
            foreach (BaseObject item in toDelete)
            {
                if (item is Asteroid)
                {
                    asteroids.Remove(item);
                    asteroids.Add(new Asteroid(new Point(rnd.Next(Width - 31), rnd.Next(Height - 31)), new Point(rnd.Next(-10, 10), rnd.Next(-10, 10)), new Size(50, 50)));
                }
                else if (item is Bullet)
                {
                    bullets.Remove(item);
                }
                ((IDisposable)item).Dispose();
            }
        }
        
        /// <summary>
        /// Обработчик нажатия клавиш в форме
        /// </summary>
        /// <param name="sender">источник события</param>
        /// <param name="e">событие нажатия клавиши</param>
        public static void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            //выход по клавише Escape
            if (e.KeyData == Keys.Escape)
            {
                (sender as Form).Close();
            }
            if (e.KeyCode == Keys.ControlKey) bullets.Add(new Bullet(new Point(_ship.Rect.X + 10, _ship.Rect.Y + 4), new Point(4, 0), new Size(4, 1)));
            if (e.KeyCode == Keys.Up) _ship.Up();
            if (e.KeyCode == Keys.Down) _ship.Down();
        }

    }
}
