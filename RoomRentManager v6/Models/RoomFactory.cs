public static class RoomFactory
{
    public static Room Create(RoomType type, string roomId, decimal pricePerWeek)
    {
        return type switch
        {
            RoomType.Single => new SingleRoom(roomId, pricePerWeek),
            RoomType.Master => new MasterRoom(roomId, pricePerWeek),
            _ => throw new ArgumentException("Unsupported room type.")
        };
    }
}
