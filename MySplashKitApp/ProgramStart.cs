using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using SplashKitSDK;

namespace MySplashKitApp
{
    public class Program
    {
        public static void Main()
        {
            SpaceGame game = new SpaceGame();
            game.Run();
        }
    }

    public class SpaceGame
    {
        private SpaceShip _player;
        private Window _gameWindow;

        public SpaceGame()
        {
           // LoadResources();
            _player = new SpaceShip { X = 300, Y = 300 };
        }

       

        public void Run()
        {
            _gameWindow = new Window("BlastOff", 600, 600);
            while (!_gameWindow.CloseRequested)
            {
                Draw();
                HandleInput();

            }
            _gameWindow.Close();
            _gameWindow = null;
            Draw();
            SplashKit.Delay(2500);

            _player.Move(100, -70);
            Draw();
            SplashKit.Delay(2500);

            _player.Rotate(60);
            Draw();
            SplashKit.Delay(2500);

            _player.Move(100, 0);
            Draw();
            SplashKit.Delay(2500);

            _gameWindow.Close();
            _gameWindow = null;

            SplashKit.Delay(2500);
        }
        private void HandleInput()
        {
            SplashKit.ProcessEvents();
            if (SplashKit.KeyDown(KeyCode.UpKey))

            {
                _player.Move(5, 0);
            }
            if (SplashKit.KeyDown(KeyCode.DownKey))

            {
                _player.Move(-5, 0);
            }
            if (SplashKit.KeyDown(KeyCode.RightKey)) _player.Rotate(5);
              if (SplashKit.KeyDown(KeyCode.LeftKey)) _player.Rotate(-5);
        }
        private void Draw()
        {
            _gameWindow.Clear(Color.Black);
            _player.Draw();
            _gameWindow.Refresh(60);
        }
    }

    public class SpaceShip
    {
        private double _x, _y,_z;
        private double _angle;
        private Bitmap _shipBitmap;

        public SpaceShip()
        {
            LoadResources();
            _angle = 270;
            _shipBitmap = SplashKit.BitmapNamed("Pegasi");
        }
         private void LoadResources()
        {
           SplashKit.LoadBitmap("Aquarii", "Aquarii.png");
          SplashKit.LoadBitmap("Gliese", "Gliese.png");
            SplashKit.LoadBitmap("Pegasi", "Pegasi.png");
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

        public double Z
        {
            get { return _z; }
            set { _z = value; }
        }

        public double Angle
        {
            get { return _angle; }
            set { _angle = value; }
        }

        public void Rotate(double amount)
        {
            _angle = (_angle + amount) % 360;
        }

        public void Draw()
        {
            _shipBitmap.Draw(_x, _y, SplashKit.OptionRotateBmp(_angle));
        }

        public void Move(double amountForward, double amountStrafe)
        {
            Vector2D movement = new Vector2D();
            Matrix2D rotation = SplashKit.RotationMatrix(_angle);

            movement.X += amountForward;
            movement.Y += amountStrafe;

            movement = SplashKit.MatrixMultiply(rotation, movement);

            _x += movement.X;
            _y += movement.Y;
        }
    }
}
