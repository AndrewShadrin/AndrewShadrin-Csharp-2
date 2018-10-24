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
            this.Width = 1024;
            this.Height = 768;
            Game.Init(this);
            Game.LoadSplash();
            timer1.Enabled = true;
        }

        /// <summary>
        /// Обработчик события таймера. Запускает отрисовку и пересчет положения объектов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            Game.Draw();
            Game.Update();
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Выход". Закрывает приложение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Старт". Запускает игру
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartGame_Click(object sender, EventArgs e)
        {
            Form gameForm = new Form();
            gameForm.Width = 1024;
            gameForm.Height = 768;
            gameForm.FormBorderStyle = FormBorderStyle.None;
            gameForm.StartPosition = FormStartPosition.CenterScreen;
            Game.Init(gameForm);
            Game.LoadGame();
            gameForm.FormClosing += GameForm_FormClosing;
            gameForm.KeyDown += Game.GameForm_KeyDown;
            this.Visible = false;
            gameForm.Show();
        }

        /// <summary>
        /// Обработчик закрытия формы. Производит инициализацию заставки по закрытию ирговой формы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Game.EndGame();

            // инициализируем заставку
            Game.Init(this);
            Game.LoadSplash();
            this.Visible = true;
        }
    }
}
