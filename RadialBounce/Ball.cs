using System;
using System.Drawing;

namespace RadialBounce
{
    public struct Point
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Point(float x, float y)
        {
            X = x;
            Y = y; 
        }
    }


    public class Ball
    {
        private int diameter;
        private Color color;
        private float x;
        private float y;

        private Point Speed;
        public Point Location
        {
            get
            {
                return new Point(x, y);
            }       
        }

        public int Diameter
        {
            get
            {
                return diameter;
            }
        }

        public Ball(float x, float y, float speed, int diameter, Color color)
        {
            this.x = x;
            this.y = y;
            this.Speed = new Point(speed, speed);
            this.diameter = diameter;
            this.color = color;
        }
        void BounceOffOutSide(Ball ball)
        {
            int deltaX = (int)(ball.Location.X - x);
            int deltaY = (int)(ball.Location.Y - y);
            int deltaDistance = (int)Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));
            
            if (deltaDistance >= ball.Diameter / 2 - diameter / 2)
            {
                KeepInSquareBoundary(new Rectangle((int)(ball.Location.X - deltaDistance / 2), (int)(ball.Location.Y - deltaDistance / 2), deltaDistance, deltaDistance));
            }

        }
        private void BounceOffInside(Ball ball)
        {
            int deltaX = (int)(ball.Location.X - x);
            int deltaY = (int)(ball.Location.Y - y);
            int deltaDistance = (int)Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));

            if (deltaDistance <= ball.Diameter / 2 + diameter / 2)
            {
                KeepOutOfSquareBoundary(new Rectangle((int)ball.Location.X, (int)ball.Location.Y, ball.Diameter, ball.Diameter));
            }

        }
        private void KeepOutOfSquareBoundary(Rectangle boundary)
        {
            int radius = diameter / 2;
            if (x - radius + diameter >= boundary.Width)
            {
                Speed.X = Math.Abs(Speed.X);
            }
            else if (x - radius <= boundary.X)
            {
                Speed.X = -Math.Abs(Speed.X);
            }

            if (y - radius + diameter >= boundary.Height)
            {
                Speed.Y = Math.Abs(Speed.Y);
            }
            else if (y - radius <= boundary.Y)
            {
                Speed.Y = -Math.Abs(Speed.Y);
            }
        }

        private void KeepInSquareBoundary(Rectangle boundary)
        {
            int radius = diameter / 2;
            if (x + radius >= boundary.X + boundary.Width)
            {
                Speed.X = -Math.Abs(Speed.X);
            }
            else if (x - radius <= boundary.X)
            {
                Speed.X = Math.Abs(Speed.X);
            }

            if (y + radius >= boundary.Y + boundary.Height)
            {
                Speed.Y = -Math.Abs(Speed.Y);
            }
            else if(y - radius <= boundary.Y)
            {
                Speed.Y = Math.Abs(Speed.Y);
            }

        }
        private void MatchSpeed(Ball ball)
        {
            x += Speed.X;
            y += Speed.Y;

            Speed = ball.Speed;
        }
        public void Update(Ball ball)
        {
            MatchSpeed(ball);
        }
        public void Update(Rectangle squareBoundary)
        {
            x += Speed.X;
            y += Speed.Y;

            KeepInSquareBoundary(squareBoundary);
        }
        public void Update(Rectangle squareBoundary, Ball innerBall, Ball outerBall)
        {
            x += Speed.X;
            y += Speed.Y;

            KeepInSquareBoundary(squareBoundary);
            BounceOffInside(innerBall);
            BounceOffOutSide(outerBall);
        }

        public void Update(Rectangle squareBoundary, Ball ball)
        {
            x += Speed.X;
            y += Speed.Y;

            KeepInSquareBoundary(squareBoundary);
            BounceOffInside(ball);
        }
        public void Draw(Graphics graphics)
        {            
            graphics.FillEllipse(new SolidBrush(color), x - diameter / 2, y - diameter / 2, diameter, diameter);
        }
    }
}