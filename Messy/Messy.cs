using System;
using SplashKitSDK;


namespace SpaceShooter
{
    public class Program
    {
        public static void Main()
        {
            SpaceGame game = new SpaceGame();
            game.Run();
        }
    }

    /// <summary>
    /// Top-level class that owns the game window and drives the main game loop.
    /// </summary>
    public class SpaceGame
    {
        private Ship _ship;
        private Window _gameWindow;

        public SpaceGame()
        {
            LoadBitmaps();
            _ship = new Ship { X = 110, Y = 110 };
        }

        /// <summary>Loads all bitmap assets used by the game.</summary>
        private void LoadBitmaps()
        {
            SplashKit.LoadBitmap("Bullet", "Aquarii.png");
            SplashKit.LoadBitmap("Gliese",  "Gliese.png");
            SplashKit.LoadBitmap("Pegasi",  "Pegasi.png");
            SplashKit.LoadBitmap("Ship",    "Fire.png");
        }

        /// <summary>Runs the main game loop until the window is closed.</summary>
        public void Run()
        {
            _gameWindow = new Window("BlastOff", 600, 600);

            while (!_gameWindow.CloseRequested)
            {
                SplashKit.ProcessEvents();
                HandleInput();
                _ship.UpdateBullet();
                Draw();
            }

            _gameWindow.Close();
            _gameWindow = null;
        }

        /// <summary>Translates keyboard input into ship movement and firing.</summary>
        private void HandleInput()
        {
            if (SplashKit.KeyDown(KeyCode.UpKey))    _ship.MoveForward(4);
            if (SplashKit.KeyDown(KeyCode.DownKey))  _ship.MoveForward(-4);
            if (SplashKit.KeyDown(KeyCode.LeftKey))  _ship.Rotate(-4);
            if (SplashKit.KeyDown(KeyCode.RightKey)) _ship.Rotate(4);
            if (SplashKit.KeyTyped(KeyCode.SpaceKey)) _ship.Shoot();
        }

        /// <summary>Clears the screen, draws all game objects, then refreshes.</summary>
        private void Draw()
        {
            _gameWindow.Clear(Color.Black);
            _ship.Draw();
            _gameWindow.Refresh(60);
        }
    }

    /// <summary>
    /// Represents the player's ship. Handles movement, rotation, drawing,
    /// and firing a single bullet.
    /// </summary>
    public class Ship
    {
        private double _x, _y;
        private double _angle;
        private Bitmap _shipBitmap;
        private Bullet _bullet = new Bullet();  // starts inactive

        public Ship()
        {
            // 270° points the ship upward in SplashKit's coordinate system.
            _angle       = 270;
            _shipBitmap  = SplashKit.BitmapNamed("Ship");
        }

        public double X
        {
            get { return _x; }
            set { _x = value; }
        }

        public double Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public double Angle
        {
            get { return _angle; }
            set { _angle = value; }
        }

        /// <summary>Rotates the ship by the given number of degrees.</summary>
        public void Rotate(double degrees)
        {
            _angle = (_angle + degrees) % 360;
        }

        /// <summary>
        /// Moves the ship forward (positive) or backward (negative) along
        /// the direction it is currently facing.
        /// </summary>
        public void MoveForward(double amount)
        {
            Vector2D movement = new Vector2D();
            Matrix2D rotation = SplashKit.RotationMatrix(_angle);

            movement.X += amount;
            movement    = SplashKit.MatrixMultiply(rotation, movement);

            _x += movement.X;
            _y += movement.Y;
        }

        /// <summary>
        /// Fires a bullet from the front tip of the ship, travelling in the
        /// direction the ship is currently facing.
        /// </summary>
        public void Shoot()
        {
            // Build a composite transform that accounts for the ship's
            // pivot (centre of bitmap), rotation, and screen position.
            Matrix2D anchorMatrix = SplashKit.TranslationMatrix(
                SplashKit.PointAt(_shipBitmap.Width / 2, _shipBitmap.Height / 2));

            // Move centre point to origin ...
            Matrix2D transform = SplashKit.MatrixMultiply(
                SplashKit.IdentityMatrix(),
                SplashKit.MatrixInverse(anchorMatrix));

            // ... rotate around origin ...
            transform = SplashKit.MatrixMultiply(transform, SplashKit.RotationMatrix(_angle));

            // ... then move back to bitmap space ...
            transform = SplashKit.MatrixMultiply(transform, anchorMatrix);

            // ... finally translate to the ship's screen position.
            transform = SplashKit.MatrixMultiply(
                transform,
                SplashKit.TranslationMatrix(_x, _y));

            // The "front tip" of the ship bitmap is the right-centre edge.
            Vector2D tipPosition = new Vector2D();
            tipPosition.X = _shipBitmap.Width;
            tipPosition.Y = _shipBitmap.Height / 2;
            tipPosition   = SplashKit.MatrixMultiply(transform, tipPosition);

            _bullet = new Bullet(tipPosition.X, tipPosition.Y, _angle);
        }

        /// <summary>Steps the active bullet forward by one frame.</summary>
        public void UpdateBullet()
        {
            _bullet.Update();
        }

        /// <summary>Draws the ship and its bullet to the screen.</summary>
        public void Draw()
        {
            _shipBitmap.Draw(_x, _y, SplashKit.OptionRotateBmp(_angle));
            _bullet.Draw();
        }
    }

    /// <summary>
    /// A single projectile fired by the ship. Travels in a straight line
    /// in the direction it was created with, then deactivates when it leaves
    /// the screen.
    /// </summary>
    public class Bullet
    {
        private const int BULLET_SPEED = 8;

        private Bitmap _bulletBitmap;
        private double _x, _y, _angle;
        private bool   _active;

        /// <summary>Creates an inactive placeholder bullet (not yet fired).</summary>
        public Bullet()
        {
            _active = false;
        }

        /// <summary>Creates an active bullet at the given position and heading.</summary>
        public Bullet(double x, double y, double angle)
        {
            _bulletBitmap = SplashKit.BitmapNamed("Bullet");

            // Centre the bitmap on the spawn point.
            _x      = x - _bulletBitmap.Width  / 2;
            _y      = y - _bulletBitmap.Height / 2;
            _angle  = angle;
            _active = true;
        }

        /// <summary>
        /// Moves the bullet forward along its heading. Deactivates the bullet
        /// if it travels outside the screen boundaries.
        /// </summary>
        public void Update()
        {
            if (!_active) return;

            // BUG FIX: was "_angle + 90" (i.e. 45 * 2), which added a 90-degree
            // offset to the bullet's travel direction. It now correctly uses _angle.
            Vector2D movement = new Vector2D();
            Matrix2D rotation = SplashKit.RotationMatrix(_angle);
            movement.X += BULLET_SPEED;
            movement    = SplashKit.MatrixMultiply(rotation, movement);

            _x += movement.X;
            _y += movement.Y;

            bool offScreen =
                _x > SplashKit.ScreenWidth()  ||
                _x < 0                         ||
                _y > SplashKit.ScreenHeight()  ||
                _y < 0;

            if (offScreen)
                _active = false;
        }

        /// <summary>Draws the bullet to the screen if it is currently active.</summary>
        public void Draw()
        {
            if (_active)
            {
                DrawingOptions options = SplashKit.OptionRotateBmp(_angle);
                _bulletBitmap.Draw(_x, _y, options);
            }
        }
    }
}
