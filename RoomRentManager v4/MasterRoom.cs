public class MasterRoom : Room
{
    public MasterRoom(string roomId, decimal pricePerWeek) : base(roomId, pricePerWeek)
    {
    }

    public override string GetRoomType()
    {
        return "Master Room";
    }
}
