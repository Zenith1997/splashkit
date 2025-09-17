using SplashKitSDK;

namespace CharacterDrawing1
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
        public LightSimulator()
        {
            _light = new LightBulb(10, 10);
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
            _simWindow.Clear(Color.White);
            _light.Draw();
            _simWindow.Refresh(60);
        }
        private void HandleInput()
        {
            SplashKit.ProcessEvents();
            if (SplashKit.KeyTyped(KeyCode.SpaceKey))
            {
              _light.TogglePower();
            }
        }

        private void LoadResources()
        {
            SplashKit.LoadBitmap("On", "medium-on-light.png");
            SplashKit.LoadBitmap("Off", "medium-off-light.png");
        }
    }

    public class LightBulb
    {
        private double _x, _y;
        private bool _isOn;

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
}
