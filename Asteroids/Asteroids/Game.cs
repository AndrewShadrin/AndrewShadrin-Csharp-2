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
        /// Массив игровых объектов заднего фона
        /// </summary>
        public static List<BaseObject> background;
        
        /// <summary>
        /// Массив игровых объектов типа "астероид"
        /// </summary>
        public static List<BaseObject> asteroids;
        
        /// <summary>
        /// Массив игровых объектов типа "снаряд"
        /// </summary>
        public static List<BaseObject> bullets;

        /// <summary>
        /// Список игровых объектов для удаления
        /// </summary>
        private static List<BaseObject> toDelete = new List<BaseObject>();

        /// <summary>
        /// Генератор псевдослучайных чисел
        /// </summary>
        static Random rnd = new Random();

        public static int Score { get; private set; }

        /// <summary>
        /// Основной игровой таймер
        /// </summary>
        private static Timer _timer = new Timer();

        /// <summary>
        /// Обработчик события таймера. Запускает отрисовку и пересчет положения объектов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void _timer_Tick(object sender, EventArgs e)
        {
            Game.Draw();
            Game.Update();
        }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        static Game()
        {
            _timer.Interval = 20;
            _timer.Tick += _timer_Tick;
            Ship.MessageDie += Finish;
        }

        /// <summary>
        /// Производит инициализацию формы и графических объектов для вывода графики
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
        internal static void ClearResourses()
        {
            // вычистим игровые объекты
            background.Clear();
            asteroids.Clear();
            bullets.Clear();
            _ship = null;
            // запустим сбор мусора
            ToBeUpdate = null;
            ToBeDraw = null;
            GC.Collect();
        }

        /// <summary>
        /// Выполняет остановку игрового мира и вывод сообщения о конце игры
        /// </summary>
        public static void Finish()
        {
            _timer.Stop();
            Buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, 360, 300);
            Buffer.Render();
        }

        /// <summary>
        /// Выполняет инициализацию списков игровых объектов
        /// </summary>
        private static void InitListOfObjects()
        {
            background = new List<BaseObject>();
            asteroids = new List<BaseObject>();
            bullets = new List<BaseObject>();
        }

        /// <summary>
        /// Производит инициализацию и загрузку игровых объектов
        /// </summary>
        public static void LoadGame()
        {
            InitListOfObjects();
            for (int i = 0; i < 15; i++)
            {
                background.Add(new Star(new Point(Width - 6, rnd.Next(Height - 6)), new Point(-rnd.Next(1,20), 0), new Size(5, 5)));
                ToBeUpdate += (background[background.Count - 1].Update);
                ToBeDraw += (background[background.Count - 1].Draw);
            }
            for (int i = 0; i < 30; i++)
            {
                asteroids.Add(new Asteroid(new Point(rnd.Next(Width - 31), rnd.Next(Height - 31)), new Point(rnd.Next(-10, 10), rnd.Next(-10, 10)), new Size(50, 50)));
                ToBeUpdate += (asteroids[asteroids.Count - 1].Update);
                ToBeDraw += (asteroids[asteroids.Count - 1].Draw);
            }
            _ship = new Ship(new Point(20, 300),new Point(0,10), new Size(50, 26));
            ToBeUpdate += _ship.Update;
            ToBeDraw += _ship.Draw;
            Score = 0;
            _timer.Enabled = true;
        }

        /// <summary>
        /// Производит инициализацию и загрузку объектов заставки
        /// </summary>
        public static void LoadSplash()
        {
            InitListOfObjects();
            for (int i = 0; i < 15; i++)
            {
                background.Add(new Star(new Point(Width - 6, rnd.Next(Height - 6)), new Point(-rnd.Next(1,20), 0), new Size(5, 5)));
                ToBeUpdate += (background[background.Count - 1].Update);
                ToBeDraw += (background[background.Count - 1].Draw);
            }
            for (int i = 0; i < 10; i++)
            {
                asteroids.Add(new Asteroid(new Point(rnd.Next(Width - 81), rnd.Next(Height - 81)), new Point(rnd.Next(-5, 5), rnd.Next(-5, 5)), new Size(80, 80)));
                ToBeUpdate += (asteroids[asteroids.Count - 1].Update);
                ToBeDraw += (asteroids[asteroids.Count - 1].Draw);
            }
            // добавим заголовок
            background.Add(new Inscription(new Point(300, 300), new Point(0, 0), "Астероиды", new Font("Times New Roman", 72, FontStyle.Bold, GraphicsUnit.Pixel), Brushes.Blue));
            ToBeDraw += (background[background.Count - 1].Draw);
            // добавим авторство
            background.Add(new Inscription(new Point(700, 700), new Point(0, 0), "Разработал Шадрин Андрей", new Font("Times New Roman", 24, FontStyle.Bold, GraphicsUnit.Pixel), Brushes.YellowGreen));
            ToBeDraw += (background[background.Count - 1].Draw);
            _ship = null;
            _timer.Enabled = true;
        }

        /// <summary>
        /// Отрисовывает графические объекты
        /// </summary>
        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            //foreach (BaseObject obj in background) obj.Draw();
            //foreach (BaseObject obj in asteroids) obj.Draw();
            //foreach (BaseObject obj in bullets) obj.Draw();
            //_ship?.Draw();
            ToBeDraw?.Invoke();

            if (_ship != null)
            {
                Buffer.Graphics.DrawString($"Energy: {_ship.Energy} Score: {Score}", new Font("Times New Roman", 24, FontStyle.Bold, GraphicsUnit.Pixel), Brushes.White, 0, 0);
            }
            Buffer.Render();
        }

        /// <summary>
        /// Делегат для сыбытия пересчета
        /// </summary>
        public delegate void NeedToBeUpdated();

        /// <summary>
        /// Событие, вызывающее перерисовку объектов игрового мира
        /// </summary>
        public static event NeedToBeUpdated ToBeDraw;
        
        /// <summary>
        /// Событие, вызывающее пересчет положения объектов игрового мира
        /// </summary>
        public static event NeedToBeUpdated ToBeUpdate;

        /// <summary>
        /// Выполняет пересчет данных игровых объектов
        /// </summary>
        public static void Update()
        {
            // пересчет положения объектов
            ToBeUpdate?.Invoke();
            
            // проверка столкновений
            foreach (Asteroid asteroid in asteroids)
            {
                foreach (Bullet bullet in bullets)
                {
                    // проверяем столкновение снарядов с астероидом
                    if (!toDelete.Contains(bullet) && !toDelete.Contains(asteroid) && bullet.Collision(asteroid))
                    {
                        System.Media.SystemSounds.Hand.Play();
                        toDelete.Add(asteroid);
                        toDelete.Add(bullet);
                        Score++;
                    }
                }
                // проверяем столкновение астероида с кораблем
                if (_ship!= null && !toDelete.Contains(asteroid) && _ship.Collision(asteroid))
                {
                    _ship?.EnergyLow(rnd.Next(1, 10));
                    toDelete.Add(asteroid);
                    System.Media.SystemSounds.Asterisk.Play();
                    if (_ship.Energy <= 0) _ship?.Die();
                }
            }
            
            // удаляем снаряды, подбитые астероиды и добавляем новые
            foreach (BaseObject item in toDelete)
            {
                ToBeUpdate -= item.Update;
                ToBeDraw -= item.Draw;
                if (item is Asteroid)
                {
                    asteroids.Remove(item);
                    asteroids.Add(new Asteroid(new Point(rnd.Next(Width - 31), rnd.Next(Height - 31)), new Point(rnd.Next(-10, 10), rnd.Next(-10, 10)), new Size(50, 50)));
                    ToBeUpdate += asteroids[asteroids.Count - 1].Update;
                    ToBeDraw += asteroids[asteroids.Count - 1].Draw;
                }
                else if (item is Bullet)
                {
                    bullets.Remove(item);
                }
                ((IDisposable)item).Dispose();
            }
            toDelete.Clear();
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
            if (e.KeyCode == Keys.ControlKey)
            {
                bullets.Add(new Bullet(new Point(_ship.Rect.X + 50, _ship.Rect.Y + 14), new Point(10, 0), new Size(25, 25)));
                ToBeUpdate += bullets[bullets.Count - 1].Update;
                ToBeDraw += bullets[bullets.Count - 1].Draw;
            }
            if (e.KeyCode == Keys.Up) _ship.Up();
            if (e.KeyCode == Keys.Down) _ship.Down();
        }

    }
}
