using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RadialBounce
{
    public partial class Form1 : Form
    {
        private Graphics graphics;
        private Bitmap image;

        Ball[] balls;
        Ball innerBall;
        Ball outerBall;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            balls = new Ball[1000];

            Random random = new Random();

            for (int i = 0; i < balls.Length; i++)
            {
                balls[i] = new Ball(x: random.Next(0, Canvas.Width), y: random.Next(0, Canvas.Height), speed: 3f, diameter: 10, color: Color.DarkGoldenrod);
            }
            
            innerBall = new Ball(x: Canvas.Width - 200, y: Canvas.Height - 200, speed: 0.5f, diameter: 270, color: Color.LightSeaGreen);
            outerBall = new Ball(x: innerBall.Location.X, y: innerBall.Location.Y, speed: 0.5f, diameter: 300, color: Color.PaleVioletRed);

            Canvas.BackColor = Color.CornflowerBlue;

            image = new Bitmap(Canvas.Width, Canvas.Height);
            graphics = Graphics.FromImage(image);

            Canvas.Image = image;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            graphics.Clear(Canvas.BackColor);
            UpdateObjects();
            Draw();
            Canvas.Image = image;
        }

        private void UpdateObjects()
        {
            outerBall.Update(new Rectangle(Canvas.Location, Canvas.Size));
            innerBall.Update(outerBall);

            foreach (var ball in balls)
            {
                ball.Update(new Rectangle(Canvas.Location, Canvas.Size), innerBall, outerBall);
            }
        }

        private void Draw()
        {
            outerBall.Draw(graphics);
            innerBall.Draw(graphics);

            foreach (var ball in balls)
            {
                ball.Draw(graphics);
            }
        }
    }
}
