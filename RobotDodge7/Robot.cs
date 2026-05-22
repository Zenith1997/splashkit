using SplashKitSDK;

public abstract class Robot : IShootable
{
    public double X { get; protected set; }
    public double Y { get; protected set; }
    public Color MainColor { get; protected set; }
    public Vector2D Velocity { get; protected set; }

    protected bool _isDying = false;

    public bool IsDying
    {
        get { return _isDying; }
    }

    public bool ReadyToRemove
    {
        get; protected set;
    }

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
        ReadyToRemove = false;

        const int speed = 1;

        // Randomly pick top/bottom or left/right
        if (SplashKit.Rnd() < 0.5)
        {
            // Start from top or bottom
            X = SplashKit.Rnd(gameWindow.Width);

            if (SplashKit.Rnd() < 0.5)
            {
                Y = -Height;
            }
            else
            {
                Y = gameWindow.Height;
            }
        }
        else
        {
            // Start from left or right
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

        // Diagonal movement
        double velocityX;
        double velocityY;

        if (X < 0)
        {
            velocityX = speed;
        }
        else if (X >= gameWindow.Width)
        {
            velocityX = -speed;
        }
        else
        {
            if (SplashKit.Rnd() < 0.5)
            {
                velocityX = speed;
            }
            else
            {
                velocityX = -speed;
            }
        }

        if (Y < 0)
        {
            velocityY = speed;
        }
        else if (Y >= gameWindow.Height)
        {
            velocityY = -speed;
        }
        else
        {
            if (SplashKit.Rnd() < 0.5)
            {
                velocityY = speed;
            }
            else
            {
                velocityY = -speed;
            }
        }

        Velocity = new Vector2D()
        {
            X = velocityX,
            Y = velocityY
        };

        CollisionCircle = SplashKit.CircleAt(X + Width / 2, Y + Height / 2, 20);
    }

    public virtual void Update()
    {
        if (!_isDying)
        {
            X += Velocity.X;
            Y += Velocity.Y;
        }

        CollisionCircle = SplashKit.CircleAt(X + Width / 2, Y + Height / 2, 20);

        UpdateHitEffect();
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

    public abstract void HitByBullet();

    protected abstract void UpdateHitEffect();
}

public class Boxy : Robot
{
    private double _blastRadius;

    public Boxy(Window gameWindow, Player player) : base(gameWindow, player)
    {
        _blastRadius = 0;
    }

    public override void Draw()
    {
        if (_isDying)
        {
            DrawBlastEffect();
            return;
        }

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

    public override void HitByBullet()
    {
        if (!_isDying)
        {
            _isDying = true;
            _blastRadius = 5;
        }
    }

    protected override void UpdateHitEffect()
    {
        if (_isDying)
        {
            _blastRadius += 4;

            if (_blastRadius > 70)
            {
                ReadyToRemove = true;
            }
        }
    }

    private void DrawBlastEffect()
    {
        double centerX = X + Width / 2;
        double centerY = Y + Height / 2;

        SplashKit.FillCircle(Color.Orange, centerX, centerY, _blastRadius);
        SplashKit.DrawCircle(Color.Red, centerX, centerY, _blastRadius + 5);
        SplashKit.DrawCircle(Color.Yellow, centerX, centerY, _blastRadius + 12);
    }
}

public class Roundy : Robot
{
    private double _meltAmount;

    public Roundy(Window gameWindow, Player player) : base(gameWindow, player)
    {
        _meltAmount = 0;
    }

    public override void Draw()
    {
        if (_isDying)
        {
            DrawMeltEffect();
            return;
        }

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
        SplashKit.DrawLine(Color.Brown, X, mouthY, X + 50, mouthY);
    }

    public override void HitByBullet()
    {
        if (!_isDying)
        {
            _isDying = true;
            _meltAmount = 0;
        }
    }

    protected override void UpdateHitEffect()
    {
        if (_isDying)
        {
            _meltAmount += 2;

            if (_meltAmount > 50)
            {
                ReadyToRemove = true;
            }
        }
    }

    private void DrawMeltEffect()
    {
        double meltHeight = Height - _meltAmount;

        if (meltHeight < 5)
        {
            meltHeight = 5;
        }

        SplashKit.FillEllipse(Color.Bisque, X, Y + _meltAmount, Width, meltHeight);
        SplashKit.FillEllipse(Color.RosyBrown, X + 5, Y + 35 + _meltAmount, 40, 15);
        SplashKit.FillCircle(MainColor, X + 18, Y + 20 + _meltAmount, 4);
        SplashKit.FillCircle(MainColor, X + 32, Y + 20 + _meltAmount, 4);
    }
}

public class Virus : Robot
{
    private Bitmap virus;
    private double _scale;

    public Virus(Window gameWindow, Player player) : base(gameWindow, player)
    {
        virus = new Bitmap("virus", "virus2.png");
        _scale = 1.0;
    }

    public override void Draw()
    {
        if (_isDying)
        {
            DrawShrinkEffect();
            return;
        }

        virus.Draw(X + 10, Y);
    }

    public override void HitByBullet()
    {
        if (!_isDying)
        {
            _isDying = true;
            _scale = 1.0;
        }
    }

    protected override void UpdateHitEffect()
    {
        if (_isDying)
        {
            _scale -= 0.05;

            if (_scale <= 0.1)
            {
                ReadyToRemove = true;
            }
        }
    }

    private void DrawShrinkEffect()
    {
        double drawX = X + 25 - (25 * _scale);
        double drawY = Y + 25 - (25 * _scale);

        virus.Draw(drawX, drawY, SplashKit.OptionScaleBmp(_scale, _scale));

        SplashKit.DrawCircle(Color.Green, X + Width / 2, Y + Height / 2, 30 * _scale);
    }
}