public sealed class FileLogger : IDisposable
{
    private readonly StreamWriter _writer;
    private bool _disposed;

    public FileLogger(string filePath)
    {
        _writer = new StreamWriter(filePath, append: true);
        _writer.AutoFlush = true;
        _writer.WriteLine($"--- RoomRent Manager session started at {DateTime.Now:yyyy-MM-dd HH:mm:ss} ---");
    }

    public void WriteLog(string message)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(FileLogger));
        }

        _writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {message}");
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _writer.WriteLine($"--- RoomRent Manager session ended at {DateTime.Now:yyyy-MM-dd HH:mm:ss} ---");
        _writer.Dispose();
        _disposed = true;
    }
}
