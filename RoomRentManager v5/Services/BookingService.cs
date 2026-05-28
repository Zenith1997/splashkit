public class BookingService
{
    private readonly List<Booking> _bookings;
    private readonly LogAction _logAction;

    public BookingService(LogAction logAction)
    {
        _bookings = new List<Booking>();
        _logAction = logAction;
    }

    public List<Booking> Bookings => _bookings;

    public bool CreateBooking(Renter renter, Room room)
    {
        if (!room.IsAvailable)
        {
            return false;
        }

        if (renter.Budget < room.PricePerWeek)
        {
            return false;
        }

        Booking booking = new Booking(renter, room, DateTime.Today);
        _bookings.Add(booking);
        _logAction($"Booking created for {renter.Name} in room {room.RoomId}");
        return true;
    }
}
