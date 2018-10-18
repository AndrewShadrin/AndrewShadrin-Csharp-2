using System;
using System.Windows.Forms;

namespace Asteroids
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Width = 1024;
            this.Height = 768;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Game.Draw();
            Game.Update();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            Form gameForm = new Form();
            gameForm.Width = 1024;
            gameForm.Height = 768;
            gameForm.FormBorderStyle = FormBorderStyle.None;
            gameForm.StartPosition = FormStartPosition.CenterScreen;
            Game.Init(gameForm);
            gameForm.FormClosing += GameForm_FormClosing;
            this.Visible = false;
            gameForm.Show();
        }

        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Game.Init(this);
            this.Visible = true;
        }
    }
}
