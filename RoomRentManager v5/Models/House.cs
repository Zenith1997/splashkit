public class House : IPrintable
{
    private readonly string _address;
    private readonly string _suburb;
    private readonly List<Room> _rooms;

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

    public Room? FindRoomById(string roomId)
    {
        return _rooms.FirstOrDefault(room => room.RoomId.Equals(roomId, StringComparison.OrdinalIgnoreCase));
    }

    public List<Room> GetAvailableRooms()
    {
        // Lambda function: returns only rooms where IsAvailable is true.
        return _rooms.Where(room => room.IsAvailable).ToList();
    }

    public void PrintDetails()
    {
        Console.WriteLine($"\nHouse: {_address}, {_suburb}");

        if (_rooms.Count == 0)
        {
            Console.WriteLine("  No rooms added yet.");
            return;
        }

        foreach (Room room in _rooms)
        {
            Console.Write("  ");
            room.PrintDetails();
        }
    }
}
