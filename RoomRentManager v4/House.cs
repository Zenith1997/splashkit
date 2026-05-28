using System;
using System.Collections.Generic;

public class House
{
    private string _address;
    private string _suburb;
    private List<Room> _rooms;

    public House(string address, string suburb)
    {
        _address = address;
        _suburb = suburb;
        _rooms = new List<Room>();
    }

    public string Address => _address;
    public string Suburb => _suburb;
    public List<Room> Rooms => _rooms;

    public void AddRoom(Room room)
    {
        _rooms.Add(room);
    }

    public void PrintDetails()
    {
        Console.WriteLine($"House: {_address}, {_suburb}");
        foreach (Room room in _rooms)
        {
            room.PrintDetails();
        }
    }
}
