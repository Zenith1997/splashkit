using SplashKitSDK;

namespace RobotDodge
{
    public class RobotDodge
    {
        private Player _player;
        private Window _gameWindow;
        private Robot _testRobot;

        public bool Quit
        {
            get { return _player.Quit; }
        }

        public RobotDodge(Window gameWindow)
        {
            _gameWindow = gameWindow;
            _player = new Player(gameWindow);
            _testRobot = RandomRobot();
        }

        public void HandleInput()
        {
            _player.HandleInput();
            _player.StayOnWindow(_gameWindow);
        }

        public void Update()
        {
            if (_player.CollidedWith(_testRobot))
            {
                _testRobot = RandomRobot();
            }
        }

        public void Draw()
        {
            _gameWindow.Clear(Color.White);

            _testRobot.Draw();
            _player.Draw();

            _gameWindow.Refresh(60);
        }

        public Robot RandomRobot()
        {
            return new Robot(_gameWindow);
        }
    }
}