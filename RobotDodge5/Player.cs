using SplashKitSDK;

namespace RobotDodge
{
    public class Player
    {
        private Bitmap _playerBitmap;
        private bool _quit;

        public double X { get; private set; }
        public double Y { get; private set; }

        public bool Quit => _quit;

        public int Width => _playerBitmap.Width;
        public int Height => _playerBitmap.Height;

        public Player(Window gameWindow)
        {
            _playerBitmap = new Bitmap("Player", "Player.png");
            X = (gameWindow.Width - Width) / 2;
            Y = (gameWindow.Height - Height) / 2;
        }

        public void Draw()
        {
            _playerBitmap.Draw(X, Y);
        }

        public void HandleInput()
        {
            const int SPEED = 4;

            if (SplashKit.KeyDown(KeyCode.LeftKey)) X -= SPEED;
            if (SplashKit.KeyDown(KeyCode.RightKey)) X += SPEED;
            if (SplashKit.KeyDown(KeyCode.UpKey)) Y -= SPEED;
            if (SplashKit.KeyDown(KeyCode.DownKey)) Y += SPEED;

            if (SplashKit.KeyTyped(KeyCode.EscapeKey))
                _quit = true;
        }

        public void StayOnWindow(Window window)
        {
            if (X < 0) X = 0;
            if (Y < 0) Y = 0;

            if (X > window.Width - Width)
                X = window.Width - Width;

            if (Y > window.Height - Height)
                Y = window.Height - Height;
        }

        public bool CollidedWith(Robot other)
        {
            return _playerBitmap.CircleCollision(X, Y, other.CollisionCircle);
        }
    }
}