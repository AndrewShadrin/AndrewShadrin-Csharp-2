using System.Drawing;

namespace Asteroids
{
    /// <summary>
    /// Определяет класс "Надпись"
    /// </summary>
    class Inscription:BaseObject
    {
        /// <summary>
        /// Текст объекта для вывода
        /// </summary>
        protected string Text { get; set; }
        
        /// <summary>
        /// Определяет шрифт текста объекта
        /// </summary>
        protected Font Font { get; set; }
        
        /// <summary>
        /// Определяет цвет текста
        /// </summary>
        protected Brush Brush { get; set; }

        /// <summary>
        /// Конструктор объекта
        /// </summary>
        /// <param name="pos">Позиция надписи</param>
        /// <param name="dir">скорость движения</param>
        /// <param name="text">Собственно текст надписи</param>
        /// <param name="font">Шрифт надписи</param>
        /// <param name="brush">Кисть, определяющая цвет надписи</param>
        public Inscription(Point pos, Point dir, string text, Font font, Brush brush) : base(pos, dir, new Size())
        {
            Text = text;
            Font = font;
            Brush = brush;
        }

        /// <summary>
        /// Выполняет отрисовку объекта
        /// </summary>
        public override void Draw()
        {
            PointF pointF1 = new PointF(Pos.X, Pos.Y);
            Game.Buffer.Graphics.DrawString(Text, Font, Brush, pointF1);
        }

        /// <summary>
        /// Производит обновление положения объекта
        /// </summary>
        public override void Update()
        {
            
        }
    }
}
