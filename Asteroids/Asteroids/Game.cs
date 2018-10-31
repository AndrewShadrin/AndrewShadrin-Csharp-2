using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace Asteroids
{
    /// <summary>
    /// Класс, предоставляющий основной игровой функционал
    /// </summary>
    static class Game
    {
        #region Game properties

        /// <summary>
        /// Контекст приложения для формирования графического буфера
        /// </summary>
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
        /// Шрифт надписей на игровом поле
        /// </summary>
        public static Font FontInscriptionScore { get; private set; }

        /// <summary>
        /// Шрифт надписи об окончании игры
        /// </summary>
        public static Font FontInscriptionFinish { get; private set; }

        /// <summary>
        /// Генератор псевдослучайных чисел
        /// </summary>
        static Random rnd = new Random();

        /// <summary>
        /// Основной игровой таймер
        /// </summary>
        private static Timer _timer = new Timer();

        /// <summary>
        /// Указатель на запись лога в файл. Истина - в файл, ложь - вывод в консоль
        /// </summary>
        private static bool WriteLogToFile = false;

        #endregion

        #region GameObjects

        /// <summary>
        /// Космический корабль игрока
        /// </summary>
        private static Ship _ship;

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
        /// Очередь игровых объектов для удаления
        /// </summary>
        private static Queue<BaseObject> toDelete;

        /// <summary>
        /// Хранит количество сбитых астероидов
        /// </summary>
        public static int Score { get; private set; }

        #endregion

        #region Game initialisation methods

        /// <summary>
        /// Конструктор класса
        /// </summary>
        static Game()
        {
            _timer.Interval = 20;
            _timer.Tick += _timer_Tick;
            Ship.MessageDie += Finish;
            FontInscriptionScore = new Font("Times New Roman", 24, FontStyle.Bold, GraphicsUnit.Pixel);
            FontInscriptionFinish = new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline);
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
        /// Выполняет инициализацию списков игровых объектов
        /// </summary>
        private static void InitListOfObjects()
        {
            background = new List<BaseObject>();
            asteroids = new List<BaseObject>();
            bullets = new List<BaseObject>();
            toDelete = new Queue<BaseObject>();
        }

        /// <summary>
        /// Производит инициализацию и загрузку игровых объектов
        /// </summary>
        public static void LoadGame()
        {
            InitListOfObjects();
            for (int i = 0; i < 15; i++)
            {
                AddGameObject(TypeOfObjects.Star);
            }
            for (int i = 0; i < 30; i++)
            {
                AddGameObject(TypeOfObjects.Asteroid);
            }
            AddGameObject(TypeOfObjects.Ship);
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
                AddGameObject(TypeOfObjects.Star);
            }
            for (int i = 0; i < 10; i++)
            {
                AddGameObject(TypeOfObjects.BigAsteroid);
            }
            // добавим заголовок
            background.Add(new Inscription(new Point(300, 300), new Point(0, 0), "Астероиды", new Font("Times New Roman", 72, FontStyle.Bold, GraphicsUnit.Pixel), Brushes.Blue));
            ToBeDraw += (background[background.Count - 1].Draw);
            //ToBeDispose += ((IDisposable)background[background.Count - 1]).Dispose;
            // добавим авторство
            background.Add(new Inscription(new Point(700, 700), new Point(0, 0), "Разработал Шадрин Андрей", new Font("Times New Roman", 24, FontStyle.Bold, GraphicsUnit.Pixel), Brushes.YellowGreen));
            ToBeDraw += (background[background.Count - 1].Draw);
            //ToBeDispose += ((IDisposable)background[background.Count - 1]).Dispose;
            _ship = null;
            _timer.Enabled = true;
        }

        #endregion

        #region Auxiliary objects and methods

        /// <summary>
        /// Содержит названия типов объектов для создания
        /// </summary>
        enum TypeOfObjects
        {
            Asteroid,
            BigAsteroid,
            Bullet,
            Ship,
            Star
        }

        /// <summary>
        /// Выполняет добавление игрового объекта
        /// </summary>
        /// <param name="type">описание типа создаваемого объекта</param>
        static void AddGameObject(TypeOfObjects type)
        {
            BaseObject item;
            switch (type)
            {
                case TypeOfObjects.Ship: item = new Ship(new Point(20, 300), new Point(0, 20), new Size(50, 26));
                    _ship = (Ship)item;
                    ((Ship)item).WriteLog += WriteLog;
                    WriteLog("Создан корабль");
                    break;
                case TypeOfObjects.Asteroid: item = new Asteroid(new Point(rnd.Next(Width - 31), rnd.Next(Height - 31)), new Point(rnd.Next(-10, 10), rnd.Next(-10, 10)), new Size(50, 50));
                    asteroids.Add(item);
                    ((Asteroid)item).WriteLog += WriteLog;
                    WriteLog("Создан обычный астероид");
                    break;
                case TypeOfObjects.Star: item = new Star(new Point(Width - 6, rnd.Next(Height - 6)), new Point(-rnd.Next(1, 20), 0), new Size(5, 5));
                    background.Add(item);
                    WriteLog("Создана звезда");
                    break;
                case TypeOfObjects.BigAsteroid: item = new Asteroid(new Point(rnd.Next(Width - 81), rnd.Next(Height - 81)), new Point(rnd.Next(-5, 5), rnd.Next(-5, 5)), new Size(80, 80));
                    asteroids.Add(item);
                    WriteLog("Создан большой астероид");
                    break;
                default: item = new Bullet(new Point(_ship.Rect.X + 50, _ship.Rect.Y + 14), new Point(10, 0), new Size(25, 25));
                    bullets.Add(item);
                    ((Bullet)item).WriteLog += WriteLog;
                    WriteLog("Выпущен снаряд");
                    break;
            }
            ToBeUpdate += item.Update;
            ToBeDraw += item.Draw;
            ToBeDispose += ((IDisposable)item).Dispose;
        }

        #endregion

        #region Game world processing methods

        /// <summary>
        /// Отрисовывает графические объекты
        /// </summary>
        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            ToBeDraw?.Invoke();

            if (_ship != null)
            {
                Buffer.Graphics.DrawString($"Энергия: {_ship.Energy} Счёт: {Score}", FontInscriptionScore, Brushes.White, 0, 0);
            }
            Buffer.Render();
        }

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
                        toDelete.Enqueue(asteroid);
                        toDelete.Enqueue(bullet);
                        Score++;
                    }
                    if (!toDelete.Contains(bullet) && bullet.Rect.Location.X>Width)
                    {
                        toDelete.Enqueue(bullet);
                    }
                }
                // проверяем столкновение астероида с кораблем
                if (_ship!= null && !toDelete.Contains(asteroid) && _ship.Collision(asteroid))
                {
                    _ship?.EnergyLow(rnd.Next(1, 10));
                    toDelete.Enqueue(asteroid);
                    System.Media.SystemSounds.Asterisk.Play();
                    if (_ship.Energy <= 0) _ship?.Die();
                }
            }

            // удаляем снаряды, подбитые астероиды и добавляем новые
            while (toDelete.Count>0)
            {
                try
                {
                    BaseObject item = toDelete.Dequeue();
                    ToBeUpdate -= item.Update;
                    ToBeDraw -= item.Draw;
                    if (item is Asteroid)
                    {
                        asteroids.Remove(item);
                        AddGameObject(TypeOfObjects.Asteroid);
                    }
                    else if (item is Bullet)
                    {
                        bullets.Remove(item);
                    }
                ((IDisposable)item).Dispose();
                }
                catch (Exception)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Производит очистку ресурсов перед закрытием формы игры
        /// </summary>
        internal static void ClearResourses()
        {
            // вычистим игровые объекты
            ToBeDispose?.Invoke();
            background.Clear();
            asteroids.Clear();
            bullets.Clear();
            _ship = null;
            ToBeUpdate = null;
            ToBeDraw = null;
            ToBeDispose = null;
            // запустим сбор мусора
            GC.Collect();
        }

        /// <summary>
        /// Выполняет остановку игрового мира и вывод сообщения о конце игры
        /// </summary>
        public static void Finish()
        {
            _timer.Stop();
            Draw();
            Buffer.Graphics.DrawString("Игра окончена", FontInscriptionFinish, Brushes.Red, 220, 300);
            Buffer.Render();
        }

        #endregion

        #region Logging

        /// <summary>
        /// Выполняет запись строки в файл лога
        /// </summary>
        /// <param name="mess">строка для записи</param>
        public static void WriteLog(string mess)
        {
            string[] content = { $"{DateTime.Now}: {mess}." };
            if (WriteLogToFile) File.AppendAllLines("game_log.txt", content);
            else Console.WriteLine($"{DateTime.Now}: {mess}");
        }

        #endregion

        #region Events

        /// <summary>
        /// Событие, вызывающее перерисовку объектов игрового мира
        /// </summary>
        public static event Action ToBeDraw;
        
        /// <summary>
        /// Событие, вызывающее пересчет положения объектов игрового мира
        /// </summary>
        public static event Action ToBeUpdate;

        /// <summary>
        /// Событие, вызывающее удаление объектов игрового мира
        /// </summary>
        public static event Action ToBeDispose;

        #endregion

        #region Event handlers

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
                AddGameObject(TypeOfObjects.Bullet);
            }
            if (e.KeyCode == Keys.Up) _ship.Up();
            if (e.KeyCode == Keys.Down) _ship.Down();
        }

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

        #endregion
    }
}
