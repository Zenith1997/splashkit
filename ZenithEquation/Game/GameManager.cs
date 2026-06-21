using System;
using System.Collections.Generic;
using TriageGame.Models;

namespace TriageGame.Game
{
    public class GameManager
    {
        private const int GAME_TIME_SECONDS = 60;

        private List<Patient> _patients;
        private List<Response> _responseHistory;

        private int _currentPatientIndex;
        private int _score;
        private int _lives;
        private bool _gameFinished;
        private string _lastMessage = "";
        private DateTime _startTime;
        private TriageLevel? _selectedLevel;

        public GameManager()
        {
            _patients = new List<Patient>();
            _responseHistory = new List<Response>();
            SetupGame();
        }

        public int CurrentPatientIndex
        {
            get { return _currentPatientIndex; }
        }

        public int Score
        {
            get { return _score; }
        }

        public int Lives
        {
            get { return _lives; }
        }

        public bool GameFinished
        {
            get { return _gameFinished; }
        }

        public string LastMessage
        {
            get { return _lastMessage; }
        }

        public TriageLevel? SelectedLevel
        {
            get { return _selectedLevel; }
        }

        public List<Response> ResponseHistory
        {
            get { return _responseHistory; }
        }

        public Patient? CurrentPatient
        {
            get
            {
                if (_currentPatientIndex >= _patients.Count)
                {
                    return null;
                }

                return _patients[_currentPatientIndex];
            }
        }

        public void SetupGame()
        {
            _patients.Clear();
            _responseHistory.Clear();

            AddCategory1Patients();
            AddCategory2Patients();
            AddCategory3Patients();
            AddCategory4Patients();
            AddCategory5Patients();

            _currentPatientIndex = 0;
            _score = 0;
            _lives = 3;
            _gameFinished = false;
            _lastMessage = "Select a category, then press SUBMIT ANSWER.";
            _selectedLevel = null;
            _startTime = DateTime.Now;
        }

        private Patient CreatePatient(
            int patientNumber,
            string scenario,
            TriageLevel level
        )
        {
            return new Patient(
                "Patient " + patientNumber,
                scenario,
                level,
                "Resources/images/patient" + patientNumber + ".png",
                "PatientImage" + patientNumber
            );
        }

        private void AddCategory1Patients()
        {
            _patients.Add(CreatePatient(
                1,
                "Not breathing and unresponsive.",
                TriageLevel.Category1
            ));

            _patients.Add(CreatePatient(
                2,
                "Severe airway obstruction and turning blue.",
                TriageLevel.Category1
            ));

            _patients.Add(CreatePatient(
                3,
                "Unconscious after major trauma with a very weak pulse.",
                TriageLevel.Category1
            ));

            _patients.Add(CreatePatient(
                4,
                "Sudden collapse and not responding to voice.",
                TriageLevel.Category1
            ));
        }

        private void AddCategory2Patients()
        {
            _patients.Add(CreatePatient(
                5,
                "Severe chest pain with sweating and shortness of breath.",
                TriageLevel.Category2
            ));

            _patients.Add(CreatePatient(
                6,
                "Severe breathing difficulty but still conscious.",
                TriageLevel.Category2
            ));

            _patients.Add(CreatePatient(
                7,
                "Severe pain after a major leg injury.",
                TriageLevel.Category2
            ));

            _patients.Add(CreatePatient(
                8,
                "Confused and drowsy after possible poisoning.",
                TriageLevel.Category2
            ));
        }

        private void AddCategory3Patients()
        {
            _patients.Add(CreatePatient(
                9,
                "Moderate bleeding from a deep cut, awake and stable.",
                TriageLevel.Category3
            ));

            _patients.Add(CreatePatient(
                10,
                "Possible broken arm with strong pain.",
                TriageLevel.Category3
            ));

            _patients.Add(CreatePatient(
                11,
                "Vomiting many times and feeling very weak.",
                TriageLevel.Category3
            ));

            _patients.Add(CreatePatient(
                12,
                "High fever and severe headache, alert but unwell.",
                TriageLevel.Category3
            ));
        }

        private void AddCategory4Patients()
        {
            _patients.Add(CreatePatient(
                13,
                "Mild asthma symptoms and speaking in full sentences.",
                TriageLevel.Category4
            ));

            _patients.Add(CreatePatient(
                14,
                "Sprained ankle, can walk but has pain.",
                TriageLevel.Category4
            ));

            _patients.Add(CreatePatient(
                15,
                "Vomiting but drinking water and no severe weakness.",
                TriageLevel.Category4
            ));

            _patients.Add(CreatePatient(
                16,
                "Ear pain for two days, uncomfortable but stable.",
                TriageLevel.Category4
            ));
        }

        private void AddCategory5Patients()
        {
            _patients.Add(CreatePatient(
                17,
                "Small rash, no fever, feels well.",
                TriageLevel.Category5
            ));

            _patients.Add(CreatePatient(
                18,
                "Minor sore throat, eating and drinking normally.",
                TriageLevel.Category5
            ));

            _patients.Add(CreatePatient(
                19,
                "Small old bruise, walking normally.",
                TriageLevel.Category5
            ));

            _patients.Add(CreatePatient(
                20,
                "Needs a routine medical certificate, no urgent symptoms.",
                TriageLevel.Category5
            ));
        }

        public void SelectAnswer(TriageLevel selectedLevel)
        {
            if (_gameFinished)
            {
                return;
            }

            _selectedLevel = selectedLevel;
            _lastMessage = "Selected: " + GetShortLevelText(selectedLevel) + ". Press SUBMIT ANSWER.";
        }

        public void SubmitSelectedAnswer()
        {
            if (_gameFinished || CurrentPatient == null)
            {
                return;
            }

            if (_selectedLevel == null)
            {
                _lastMessage = "Please select a category before submitting.";
                return;
            }

            Patient currentPatient = CurrentPatient;
            TriageLevel selectedAnswer = _selectedLevel.Value;

            Response response = new Response(
                currentPatient.Name,
                currentPatient.Scenario,
                selectedAnswer,
                currentPatient.Level
            );

            _responseHistory.Add(response);

            if (response.IsCorrect)
            {
                _score++;
                _lastMessage = "Correct! +1 mark. Moving to next patient.";
            }
            else
            {
                _lives--;
                _lastMessage = "Wrong! Correct answer: " + currentPatient.LevelText();
            }

            _selectedLevel = null;
            _currentPatientIndex++;

            UpdateGameState();
        }

        public void UpdateGameState()
        {
            if (_gameFinished)
            {
                return;
            }

            if (TimeLeft() <= 0)
            {
                _gameFinished = true;
                _lastMessage = "Time is over!";
                return;
            }

            if (_lives <= 0)
            {
                _gameFinished = true;
                _lastMessage = "No lives left!";
                return;
            }

            if (_currentPatientIndex >= _patients.Count)
            {
                _gameFinished = true;
                _lastMessage = "All patients completed!";
            }
        }

        public int TimeLeft()
        {
            TimeSpan elapsed = DateTime.Now - _startTime;
            int timeLeft = GAME_TIME_SECONDS - (int)elapsed.TotalSeconds;

            if (timeLeft < 0)
            {
                return 0;
            }

            return timeLeft;
        }

        public int TotalPatients()
        {
            return _patients.Count;
        }

        public string GetShortLevelText(TriageLevel level)
        {
            if (level == TriageLevel.Category1) return "Category 1";
            if (level == TriageLevel.Category2) return "Category 2";
            if (level == TriageLevel.Category3) return "Category 3";
            if (level == TriageLevel.Category4) return "Category 4";
            return "Category 5";
        }
    }
}
