using SplashKitSDK;

public class Bullet
{

    public double x { get; private set; }
    public double y { get; private set; }
    private double velocityX;
    private double velocityY;
    private const double bulletSpeed = 8.0;
    private const double bulletRadius = 10;
    public bool Active { get; set; } = true;

    public Bullet(double startX, double startY, double targetX, double targetY)
    {

        //startx and starty are player's starting position
        x = startX;
        y = startY;
        Shoot(startX, startY, targetX, targetY);

    }

    public void Shoot(double startX, double startY, double targetX, double targetY)
    {

        // Calculate direction vector
        double directionX = targetX - startX;
        double directionY = targetY - startY;

        // Normalize the direction
        double magnitude = Math.Sqrt(directionX * directionX + directionY * directionY);
        velocityX = directionX / magnitude * bulletSpeed;
        velocityY = directionY / magnitude * bulletSpeed;
    }

    public void MoveBullet()
    {
        // Moving the bullet
        x += velocityX;
        y += velocityY;
    }

    public bool IsOffScreen(Window window)
    {
        return x < 0 || x > window.Width || y < 0 || y > window.Height;
    }

    public void Draw()
    {
        SplashKit.FillCircle(Color.Crimson, x, y, bulletRadius);
    }

    public bool RobotShot(Robot robot)
    {
        // bullet's collision circle
        Circle bulletCollisionCircle = new Circle();
        bulletCollisionCircle.Center.X = x;
        bulletCollisionCircle.Center.Y = y;
        bulletCollisionCircle.Radius = bulletRadius;

        // Robot's collision circle
        Circle robotCollisionCircle = robot.CollisionCircle;

        //check for circle intersection
        return SplashKit.CirclesIntersect(bulletCollisionCircle, robotCollisionCircle);
    }

}