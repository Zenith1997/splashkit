public class HouseService
{
    private readonly List<House> _houses;
    private readonly LogAction _logAction;

    public HouseService(LogAction logAction)
    {
        _houses = new List<House>();
        _logAction = logAction;
    }

    public IReadOnlyList<House> Houses => _houses.AsReadOnly();

    public OperationResult<House> AddHouse(string address, string suburb)
    {
        if (string.IsNullOrWhiteSpace(address))
        {
            return OperationResult<House>.Fail("Address cannot be empty.");
        }

        if (string.IsNullOrWhiteSpace(suburb))
        {
            return OperationResult<House>.Fail("Suburb cannot be empty.");
        }

        bool duplicate = _houses.Any(house =>
            house.Address.Equals(address.Trim(), StringComparison.OrdinalIgnoreCase) &&
            house.Suburb.Equals(suburb.Trim(), StringComparison.OrdinalIgnoreCase));

        if (duplicate)
        {
            return OperationResult<House>.Fail("This house already exists in the system.");
        }

        House newHouse = new House(address, suburb);
        _houses.Add(newHouse);
        _logAction($"House added: {newHouse.Address}, {newHouse.Suburb}");
        return OperationResult<House>.Ok(newHouse, "House added successfully.");
    }

    public OperationResult<Room> AddRoomToHouse(int houseNumber, RoomType roomType, string roomId, decimal pricePerWeek)
    {
        OperationResult<House> houseResult = GetHouseByNumber(houseNumber);
        if (!houseResult.Success || houseResult.Value == null)
        {
            return OperationResult<Room>.Fail(houseResult.Message);
        }

        if (string.IsNullOrWhiteSpace(roomId))
        {
            return OperationResult<Room>.Fail("Room ID cannot be empty.");
        }

        if (pricePerWeek <= 0)
        {
            return OperationResult<Room>.Fail("Room price must be greater than zero.");
        }

        if (RoomIdExists(roomId))
        {
            return OperationResult<Room>.Fail("Room IDs must be unique across the whole system.");
        }

        Room room = RoomFactory.Create(roomType, roomId, pricePerWeek);
        OperationResult addResult = houseResult.Value.AddRoom(room);

        if (!addResult.Success)
        {
            return OperationResult<Room>.Fail(addResult.Message);
        }

        _logAction($"Room added: {room.RoomId} ({room.TypeName}) at {houseResult.Value.Address}");
        return OperationResult<Room>.Ok(room, "Room added successfully.");
    }

    public OperationResult<House> GetHouseByNumber(int houseNumber)
    {
        if (houseNumber < 1 || houseNumber > _houses.Count)
        {
            return OperationResult<House>.Fail("Invalid house number.");
        }

        return OperationResult<House>.Ok(_houses[houseNumber - 1], "House found.");
    }

    public Room? FindAvailableRoomById(string roomId)
    {
        if (string.IsNullOrWhiteSpace(roomId))
        {
            return null;
        }

        return _houses.SelectMany(house => house.Rooms)
                      .FirstOrDefault(room => room.RoomId.Equals(roomId.Trim(), StringComparison.OrdinalIgnoreCase) && room.IsAvailable);
    }

    public IReadOnlyList<Room> GetAllAvailableRooms()
    {
        // Lambda functions with SelectMany, Where, and OrderBy.
        return _houses.SelectMany(house => house.Rooms)
                      .Where(room => room.IsAvailable)
                      .OrderBy(room => room.PricePerWeek)
                      .ToList();
    }

    private bool RoomIdExists(string roomId)
    {
        return _houses.SelectMany(house => house.Rooms)
                      .Any(room => room.RoomId.Equals(roomId.Trim(), StringComparison.OrdinalIgnoreCase));
    }
}
