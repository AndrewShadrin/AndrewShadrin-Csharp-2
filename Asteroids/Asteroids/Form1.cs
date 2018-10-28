using System;
using System.Windows.Forms;

namespace Asteroids
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Конструктор формы. Производит инициализацию компонентов заставки.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            Width = 1024;
            Height = 768;
            Game.Init(this);
            Game.LoadSplash();
            Game.WriteLog("Запуск заставки");
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Выход". Закрывает приложение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            Game.WriteLog("Выход из приложения");
            Application.Exit();
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Старт". Запускает игру
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartGame_Click(object sender, EventArgs e)
        {
            Game.ClearResourses();
            Form gameForm = new Form
            {
                Width = 1024,
                Height = 768,
                FormBorderStyle = FormBorderStyle.None,
                StartPosition = FormStartPosition.CenterScreen
            };
            Game.Init(gameForm);
            Game.LoadGame();
            gameForm.FormClosing += GameForm_FormClosing;
            gameForm.KeyDown += Game.GameForm_KeyDown;
            Visible = false;
            Game.WriteLog("Игра начата");
            gameForm.Show();
        }

        /// <summary>
        /// Обработчик закрытия формы. Производит инициализацию заставки по закрытию игровой формы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Game.ClearResourses();
            Game.WriteLog("Выход из игры");

            // инициализируем заставку
            Game.Init(this);
            Game.LoadSplash();
            Visible = true;
        }
    }
}
