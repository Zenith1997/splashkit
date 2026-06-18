using System;
using System.Collections.Generic;
using System.Linq;
using SplashKitSDK;

namespace TriageGame
{
    public enum TriageLevel
    {
        Red = 1,      // Emergency
        Orange = 2,   // Very urgent
        Yellow = 3,   // Urgent
        Green = 4     // Less urgent
    }

    public class Patient
    {
        public string Name { get; private set; }
        public string Scenario { get; private set; }
        public TriageLevel Level { get; private set; }
        public Rectangle CardRect { get; set; }
        public int TreatedOrder { get; set; }
        public Color SpriteColor { get; private set; }
        public string Icon { get; private set; }

        public Patient(string name, string scenario, TriageLevel level, Color spriteColor, string icon)
        {
            Name = name;
            Scenario = scenario;
            Level = level;
            SpriteColor = spriteColor;
            Icon = icon;
            TreatedOrder = 0;
        }

        public bool IsTreated()
        {
            return TreatedOrder > 0;
        }

        public bool ContainsPoint(Point2D point)
        {
            return point.X >= CardRect.X &&
                   point.X <= CardRect.X + CardRect.Width &&
                   point.Y >= CardRect.Y &&
                   point.Y <= CardRect.Y + CardRect.Height;
        }

        public string LevelText()
        {
            if (Level == TriageLevel.Red) return "RED - Emergency";
            if (Level == TriageLevel.Orange) return "ORANGE - Very Urgent";
            if (Level == TriageLevel.Yellow) return "YELLOW - Urgent";
            return "GREEN - Less Urgent";
        }
    }

    public class Program
    {
        private const int SCREEN_WIDTH = 1100;
        private const int SCREEN_HEIGHT = 720;

        private static List<Patient> _patients = new List<Patient>();
        private static int _nextTreatOrder = 1;
        private static bool _gameFinished = false;
        private static bool _playerWon = false;

        public static void Main()
        {
            Window window = new Window("Emergency Triage Training Game", SCREEN_WIDTH, SCREEN_HEIGHT);

            SetupRound();

            while (!SplashKit.QuitRequested())
            {
                SplashKit.ProcessEvents();

                HandleInput();

                DrawGame(window);

                window.Refresh(60);
            }

            window.Close();
        }

        private static void SetupRound()
        {
            _patients.Clear();

            _patients.Add(new Patient(
                "Patient A",
                "Chest pain, sweating, shortness of breath",
                TriageLevel.Red,
                SplashKit.RGBColor(230, 80, 80),
                "!"
            ));

            _patients.Add(new Patient(
                "Patient B",
                "Deep cut on arm, bleeding but awake",
                TriageLevel.Orange,
                SplashKit.RGBColor(255, 150, 60),
                "+"
            ));

            _patients.Add(new Patient(
                "Patient C",
                "Broken wrist, pain but stable",
                TriageLevel.Yellow,
                SplashKit.RGBColor(230, 210, 70),
                "B"
            ));

            _patients.Add(new Patient(
                "Patient D",
                "Small headache, walking and talking normally",
                TriageLevel.Green,
                SplashKit.RGBColor(80, 190, 100),
                "Z"
            ));

            // Position patient cards
            _patients[0].CardRect = SplashKit.RectangleFrom(70, 150, 460, 190);
            _patients[1].CardRect = SplashKit.RectangleFrom(570, 150, 460, 190);
            _patients[2].CardRect = SplashKit.RectangleFrom(70, 380, 460, 190);
            _patients[3].CardRect = SplashKit.RectangleFrom(570, 380, 460, 190);

            _nextTreatOrder = 1;
            _gameFinished = false;
            _playerWon = false;
        }

        private static void HandleInput()
        {
            if (_gameFinished)
            {
                if (SplashKit.KeyTyped(KeyCode.RKey))
                {
                    SetupRound();
                }

                return;
            }

            if (SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                Point2D mousePosition = SplashKit.MousePosition();

                foreach (Patient patient in _patients)
                {
                    if (patient.ContainsPoint(mousePosition) && !patient.IsTreated())
                    {
                        TreatPatient(patient);
                        break;
                    }
                }
            }

            if (AllPatientsTreated())
            {
                CheckResult();
            }
        }

        private static void TreatPatient(Patient patient)
        {
            patient.TreatedOrder = _nextTreatOrder;
            _nextTreatOrder++;
        }

        private static bool AllPatientsTreated()
        {
            foreach (Patient patient in _patients)
            {
                if (!patient.IsTreated())
                {
                    return false;
                }
            }

            return true;
        }

        private static void CheckResult()
        {
            List<Patient> selectedOrder = _patients
                .OrderBy(patient => patient.TreatedOrder)
                .ToList();

            List<Patient> correctOrder = _patients
                .OrderBy(patient => (int)patient.Level)
                .ToList();

            _playerWon = true;

            for (int i = 0; i < selectedOrder.Count; i++)
            {
                if (selectedOrder[i] != correctOrder[i])
                {
                    _playerWon = false;
                    break;
                }
            }

            _gameFinished = true;
        }

        private static void DrawGame(Window window)
        {
            window.Clear(SplashKit.RGBColor(245, 247, 250));

            DrawHeader();

            foreach (Patient patient in _patients)
            {
                DrawPatientCard(patient);
            }

            DrawInstructionPanel();

            if (_gameFinished)
            {
                DrawResultScreen();
            }
        }

        private static void DrawHeader()
        {
            SplashKit.DrawText("Emergency Triage Training Game", Color.Black, 70, 35);
            SplashKit.DrawText("Click the patients in the correct treatment order.", Color.Black, 70, 70);
            SplashKit.DrawText("Priority: RED -> ORANGE -> YELLOW -> GREEN", Color.Black, 70, 100);
        }

        private static void DrawPatientCard(Patient patient)
        {
            Rectangle rect = patient.CardRect;

            Color cardColor = Color.White;

            if (patient.IsTreated())
            {
                cardColor = SplashKit.RGBColor(220, 235, 255);
            }

            SplashKit.FillRectangle(cardColor, rect);
            SplashKit.DrawRectangle(Color.Black, rect);

            DrawPatientSprite(patient, rect.X + 35, rect.Y + 35);

            SplashKit.DrawText(patient.Name, Color.Black, rect.X + 140, rect.Y + 30);
            SplashKit.DrawText(patient.Scenario, Color.Black, rect.X + 140, rect.Y + 65);

            if (patient.IsTreated())
            {
                SplashKit.DrawText("Treated order: " + patient.TreatedOrder, Color.Blue, rect.X + 140, rect.Y + 110);
            }
            else
            {
                SplashKit.DrawText("Click to treat", SplashKit.RGBColor(80, 80, 80), rect.X + 140, rect.Y + 110);
            }

            // This is hidden during the game idea, but useful while learning.
            // Later you can remove this line to make the game harder.
            SplashKit.DrawText("Triage: " + patient.LevelText(), SplashKit.RGBColor(90, 90, 90), rect.X + 140, rect.Y + 145);
        }

        private static void DrawPatientSprite(Patient patient, double x, double y)
        {
            // Head
            SplashKit.FillCircle(SplashKit.RGBColor(240, 200, 170), x + 40, y + 25, 25);
            SplashKit.DrawCircle(Color.Black, x + 40, y + 25, 25);

            // Body
            SplashKit.FillRectangle(patient.SpriteColor, x + 15, y + 55, 50, 70);
            SplashKit.DrawRectangle(Color.Black, x + 15, y + 55, 50, 70);

            // Legs
            SplashKit.FillRectangle(SplashKit.RGBColor(70, 90, 130), x + 15, y + 125, 20, 35);
            SplashKit.FillRectangle(SplashKit.RGBColor(70, 90, 130), x + 45, y + 125, 20, 35);

            // Simple face
            SplashKit.FillCircle(Color.Black, x + 30, y + 20, 3);
            SplashKit.FillCircle(Color.Black, x + 50, y + 20, 3);
            SplashKit.DrawLine(Color.Black, x + 30, y + 38, x + 50, y + 38);

            // Medical icon
            SplashKit.FillCircle(Color.White, x + 75, y + 20, 20);
            SplashKit.DrawCircle(Color.Black, x + 75, y + 20, 20);
            SplashKit.DrawText(patient.Icon, Color.Black, x + 68, y + 10);
        }

        private static void DrawInstructionPanel()
        {
            SplashKit.FillRectangle(SplashKit.RGBColor(235, 235, 235), 70, 610, 960, 70);
            SplashKit.DrawRectangle(Color.Black, 70, 610, 960, 70);

            SplashKit.DrawText("How to play:", Color.Black, 90, 625);
            SplashKit.DrawText("Click patients from most urgent to least urgent. After all 4 are treated, the game checks your order.", Color.Black, 90, 650);
        }

        private static void DrawResultScreen()
        {
            SplashKit.FillRectangle(SplashKit.RGBAColor(0, 0, 0, 180), 0, 0, SCREEN_WIDTH, SCREEN_HEIGHT);

            SplashKit.FillRectangle(Color.White, 300, 210, 500, 270);
            SplashKit.DrawRectangle(Color.Black, 300, 210, 500, 270);

            if (_playerWon)
            {
                SplashKit.DrawText("YOU WIN!", SplashKit.RGBColor(0, 150, 70), 470, 250);
                SplashKit.DrawText("Correct triage order.", Color.Black, 430, 290);
            }
            else
            {
                SplashKit.DrawText("YOU LOSE!", Color.Red, 465, 250);
                SplashKit.DrawText("Wrong treatment order.", Color.Black, 425, 290);
            }

            SplashKit.DrawText("Correct order was:", Color.Black, 410, 335);

            List<Patient> correctOrder = _patients
                .OrderBy(patient => (int)patient.Level)
                .ToList();

            double y = 365;

            for (int i = 0; i < correctOrder.Count; i++)
            {
                Patient patient = correctOrder[i];
                SplashKit.DrawText(
                    (i + 1) + ". " + patient.Name + " - " + patient.LevelText(),
                    Color.Black,
                    365,
                    y
                );

                y += 25;
            }

            SplashKit.DrawText("Press R to restart.", Color.Blue, 445, 445);
        }
    }
}