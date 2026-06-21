using System.Collections.Generic;
using System.IO;
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
        private Dictionary<string, Bitmap> _loadedBitmaps;

        public GameRenderer()
        {
            _font = new Font("GameFont", "game_font.ttf");
            _loadedBitmaps = new Dictionary<string, Bitmap>();
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
                570,
                140,
                10
            );
        }

        private void DrawCurrentPatient(GameManager gameManager)
        {
            Patient patient = gameManager.CurrentPatient!;

            Rectangle cardRect = SplashKit.RectangleFrom(90, 180, 920, 245);

            SplashKit.FillRectangle(Color.White, cardRect);
            SplashKit.DrawRectangle(Color.Black, cardRect);

            DrawPatientImage(patient, cardRect.X + 25, cardRect.Y + 25, 280, 170);

            DrawText(patient.Name, Color.Black, cardRect.X + 335, cardRect.Y + 35, 26);

            DrawText("Scenario:", Color.Black, cardRect.X + 335, cardRect.Y + 80, 22);

            DrawText(
                patient.Scenario,
                Color.Black,
                cardRect.X + 335,
                cardRect.Y + 115,
                21
            );

            DrawText(
                "Patient " + (gameManager.CurrentPatientIndex + 1) + " of " + gameManager.TotalPatients(),
                SplashKit.RGBColor(80, 80, 80),
                cardRect.X + 335,
                cardRect.Y + 165,
                20
            );
        }

        private Bitmap? GetPatientBitmap(Patient patient)
        {
            if (_loadedBitmaps.ContainsKey(patient.ImageName))
            {
                return _loadedBitmaps[patient.ImageName];
            }

            if (!File.Exists(patient.ImagePath))
            {
                return null;
            }

            Bitmap bitmap = SplashKit.LoadBitmap(patient.ImageName, patient.ImagePath);
            _loadedBitmaps[patient.ImageName] = bitmap;

            return bitmap;
        }

        private void DrawPatientImage(Patient patient, double x, double y, double width, double height)
        {
            Bitmap? bitmap = GetPatientBitmap(patient);

            if (bitmap == null)
            {
                DrawMissingImagePlaceholder(patient, x, y, width, height);
                return;
            }

            double scaleX = width / bitmap.Width;
            double scaleY = height / bitmap.Height;

            SplashKit.DrawBitmap(
                bitmap,
                x,
                y,
                SplashKit.OptionScaleBmp(scaleX, scaleY)
            );

            SplashKit.DrawRectangle(Color.Black, x, y, width, height);
        }

        private void DrawMissingImagePlaceholder(Patient patient, double x, double y, double width, double height)
        {
            SplashKit.FillRectangle(SplashKit.RGBColor(230, 230, 230), x, y, width, height);
            SplashKit.DrawRectangle(Color.Black, x, y, width, height);

            DrawText("Missing image", Color.Red, x + 25, y + 55, 18);
            DrawText(patient.ImagePath, Color.Black, x + 15, y + 95, 13);
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
