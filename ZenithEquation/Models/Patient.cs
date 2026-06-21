namespace TriageGame.Models
{
    public class Patient
    {
        public string Name { get; private set; }
        public string Scenario { get; private set; }
        public TriageLevel Level { get; private set; }

        public string ImagePath { get; private set; }
        public string ImageName { get; private set; }

        public Patient(
            string name,
            string scenario,
            TriageLevel level,
            string imagePath,
            string imageName
        )
        {
            Name = name;
            Scenario = scenario;
            Level = level;
            ImagePath = imagePath;
            ImageName = imageName;
        }

        public string LevelText()
        {
            if (Level == TriageLevel.Category1)
            {
                return "Category 1 - Resuscitation - Immediate";
            }

            if (Level == TriageLevel.Category2)
            {
                return "Category 2 - Emergency - within 10 minutes";
            }

            if (Level == TriageLevel.Category3)
            {
                return "Category 3 - Urgent - within 30 minutes";
            }

            if (Level == TriageLevel.Category4)
            {
                return "Category 4 - Semi-urgent - within 60 minutes";
            }

            return "Category 5 - Non-urgent - within 120 minutes";
        }
    }
}
