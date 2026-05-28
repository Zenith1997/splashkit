public class Booking : IPrintable, IPayable
{
    private readonly Renter _renter;
    private readonly Room _room;
    private readonly DateTime _startDate;
    private bool _isActive;
    private readonly List<Payment> _payments;

    public Booking(Renter renter, Room room, DateTime startDate)
    {
        _renter = renter;
        _room = room;
        _startDate = startDate;
        _isActive = true;
        _payments = new List<Payment>();
        _room.MarkBooked();
    }

    public Renter Renter => _renter;
    public Room Room => _room;
    public bool IsActive => _isActive;

    public void RecordPayment(decimal amount)
    {
        _payments.Add(new Payment(amount));
    }

    public void EndBooking()
    {
        _isActive = false;
        _room.MarkAvailable();
    }

    public void PrintDetails()
    {
        Console.WriteLine($"\nBooking: {_renter.Name} booked room {_room.RoomId} from {_startDate.ToShortDateString()} | Active: {_isActive}");

        if (_payments.Count == 0)
        {
            Console.WriteLine("  No payments recorded.");
            return;
        }

        foreach (Payment payment in _payments)
        {
            Console.Write("  ");
            payment.PrintDetails();
        }
    }
}
