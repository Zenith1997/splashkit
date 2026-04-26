using SplashKitSDK;
using System.Collections.Generic;

namespace RobotDodge
{
    public class RobotDodge
    {
        private Player _player;
        private Window _gameWindow;
        private List<Robot> _robots;

        public bool Quit => _player.Quit;

        public RobotDodge(Window gameWindow)
        {
            _gameWindow = gameWindow;
            _player = new Player(gameWindow);
            _robots = new List<Robot>();
        }

        public void HandleInput()
        {
            _player.HandleInput();
            _player.StayOnWindow(_gameWindow);
        }

        public void Update()
        {
            // Spawn new robots randomly
            if (SplashKit.Rnd() < 0.02)
            {
                _robots.Add(new Robot(_gameWindow, _player));
            }

            // Update all robots
            foreach (Robot r in _robots)
            {
                r.Update();
            }

            CheckCollisions();
        }

        private void CheckCollisions()
        {
            List<Robot> toRemove = new List<Robot>();

            foreach (Robot r in _robots)
            {
                if (_player.CollidedWith(r) || r.IsOffscreen(_gameWindow))
                {
                    toRemove.Add(r);
                }
            }

            foreach (Robot r in toRemove)
            {
                _robots.Remove(r);
            }
        }

        public void Draw()
        {
            _gameWindow.Clear(Color.White);

            foreach (Robot r in _robots)
            {
                r.Draw();
            }

            _player.Draw();

            _gameWindow.Refresh(60);
        }
    }
}