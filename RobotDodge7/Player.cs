using SplashKitSDK;
#nullable disable

public class Player
{

    private Bitmap _PlayerBitmap;
    private Window _gameWindow;

    public double X { get; private set; }
    public double Y { get; private set; }
    public bool Quit { get; private set; }

    // auto-property. Where C# adds the code and fields 
    public int Width
    {
        get
        {
            return _PlayerBitmap.Width;
        }
    }

    public int Height
    {
        get
        {
            return _PlayerBitmap.Height;
        }
    }

    public Player(Window gameWindow)
    {
        _PlayerBitmap = new Bitmap("player", "Player.png");
        _gameWindow = gameWindow;
        X = (_gameWindow.Width - Width) / 2;
        Y = (_gameWindow.Height - Height) / 2;
        Quit = false;

    }

    public void Draw()
    {
        _PlayerBitmap.Draw(X, Y);
    }

    public void HandleInput()
    {
        const int speed = 5;
        if (SplashKit.KeyDown(KeyCode.UpKey))
        {
            Y -= speed;
        }
        if (SplashKit.KeyDown(KeyCode.DownKey))
        {
            Y += speed;
        }
        if (SplashKit.KeyDown(KeyCode.LeftKey))
        {
            X -= speed;
        }
        if (SplashKit.KeyDown(KeyCode.RightKey))
        {
            X += speed;
        }
        if (SplashKit.KeyDown(KeyCode.EscapeKey))
        {
            Quit = true;
        }

    }

    public void StayOnWindow(Window limit)
    {
        const int GAP = 10;

        if (X > limit.Width - GAP)
        {
            X = limit.Width - Width;
        }
        if (X < GAP)
        {
            X = GAP;
        }
        if (Y < GAP)
        {
            Y = GAP;
        }
        if (Y > limit.Height - GAP)
        {
            Y = limit.Height - Height;
        }

    }

    public bool CollidedWith(Robot other)
    {
        return _PlayerBitmap.CircleCollision(X, Y, other.CollisionCircle);
    }
}
