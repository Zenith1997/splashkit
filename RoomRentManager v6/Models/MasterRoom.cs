public class MasterRoom : Room
{
    public MasterRoom(string roomId, decimal pricePerWeek) : base(roomId, pricePerWeek)
    {
    }

    public override RoomType Type => RoomType.Master;
    public override string TypeName => "Master Room";

    public override void PrintDetails()
    {
        base.PrintDetails();
        Console.WriteLine("  Extra: Larger room suitable for couples or higher-budget renters.");
    }
}
