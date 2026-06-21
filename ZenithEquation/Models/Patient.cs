using SplashKitSDK;

namespace TriageGame.Models
{
    public class Patient
    {
        public string Name { get; private set; }
        public string Scenario { get; private set; }
        public TriageLevel Level { get; private set; }
        public Color SpriteColor { get; private set; }
        public string Icon { get; private set; }

        public Patient(string name, string scenario, TriageLevel level, Color spriteColor, string icon)
        {
            Name = name;
            Scenario = scenario;
            Level = level;
            SpriteColor = spriteColor;
            Icon = icon;
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
