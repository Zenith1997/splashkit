public abstract class Room : IPrintable
{
    private readonly string _roomId;
    private readonly decimal _pricePerWeek;
    private bool _isAvailable;

    protected Room(string roomId, decimal pricePerWeek)
    {
        _roomId = roomId;
        _pricePerWeek = pricePerWeek;
        _isAvailable = true;
    }

    public string RoomId => _roomId;
    public decimal PricePerWeek => _pricePerWeek;
    public bool IsAvailable => _isAvailable;

    public void MarkBooked()
    {
        _isAvailable = false;
    }

    public void MarkAvailable()
    {
        _isAvailable = true;
    }

    public abstract string GetRoomType();

    public virtual void PrintDetails()
    {
        Console.WriteLine($"Room ID: {_roomId} | Type: {GetRoomType()} | Price: {_pricePerWeek:C} | Available: {_isAvailable}");
    }
}
