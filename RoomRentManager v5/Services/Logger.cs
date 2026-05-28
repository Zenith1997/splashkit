public class Logger : IDisposable
{
    private bool _disposed;

    public void WriteLog(string message)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(Logger));
        }

        Console.WriteLine($"[LOG] {DateTime.Now}: {message}");
    }

    public void Dispose()
    {
        // In a real program, close files, database connections, or network connections here.
        _disposed = true;
        Console.WriteLine("Logger disposed. Resources cleaned up.");
    }
}
