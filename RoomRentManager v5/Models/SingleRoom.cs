public class SingleRoom : Room
{
    public SingleRoom(string roomId, decimal pricePerWeek) : base(roomId, pricePerWeek)
    {
    }

    public override string GetRoomType()
    {
        return "Single Room";
    }
}
