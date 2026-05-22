using System;
using SplashKitSDK;
#nullable disable

namespace RobotDodge
{
    public class Program
    {
        public static void Main()
        {
            Window window = new Window("RobotDodge", 600, 600);
            RobotDodgee robotDodge = new RobotDodgee(window);

            while (!window.CloseRequested || !robotDodge.Quit)
            {
                SplashKit.ProcessEvents();
                robotDodge.HandleInput();
                robotDodge.Draw();
                robotDodge.Update();

                if (robotDodge.Quit)
                {
                    window.Close();

                }

            }

        }
    }
}
