using System;
using System.Security.Principal;
using SplashKitSDK;


namespace RobotDodge{


public class Program
{
    public static void Main(string[] args)
    {
      Window myWindow = new Window("Game",500,500);
      Player player = new Player(myWindow);
      player.Draw();
      myWindow.Refresh(60);
      SplashKit.Delay(5000);
    }
}}