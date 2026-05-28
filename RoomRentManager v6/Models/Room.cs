public abstract class Room : IPrintable
{
    private bool _isAvailable;

    protected Room(string roomId, decimal pricePerWeek)
    {
        if (string.IsNullOrWhiteSpace(roomId))
        {
            throw new ArgumentException("Room ID cannot be empty.");
        }

        if (pricePerWeek <= 0)
        {
            throw new ArgumentException("Room price must be greater than zero.");
        }

        RoomId = roomId.Trim().ToUpperInvariant();
        PricePerWeek = pricePerWeek;
        _isAvailable = true;
    }

    public string RoomId { get; }
    public decimal PricePerWeek { get; }
    public bool IsAvailable => _isAvailable;

    public abstract RoomType Type { get; }
    public abstract string TypeName { get; }

    public OperationResult MarkBooked()
    {
        if (!_isAvailable)
        {
            return OperationResult.Fail($"Room {RoomId} is already booked.");
        }

        _isAvailable = false;
        return OperationResult.Ok($"Room {RoomId} marked as booked.");
    }

    public OperationResult MarkAvailable()
    {
        if (_isAvailable)
        {
            return OperationResult.Fail($"Room {RoomId} is already available.");
        }

        _isAvailable = true;
        return OperationResult.Ok($"Room {RoomId} marked as available.");
    }

    public virtual void PrintDetails()
    {
        string availability = IsAvailable ? "Available" : "Booked";
        Console.WriteLine($"Room ID: {RoomId} | Type: {TypeName} | Price: {PricePerWeek:C}/week | Status: {availability}");
    }
}
