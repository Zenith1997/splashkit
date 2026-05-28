public class House : IPrintable
{
    private readonly List<Room> _rooms;

    public House(string address, string suburb)
    {
        if (string.IsNullOrWhiteSpace(address))
        {
            throw new ArgumentException("Address cannot be empty.");
        }

        if (string.IsNullOrWhiteSpace(suburb))
        {
            throw new ArgumentException("Suburb cannot be empty.");
        }

        Address = address.Trim();
        Suburb = suburb.Trim();
        _rooms = new List<Room>();
    }

    public string Address { get; }
    public string Suburb { get; }
    public IReadOnlyList<Room> Rooms => _rooms.AsReadOnly();

    public OperationResult AddRoom(Room room)
    {
        if (_rooms.Any(existingRoom => existingRoom.RoomId.Equals(room.RoomId, StringComparison.OrdinalIgnoreCase)))
        {
            return OperationResult.Fail($"Room ID {room.RoomId} already exists in this house.");
        }

        _rooms.Add(room);
        return OperationResult.Ok($"Room {room.RoomId} added to {Address}, {Suburb}.");
    }

    public Room? FindRoomById(string roomId)
    {
        return _rooms.FirstOrDefault(room => room.RoomId.Equals(roomId.Trim(), StringComparison.OrdinalIgnoreCase));
    }

    public IReadOnlyList<Room> GetAvailableRooms()
    {
        // Lambda expression: only return rooms where the availability flag is true.
        return _rooms.Where(room => room.IsAvailable)
                     .OrderBy(room => room.PricePerWeek)
                     .ToList();
    }

    public void PrintDetails()
    {
        Console.WriteLine($"House: {Address}, {Suburb}");

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
