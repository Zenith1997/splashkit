namespace TriageGame.Models
{
    public class Response
    {
        public string PatientName { get; private set; }
        public string Scenario { get; private set; }
        public TriageLevel SelectedLevel { get; private set; }
        public TriageLevel CorrectLevel { get; private set; }
        public bool IsCorrect { get; private set; }

        public Response(
            string patientName,
            string scenario,
            TriageLevel selectedLevel,
            TriageLevel correctLevel
        )
        {
            PatientName = patientName;
            Scenario = scenario;
            SelectedLevel = selectedLevel;
            CorrectLevel = correctLevel;
            IsCorrect = selectedLevel == correctLevel;
        }

        public string SelectedText()
        {
            return LevelToShortText(SelectedLevel);
        }

        public string CorrectText()
        {
            return LevelToShortText(CorrectLevel);
        }

        public string ResultText()
        {
            if (IsCorrect)
            {
                return "Correct";
            }

            return "Wrong";
        }

        private string LevelToShortText(TriageLevel level)
        {
            if (level == TriageLevel.Category1) return "Cat 1";
            if (level == TriageLevel.Category2) return "Cat 2";
            if (level == TriageLevel.Category3) return "Cat 3";
            if (level == TriageLevel.Category4) return "Cat 4";
            return "Cat 5";
        }
    }
}
