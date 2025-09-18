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

            if (SplashKit.KeyTyped(KeyCode.Num1Key)) _player.ShipKind = SpaceShip.ShipType.Aquarii;
            if (SplashKit.KeyTyped(KeyCode.Num2Key)) _player.ShipKind = SpaceShip.ShipType.Gliese;
            if (SplashKit.KeyTyped(KeyCode.Num3Key)) _player.ShipKind = SpaceShip.ShipType.Pegasi;


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
        private double _x, _y, _z;
        private double _angle;
        private Bitmap _shipBitmap;

        private ShipType _kind;

        public enum ShipType
        {
            Aquarii,
            Gliese,
            Pegasi
        }
        public SpaceShip()
        {
            LoadResources();
            _angle = 270;
            ShipKind = ShipType.Aquarii;
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
        public ShipType ShipKind
        {
            get { return _kind; }
            set
            {
                _kind = value;
                SetShipBitmap();
            }
        }
        public double Y
        {
            get { return _y; }
            set { _y = value; }

        }
        private void SetShipBitmap()
        {
            switch (_kind)
            {
                case ShipType.Aquarii:
                    _shipBitmap = SplashKit.BitmapNamed("Aquarii");
                    break;
                case ShipType.Gliese:
                    _shipBitmap = SplashKit.BitmapNamed("Gliese");
                    break;
                case ShipType.Pegasi:
                    _shipBitmap = SplashKit.BitmapNamed("Pegasi");
                    break;
                default:
                    _shipBitmap = SplashKit.BitmapNamed("Aquarii");
                    break;
            }
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


//Single Responsibility Principle applied in LightBulb/Program.cs
//Before refactoring, the LightBulb class handled both the state of the light bulb and the drawing logic.
//After refactoring, the LightBulb class only manages the state, while the Program class handles drawing and input.
//This separation of concerns makes the code more maintainable and adheres to the Single Responsibility Principle
// by ensuring each class has a single reason to change.
//Single responsibility principla says that a class should have only one reason to change, meaning it should only have one job or responsibility.