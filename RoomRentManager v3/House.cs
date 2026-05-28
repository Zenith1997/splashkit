using System;

public class House
{
    public string HouseId { get; }
    public string Address { get; }
    public int NumberOfRooms { get; }
    public decimal PricePerWeek { get; }
    public bool IsAvailable { get; private set; } = true;

    public House(string houseId, string address, int numberOfRooms, decimal pricePerWeek)
    {
        HouseId = houseId;
        Address = address;
        NumberOfRooms = numberOfRooms;
        PricePerWeek = pricePerWeek;
    }

    public void PrintDetails()
    {
        Console.WriteLine($"House ID: {HouseId}, Address: {Address}, Rooms: {NumberOfRooms}, Price/Week: {PricePerWeek:C}, Available: {IsAvailable}");
    }

    public void Book()
    {
        IsAvailable = false;
    }

    public void Vacate()
    {
        IsAvailable = true;
    }
}
