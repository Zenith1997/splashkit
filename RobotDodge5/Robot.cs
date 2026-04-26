using SplashKitSDK;

namespace RobotDodge
{
    public class Robot
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public Color MainColor { get; private set; }

        private Vector2D Velocity { get; set; }

        public int Width => 50;
        public int Height => 50;

        public Circle CollisionCircle
        {
            get
            {
                return SplashKit.CircleAt(X + Width / 2, Y + Height / 2, 20);
            }
        }

        public Robot(Window gameWindow, Player player)
        {
            // Spawn OFF SCREEN
            if (SplashKit.Rnd() < 0.5)
            {
                X = SplashKit.Rnd(gameWindow.Width);
                if (SplashKit.Rnd() < 0.5)
                    Y = -Height;
                else
                    Y = gameWindow.Height;
            }
            else
            {
                Y = SplashKit.Rnd(gameWindow.Height);
                if (SplashKit.Rnd() < 0.5)
                    X = -Width;
                else
                    X = gameWindow.Width;
            }

            MainColor = SplashKit.RandomRGBColor(200);

            const int SPEED = 4;

            Point2D fromPt = new Point2D() { X = X, Y = Y };
            Point2D toPt = new Point2D() { X = player.X, Y = player.Y };

            Vector2D dir = SplashKit.UnitVector(
                SplashKit.VectorPointToPoint(fromPt, toPt)
            );

            Velocity = SplashKit.VectorMultiply(dir, SPEED);
        }

        public void Update()
        {
            X += Velocity.X;
            Y += Velocity.Y;
        }

        public bool IsOffscreen(Window screen)
        {
            return X < -Width || X > screen.Width || Y < -Height || Y > screen.Height;
        }

        public void Draw()
        {
            double leftX = X + 12;
            double rightX = X + 27;
            double eyeY = Y + 10;
            double mouthY = Y + 30;

            SplashKit.FillRectangle(Color.Gray, X, Y, Width, Height);
            SplashKit.FillRectangle(MainColor, leftX, eyeY, 10, 10);
            SplashKit.FillRectangle(MainColor, rightX, eyeY, 10, 10);
            SplashKit.FillRectangle(MainColor, leftX, mouthY, 25, 10);
            SplashKit.FillRectangle(Color.Black, leftX + 2, mouthY + 2, 21, 6);
        }
    }
}