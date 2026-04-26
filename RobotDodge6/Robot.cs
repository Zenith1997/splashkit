using SplashKitSDK;

public abstract class Robot
{
    public double X { get; private set; }
    public double Y { get; private set; }
    public Color MainColor { get; private set; }
    public Vector2D Velocity { get; private set; }
    public int Width
    {
        get { return 50; }
    }
    public int Height
    {
        get { return 50; }
    }

    public Circle CollisionCircle
    {
        get; private set;
    }

    public Robot(Window gameWindow, Player player)
    {
        MainColor = Color.RandomRGB(200);

        const int speed = 4;

        // get a point for the robot
        Point2D fromPt = new Point2D() { X = X, Y = Y };

        // get a point for the player
        Point2D toPt = new Point2D()
        {
            X = player.X,
            Y = player.Y
        };

        //calculate the direction to head
        Vector2D dir;
        dir = SplashKit.UnitVector(SplashKit.VectorPointToPoint(fromPt, toPt));

        // randomly pick top/bottomor left/right
        if (SplashKit.Rnd() < 0.5)
        {

            //  top or bottom
            //  start by picking a random position left or right (X)
            X = SplashKit.Rnd(gameWindow.Width);

            // work out if top or bottom?
            if (SplashKit.Rnd() < 0.5)
            {
                Y = -Height; //top so above top
            }
            else
            {
                Y = gameWindow.Height; //below so below bottom
            }
        }
        else
        {
            //left or right
            Y = SplashKit.Rnd(gameWindow.Height);

            if (SplashKit.Rnd() < 0.5)
            {
                X = -Width;
            }
            else
            {
                X = gameWindow.Width;
            }
        }

        // set the speed and assign to the velocity
        Velocity = SplashKit.VectorMultiply(dir, speed);
    }

    public void Update()
    {
        X += Velocity.X;
        Y += Velocity.Y;
        CollisionCircle = SplashKit.CircleAt(X + Width / 2, Y + Height / 2, 20);
    }

    public bool IsOffscreen(Window screen)
    {
        if (X < -Width || X > screen.Width || Y < -Height || Y > screen.Height)
        {
            return true;
        }
        return false;
    }

    public abstract void Draw();

}

public class Boxy : Robot
{
    public Boxy(Window gameWindow, Player player) : base(gameWindow, player) { }

    public override void Draw()
    {
        double leftX = X + 12;
        double rightX = X + 27;
        double eyeY = Y + 10;
        double mouthY = Y + 30;


        SplashKit.FillRectangle(Color.Gray, X, Y, Width, Height);
        SplashKit.FillRectangle(MainColor, leftX, eyeY, 10, 10);
        SplashKit.FillRectangle(MainColor, rightX, eyeY, 10, 10);
        SplashKit.FillRectangle(MainColor, leftX, mouthY, 25, 10);
        SplashKit.FillRectangle(MainColor, leftX + 2, mouthY + 2, 21, 6);

    }
}

public class Roundy : Robot
{
    public Roundy(Window gameWindow, Player player) : base(gameWindow, player) { }

    public override void Draw()
    {
        double leftX, midX, rightX;
        double midY, eyeY, mouthY;

        leftX = X + 17;
        midX = X + 25;
        rightX = X + 33;

        midY = Y + 25;
        eyeY = Y + 20;
        mouthY = Y + 35;

        SplashKit.FillCircle(Color.Bisque, midX, midY, 25);
        SplashKit.DrawCircle(Color.RosyBrown, midX, midY, 25);
        SplashKit.FillCircle(MainColor, leftX, eyeY, 5);
        SplashKit.FillCircle(MainColor, rightX, eyeY, 5);
        SplashKit.FillEllipse(Color.RosyBrown, X, eyeY, 50, 30);
        SplashKit.DrawLine(Color.Brown, X, mouthY, X + 50, Y + 35);
    }
}

public class Virus : Robot{
    private Bitmap virus;
    public Virus(Window gameWindow, Player player) : base(gameWindow, player) {
         virus = new Bitmap("virus", "virus2.png");
         
    }
    public override void Draw(){
        virus.Draw(X+10,Y);
    }
}