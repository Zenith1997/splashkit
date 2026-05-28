public class SingleRoom : Room
{
    public SingleRoom(string roomId, decimal pricePerWeek) : base(roomId, pricePerWeek)
    {
    }

    public override RoomType Type => RoomType.Single;
    public override string TypeName => "Single Room";
}
