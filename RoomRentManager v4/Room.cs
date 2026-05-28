using System;

public abstract class Room
{
    private string _roomId;
    private decimal _pricePerWeek;
    private bool _isAvailable;

    public Room(string roomId, decimal pricePerWeek)
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

    public abstract string GetRoomType();

    public virtual void PrintDetails()
    {
        Console.WriteLine($"Room: {_roomId}, Type: {GetRoomType()}, Price: {_pricePerWeek:C}, Available: {_isAvailable}");
    }
}
