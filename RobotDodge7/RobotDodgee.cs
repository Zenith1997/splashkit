using System;
using SplashKitSDK;
using System.Collections.Generic;
#nullable disable

public class RobotDodgee
{
    private Player player;
    private Bitmap _heart;
    private Window gameWindow;
    private SplashKitSDK.Timer _Timer;

    private List<Robot> _Robots;
    private List<Bullet> _Bullets;

    private int lives = 5;
    private bool gameOver = false;
    private Font font;

    private static Random random = new Random();

    public bool Quit
    {
        get { return player.Quit || gameOver; }
    }

    public RobotDodgee(Window gameWindow)
    {
        this.gameWindow = gameWindow;

        player = new Player(gameWindow);
        _Bullets = new List<Bullet>();
        _Robots = new List<Robot>();

        _heart = new Bitmap("heart", "heart.png");
        _Timer = new SplashKitSDK.Timer("GameTimer");
        font = SplashKit.LoadFont("LoveDays-2v7Oe", "LoveDays-2v7Oe.ttf");

        _Timer.Start();
    }

    public void HandleInput()
    {
        player.HandleInput();

        if (SplashKit.MouseClicked(MouseButton.LeftButton))
        {
            double startX = player.X + player.Width / 2;
            double startY = player.Y + player.Height / 2;

            double targetX = SplashKit.MouseX();
            double targetY = SplashKit.MouseY();

            _Bullets.Add(new Bullet(startX, startY, targetX, targetY));
        }

        player.StayOnWindow(gameWindow);
    }

    public void Draw()
    {
        if (!gameWindow.CloseRequested && !gameOver)
        {
            gameWindow.Clear(Color.Beige);

            DrawHeart(lives, gameWindow);
            GetScore();

            foreach (Robot robot in _Robots)
            {
                robot.Draw();
            }

            player.Draw();

            foreach (Bullet bullet in _Bullets)
            {
                bullet.Draw();
            }

            gameWindow.Refresh(60);
        }
    }

    public void DrawHeart(int lives, Window gameWindow)
    {
        int xheart = 10;

        for (int i = 0; i < lives; i++)
        {
            _heart.Draw(xheart, 10);
            xheart += 50;
        }

        if (lives == 0)
        {
            gameWindow.Close();
        }
    }

    public void GetScore()
    {
        string score = $"Score: {_Timer.Ticks / 1000}";
        SplashKit.DrawText(score, Color.BlueViolet, font, 24, 500, 10);
    }

    public void Update()
    {
        if (gameOver)
        {
            _Timer.Stop();
            return;
        }

        foreach (Robot robot in _Robots)
        {
            robot.Update();
        }

        foreach (Bullet bullet in _Bullets)
        {
            bullet.MoveBullet();
        }

        _Bullets.RemoveAll(bullet => bullet.IsOffScreen(gameWindow) || !bullet.Active);

        if (_Robots.Count < 10)
        {
            _Robots.Add(RandomRobot());
        }

        CheckCollisions();

        _Robots.RemoveAll(robot => robot.ReadyToRemove);
    }

    private void CheckCollisions()
    {
        List<Robot> toBeRemovedRobots = new List<Robot>();

        foreach (Robot robot in _Robots)
        {
            if (!robot.IsDying)
            {
                if (player.CollidedWith(robot) || robot.IsOffscreen(gameWindow))
                {
                    toBeRemovedRobots.Add(robot);
                }

                if (player.CollidedWith(robot))
                {
                    lives--;

                    if (lives <= 0)
                    {
                        gameOver = true;
                    }
                }

                foreach (Bullet bullet in _Bullets)
                {
                    if (bullet.Active && bullet.RobotShot(robot))
                    {
                        robot.HitByBullet();
                        bullet.Active = false;
                    }
                }
            }
        }

        foreach (Robot bot in toBeRemovedRobots)
        {
            _Robots.Remove(bot);
        }
    }

    public Robot RandomRobot()
    {
        int robotType = random.Next(3);

        switch (robotType)
        {
            case 0:
                return new Boxy(gameWindow, player);

            case 1:
                return new Roundy(gameWindow, player);

            case 2:
                return new Virus(gameWindow, player);

            default:
                throw new InvalidOperationException("Unknown robot type");
        }
    }
}