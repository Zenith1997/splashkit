using System.Collections.Generic;
using SplashKitSDK;
using TriageGame.Game;
using TriageGame.Models;
using TriageGame.UI;

namespace TriageGame
{
    public class Program
    {
        private const int SCREEN_WIDTH = 1100;
        private const int SCREEN_HEIGHT = 720;

        public static void Main()
        {
            Window window = new Window("Emergency Triage Quiz Game", SCREEN_WIDTH, SCREEN_HEIGHT);

            GameManager gameManager = new GameManager();
            GameRenderer renderer = new GameRenderer();

            List<Button> buttons = CreateButtons();

            while (!SplashKit.QuitRequested())
            {
                SplashKit.ProcessEvents();

                HandleInput(gameManager, buttons);

                gameManager.UpdateGameState();

                renderer.DrawGame(window, gameManager, buttons);

                window.Refresh(60);
            }

            window.Close();
        }

        private static List<Button> CreateButtons()
        {
            List<Button> buttons = new List<Button>();

            buttons.Add(new Button(
                SplashKit.RectangleFrom(45, 455, 170, 70),
                "1",
                SplashKit.RGBColor(180, 0, 0),
                TriageLevel.Category1,
                false
            ));

            buttons.Add(new Button(
                SplashKit.RectangleFrom(225, 455, 170, 70),
                "2",
                SplashKit.RGBColor(230, 80, 80),
                TriageLevel.Category2,
                false
            ));

            buttons.Add(new Button(
                SplashKit.RectangleFrom(405, 455, 170, 70),
                "3",
                SplashKit.RGBColor(255, 150, 60),
                TriageLevel.Category3,
                false
            ));

            buttons.Add(new Button(
                SplashKit.RectangleFrom(585, 455, 170, 70),
                "4",
                SplashKit.RGBColor(230, 210, 70),
                TriageLevel.Category4,
                false
            ));

            buttons.Add(new Button(
                SplashKit.RectangleFrom(765, 455, 170, 70),
                "5",
                SplashKit.RGBColor(80, 190, 100),
                TriageLevel.Category5,
                false
            ));

            buttons.Add(new Button(
                SplashKit.RectangleFrom(405, 540, 290, 55),
                "SUBMIT",
                SplashKit.RGBColor(30, 90, 180),
                null,
                true
            ));

            return buttons;
        }

        private static void HandleInput(GameManager gameManager, List<Button> buttons)
        {
            if (gameManager.GameFinished)
            {
                if (SplashKit.KeyTyped(KeyCode.RKey))
                {
                    gameManager.SetupGame();
                }

                return;
            }

            if (SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                Point2D mousePosition = SplashKit.MousePosition();

                foreach (Button button in buttons)
                {
                    if (button.ContainsPoint(mousePosition))
                    {
                        if (button.IsSubmitButton)
                        {
                            gameManager.SubmitSelectedAnswer();
                        }
                        else if (button.Level != null)
                        {
                            gameManager.SelectAnswer(button.Level.Value);
                        }

                        break;
                    }
                }
            }
        }
    }
}
