using SplashKitSDK;

public  class Robot
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

    public void Draw()  {
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
