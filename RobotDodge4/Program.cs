using SplashKitSDK;
using System;

namespace RobotDodge
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Window myWindow = new Window("Robot Dodge", 800, 600);
            Player player = new Player(myWindow);

            // 🔥 MAIN GAME LOOP
            while (!myWindow.CloseRequested && !player.Quit)
            {
                SplashKit.ProcessEvents();

                player.HandleInput();
                player.StayOnWindow(myWindow);

                myWindow.Clear(Color.White);

                player.Draw();

                myWindow.Refresh(60);
            }
        }
    }
}