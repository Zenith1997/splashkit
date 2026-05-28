public class HouseService
{
    private readonly List<House> _houses;
    private readonly LogAction _logAction;

    public HouseService(LogAction logAction)
    {
        _houses = new List<House>();
        _logAction = logAction;
    }

    public List<House> Houses => _houses;

    public void AddHouse(string address, string suburb)
    {
        _houses.Add(new House(address, suburb));
        _logAction($"House added: {address}, {suburb}");
    }

    public bool AddRoomToHouse(int houseIndex, Room room)
    {
        if (houseIndex < 0 || houseIndex >= _houses.Count)
        {
            return false;
        }

        _houses[houseIndex].AddRoom(room);
        _logAction($"Room added: {room.RoomId}");
        return true;
    }

    public Room? FindAvailableRoomById(string roomId)
    {
        foreach (House house in _houses)
        {
            Room? room = house.FindRoomById(roomId);
            if (room != null && room.IsAvailable)
            {
                return room;
            }
        }

        return null;
    }

    public List<Room> GetAllAvailableRooms()
    {
        // Lambda function inside SelectMany/Where.
        return _houses.SelectMany(house => house.Rooms)
                      .Where(room => room.IsAvailable)
                      .ToList();
    }
}
