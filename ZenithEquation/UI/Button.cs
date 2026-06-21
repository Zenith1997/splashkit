using SplashKitSDK;
using TriageGame.Models;

namespace TriageGame.UI
{
    public class Button
    {
        public Rectangle Rect { get; private set; }
        public string Text { get; private set; }
        public Color Color { get; private set; }
        public TriageLevel? Level { get; private set; }
        public bool IsSubmitButton { get; private set; }

        public Button(Rectangle rect, string text, Color color, TriageLevel? level, bool isSubmitButton)
        {
            Rect = rect;
            Text = text;
            Color = color;
            Level = level;
            IsSubmitButton = isSubmitButton;
        }

        public bool ContainsPoint(Point2D point)
        {
            return point.X >= Rect.X &&
                   point.X <= Rect.X + Rect.Width &&
                   point.Y >= Rect.Y &&
                   point.Y <= Rect.Y + Rect.Height;
        }
    }
}
