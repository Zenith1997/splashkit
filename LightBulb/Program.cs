using SplashKitSDK;

namespace LightBulb
{
    public class Program
    {
        public static void Main()
        {
            LightSimulator simulation = new LightSimulator();
            simulation.Run();
        }
    }

    public class LightSimulator
    {
        private Window _simWindow;
        private LightBulb _light;
        private LightSwitch _lightSwitch;
        public LightSimulator()
        {
            _light = new LightBulb(10, 10);
            _lightSwitch = new LightSwitch{ X = 250, Y = 400, ConnectedLight = _light };

        }

        public void Run()
        {
            _simWindow = new Window("My Lightroom", 600, 600);
            LoadResources();

            while (!_simWindow.CloseRequested)
            {
                Draw();
                HandleInput();
            }

            _simWindow.Close();
            _simWindow = null;
        }

        private void Draw()
        {
            _simWindow.Clear(_light.IsOn ? Color.Yellow : Color.Gray);
            _light.Draw();
            _lightSwitch.Draw();
            _simWindow.Refresh(60);
        }
        private void HandleInput()
        {
            SplashKit.ProcessEvents();

         if(_lightSwitch.IsUnderMouse && SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                _lightSwitch.Switch();
            }
        }

        private void LoadResources()
        {
            SplashKit.LoadBitmap("On", "medium-on-light.png");
            SplashKit.LoadBitmap("Off", "medium-off-light.png");
            SplashKit.LoadBitmap("SwitchOn", "switch-on.jpg");
            SplashKit.LoadBitmap("SwitchOff", "switch-off.jpg");
        }
    }

    public class LightBulb
    {
        private double _x, _y;
        private bool _isOn;
        public bool IsOn
        {
            get { return _isOn; }
        }

        public LightBulb(double x, double y)
        {
            _x = x;
            _y = y;
            _isOn = false;
        }

        public void TogglePower()
        {
            _isOn = !_isOn;
        }

        public Bitmap Image
        {
            get { return SplashKit.BitmapNamed(_isOn ? "On" : "Off"); }
        }

        public void Draw()
        {
            Image.Draw(_x, _y);
        }
    }

    public class LightSwitch
    {
        private LightBulb _connectedLight;
        private double _x, _y;
        private bool _isOn;

        public LightSwitch()
        {
            _isOn = false;
        }

        public Bitmap Image
        {
            get { return SplashKit.BitmapNamed(_isOn ? "SwitchOn" : "SwitchOff"); }
        }
        public double X
        {
            get { return _x; }
            set { _x = value; }
        }

        public double Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }
        public LightBulb ConnectedLight
        {
            get { return _connectedLight; }
            set { _connectedLight = value; }
        }
        //Switch method to toggle the switch and the connected light
        public void Switch()

        {
            _isOn = !_isOn;
            if (_connectedLight != null)
            {
                _connectedLight.TogglePower();
            }
        }

        public bool IsUnderMouse
        {
            get { return Image.PointCollision(_x, _y, SplashKit.MouseX(), SplashKit.MouseY()); }
        }

        public void Draw()
        {
            SplashKit.DrawBitmap(Image, _x, _y);
        }
    }

    
}
