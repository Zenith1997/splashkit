public class Booking : IPrintable, IPayable
{
    private readonly List<Payment> _payments;

    public Booking(Renter renter, Room room, DateTime startDate)
    {
        Renter = renter ?? throw new ArgumentNullException(nameof(renter));
        Room = room ?? throw new ArgumentNullException(nameof(room));
        StartDate = startDate;
        BookingId = Guid.NewGuid().ToString("N")[..8].ToUpperInvariant();
        Status = BookingStatus.Active;
        _payments = new List<Payment>();
    }

    public string BookingId { get; }
    public Renter Renter { get; }
    public Room Room { get; }
    public DateTime StartDate { get; }
    public DateTime? EndDate { get; private set; }
    public BookingStatus Status { get; private set; }
    public IReadOnlyList<Payment> Payments => _payments.AsReadOnly();
    public decimal TotalPaid => _payments.Sum(payment => payment.Amount);

    public OperationResult RecordPayment(decimal amount, string method)
    {
        if (Status != BookingStatus.Active)
        {
            return OperationResult.Fail("Cannot record a payment for a booking that is not active.");
        }

        if (amount <= 0)
        {
            return OperationResult.Fail("Payment amount must be greater than zero.");
        }

        Payment payment = new Payment(amount, method);
        _payments.Add(payment);
        return OperationResult.Ok($"Payment of {amount:C} recorded for booking {BookingId}.");
    }

    public OperationResult EndBooking()
    {
        if (Status != BookingStatus.Active)
        {
            return OperationResult.Fail("Only active bookings can be ended.");
        }

        Status = BookingStatus.Completed;
        EndDate = DateTime.Today;
        Room.MarkAvailable();
        return OperationResult.Ok($"Booking {BookingId} ended and room {Room.RoomId} is now available.");
    }

    public void PrintDetails()
    {
        Console.WriteLine($"Booking ID: {BookingId} | Renter: {Renter.Name} | Room: {Room.RoomId} | Start: {StartDate:d} | Status: {Status} | Total Paid: {TotalPaid:C}");

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
