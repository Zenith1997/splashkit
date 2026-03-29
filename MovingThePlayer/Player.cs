using SplashKitSDK;
using System;

public class Player
{
    private Bitmap _playerBitmap;

    public double X { get; private set; }
    public double Y { get; private set; }

    public bool Quit { get; private set; }

    public int Width => _playerBitmap.Width;
    public int Height => _playerBitmap.Height;

    private const int SPEED = 5;
    private const int GAP = 10;

    public Player(Window gameWindow)
    {
        _playerBitmap = new Bitmap("Player", "Player.png");

        X = (gameWindow.Width - Width) / 2;
        Y = (gameWindow.Height - Height) / 2;

        Quit = false;
    }

    public void Draw()
    {
        _playerBitmap.Draw(X, Y);
    }

    // 🔥 HANDLE INPUT (MAIN FEATURE)
    public void HandleInput()
    {
        if (SplashKit.KeyDown(KeyCode.UpKey))
            Y -= SPEED;

        if (SplashKit.KeyDown(KeyCode.DownKey))
            Y += SPEED;

        if (SplashKit.KeyDown(KeyCode.LeftKey))
            X -= SPEED;

        if (SplashKit.KeyDown(KeyCode.RightKey))
            X += SPEED;

        if (SplashKit.KeyTyped(KeyCode.EscapeKey))
            Quit = true;
    }

    // 🔥 KEEP PLAYER INSIDE WINDOW
    public void StayOnWindow(Window window)
    {
        if (X < GAP) X = GAP;

        if (X + Width > window.Width - GAP)
            X = window.Width - Width - GAP;

        if (Y < GAP) Y = GAP;

        if (Y + Height > window.Height - GAP)
            Y = window.Height - Height - GAP;
    }
}