using System;
using SplashKitSDK;

namespace MakeAScene
{
    public class Program
    {
        public static void Main()
        {
            Window myWindow = new Window("Make a Scene!", 500, 400);

            Bitmap scene1 = new Bitmap("scene1", "scene1.png");
            Bitmap scene2 = new Bitmap("scene2", "scene2.png");
            Bitmap scene3 = new Bitmap("scene3", "scene3.png");
            Bitmap scene4 = new Bitmap("scene4", "scene4.png");

            SoundEffect sound1 = new SoundEffect("sound1", "sound1.wav");
            SoundEffect sound2 = new SoundEffect("sound2", "sound2.wav");

            // Scene 1
            myWindow.Clear(Color.White);
            myWindow.DrawBitmap(scene1, 0, 0);
            myWindow.Refresh(60);
            SplashKit.Delay(2000);

            // Scene 2
            myWindow.Clear(Color.White);
            myWindow.DrawBitmap(scene2, 0, 0);
            myWindow.Refresh(60);
            sound1.Play();
            SplashKit.Delay(2000);

            // Scene 3
            myWindow.Clear(Color.White);
            myWindow.DrawBitmap(scene3, 0, 0);
            myWindow.Refresh(60);
            SplashKit.Delay(2000);

            // Scene 4
            myWindow.Clear(Color.White);
            myWindow.DrawBitmap(scene4, 0, 0);
            myWindow.Refresh(60);
            sound2.Play();
            SplashKit.Delay(3000);

            // Keep window open until user closes it
            while (!myWindow.CloseRequested)
            {
                SplashKit.ProcessEvents();
            }
        }
    }
}