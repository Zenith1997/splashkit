using System.Collections.Generic;
using SplashKitSDK;
using TriageGame.Game;
using TriageGame.Models;

namespace TriageGame.UI
{
    public class GameRenderer
    {
        private const int SCREEN_WIDTH = 1100;
        private const int SCREEN_HEIGHT = 720;

        private Font _font;

        public GameRenderer()
        {
            _font = new Font("GameFont", "game_font.ttf");
        }

        public void DrawGame(Window window, GameManager gameManager, List<Button> buttons)
        {
            window.Clear(SplashKit.RGBColor(245, 247, 250));

            DrawHeader(gameManager);

            if (!gameManager.GameFinished && gameManager.CurrentPatient != null)
            {
                DrawCurrentPatient(gameManager);
                DrawAnswerButtons(buttons, gameManager.SelectedLevel);
            }

            DrawBottomInfo(gameManager);

            if (gameManager.GameFinished)
            {
                DrawResultScreen(gameManager);
            }
        }

        private void DrawText(string text, Color color, double x, double y, int size)
        {
            SplashKit.DrawText(text, color, _font, size, x, y);
        }

        private void DrawHeader(GameManager gameManager)
        {
            DrawText("Emergency Triage Quiz Game", Color.Black, 70, 25, 30);

            DrawText("Score: " + gameManager.Score, Color.Black, 70, 70, 22);
            DrawText("Lives: " + gameManager.Lives, Color.Red, 210, 70, 22);
            DrawText("Time Left: " + gameManager.TimeLeft() + "s", Color.Black, 340, 70, 22);

            DrawText(
                "Choose the correct Australian Triage Scale category.",
                Color.Black,
                70,
                110,
                20
            );

            DrawText(
                "Select an answer first. Then press SUBMIT ANSWER.",
                SplashKit.RGBColor(40, 70, 140),
                70,
                140,
                20
            );
        }

        private void DrawCurrentPatient(GameManager gameManager)
        {
            Patient patient = gameManager.CurrentPatient;

            Rectangle cardRect = SplashKit.RectangleFrom(120, 185, 860, 235);

            SplashKit.FillRectangle(Color.White, cardRect);
            SplashKit.DrawRectangle(Color.Black, cardRect);

            DrawPatientSprite(patient, cardRect.X + 55, cardRect.Y + 35);

            DrawText(patient.Name, Color.Black, cardRect.X + 215, cardRect.Y + 35, 26);

            DrawText("Scenario:", Color.Black, cardRect.X + 215, cardRect.Y + 80, 22);

            DrawText(
                patient.Scenario,
                Color.Black,
                cardRect.X + 215,
                cardRect.Y + 115,
                21
            );

            DrawText(
                "Patient " + (gameManager.CurrentPatientIndex + 1) + " of " + gameManager.TotalPatients(),
                SplashKit.RGBColor(80, 80, 80),
                cardRect.X + 215,
                cardRect.Y + 165,
                20
            );
        }

        private void DrawAnswerButtons(List<Button> buttons, TriageLevel? selectedLevel)
        {
            foreach (Button button in buttons)
            {
                bool selected = false;

                if (button.Level != null && selectedLevel != null && button.Level == selectedLevel)
                {
                    selected = true;
                }

                DrawButton(button, selected);
            }
        }

        private void DrawButton(Button button, bool selected)
        {
            Color fillColor = button.Color;

            if (button.IsSubmitButton)
            {
                fillColor = SplashKit.RGBColor(30, 90, 180);
            }

            SplashKit.FillRectangle(fillColor, button.Rect);
            SplashKit.DrawRectangle(Color.Black, button.Rect);

            if (selected)
            {
                SplashKit.DrawRectangle(Color.Black, button.Rect.X - 4, button.Rect.Y - 4, button.Rect.Width + 8, button.Rect.Height + 8);
                SplashKit.DrawRectangle(Color.Black, button.Rect.X - 8, button.Rect.Y - 8, button.Rect.Width + 16, button.Rect.Height + 16);
            }

            Color textColor = Color.Black;

            if (button.IsSubmitButton)
            {
                textColor = Color.White;
            }

            DrawText(
                button.Text,
                textColor,
                button.Rect.X + 25,
                button.Rect.Y + 22,
                20
            );
        }

        private void DrawBottomInfo(GameManager gameManager)
        {
            SplashKit.FillRectangle(SplashKit.RGBColor(235, 235, 235), 70, 610, 960, 80);
            SplashKit.DrawRectangle(Color.Black, 70, 610, 960, 80);

            DrawText("Rules:", Color.Black, 90, 622, 20);

            DrawText(
                "Correct answer = +1 mark. Wrong answer = lose 1 life. You have 60 seconds.",
                Color.Black,
                90,
                650,
                18
            );

            if (gameManager.LastMessage != "")
            {
                DrawText(gameManager.LastMessage, SplashKit.RGBColor(20, 60, 160), 90, 570, 20);
            }
        }

        private void DrawPatientSprite(Patient patient, double x, double y)
        {
            SplashKit.FillCircle(SplashKit.RGBColor(240, 200, 170), x + 40, y + 25, 25);
            SplashKit.DrawCircle(Color.Black, x + 40, y + 25, 25);

            SplashKit.FillRectangle(patient.SpriteColor, x + 15, y + 55, 50, 70);
            SplashKit.DrawRectangle(Color.Black, x + 15, y + 55, 50, 70);

            SplashKit.FillRectangle(SplashKit.RGBColor(70, 90, 130), x + 15, y + 125, 20, 35);
            SplashKit.FillRectangle(SplashKit.RGBColor(70, 90, 130), x + 45, y + 125, 20, 35);

            SplashKit.FillCircle(Color.Black, x + 30, y + 20, 3);
            SplashKit.FillCircle(Color.Black, x + 50, y + 20, 3);
            SplashKit.DrawLine(Color.Black, x + 30, y + 38, x + 50, y + 38);

            SplashKit.FillCircle(Color.White, x + 75, y + 20, 20);
            SplashKit.DrawCircle(Color.Black, x + 75, y + 20, 20);
            DrawText(patient.Icon, Color.Black, x + 67, y + 4, 22);
        }

        private void DrawResultScreen(GameManager gameManager)
        {
            SplashKit.FillRectangle(SplashKit.RGBAColor(0, 0, 0, 180), 0, 0, SCREEN_WIDTH, SCREEN_HEIGHT);

            SplashKit.FillRectangle(Color.White, 280, 190, 550, 350);
            SplashKit.DrawRectangle(Color.Black, 280, 190, 550, 350);

            DrawText("GAME OVER", Color.Black, 445, 230, 30);

            DrawText(
                "Final Score: " + gameManager.Score + " / " + gameManager.TotalPatients(),
                Color.Black,
                405,
                290,
                24
            );

            DrawText("Lives Left: " + gameManager.Lives, Color.Black, 445, 330, 22);

            DrawText("Message:", Color.Black, 360, 375, 20);
            DrawText(gameManager.LastMessage, SplashKit.RGBColor(20, 60, 160), 360, 405, 18);

            if (gameManager.Score == gameManager.TotalPatients())
            {
                DrawText("Excellent triage decisions!", SplashKit.RGBColor(0, 150, 70), 380, 455, 22);
            }
            else if (gameManager.Score >= 10)
            {
                DrawText("Good attempt. Keep practising.", Color.Blue, 380, 455, 22);
            }
            else
            {
                DrawText("You need more triage practice.", Color.Red, 380, 455, 22);
            }

            DrawText("Press R to restart.", Color.Blue, 420, 500, 22);
        }
    }
}
