using SplashKitSDK;

namespace RobotDodge
{
    public class Robot
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public Color MainColor { get; private set; }

        public int Width
        {
            get { return 50; }
        }

        public int Height
        {
            get { return 50; }
        }

        public Circle CollisionCircle
        {
            get
            {
                return SplashKit.CircleAt(X + Width / 2, Y + Height / 2, 20);
            }
        }

        public Robot(Window gameWindow)
        {
            X = SplashKit.Rnd(gameWindow.Width - Width);
            Y = SplashKit.Rnd(gameWindow.Height - Height);
            MainColor = SplashKit.RandomRGBColor(200);
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
            SplashKit.FillRectangle(MainColor, leftX + 2, mouthY + 2, 21, 6);
        }
    }
}