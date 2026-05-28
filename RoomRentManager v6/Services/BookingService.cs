public class BookingService
{
    private readonly List<Booking> _bookings;
    private readonly LogAction _logAction;

    public BookingService(LogAction logAction)
    {
        _bookings = new List<Booking>();
        _logAction = logAction;
    }

    public IReadOnlyList<Booking> Bookings => _bookings.AsReadOnly();

    public OperationResult<Booking> CreateBooking(Renter renter, Room room)
    {
        if (renter == null)
        {
            return OperationResult<Booking>.Fail("Renter is required.");
        }

        if (room == null)
        {
            return OperationResult<Booking>.Fail("Room is required.");
        }

        if (!room.IsAvailable)
        {
            return OperationResult<Booking>.Fail("This room is not available.");
        }

        if (renter.WeeklyBudget < room.PricePerWeek)
        {
            return OperationResult<Booking>.Fail("The renter's weekly budget is lower than the room price.");
        }

        bool renterAlreadyHasActiveBooking = _bookings.Any(booking => booking.Renter == renter && booking.Status == BookingStatus.Active);
        if (renterAlreadyHasActiveBooking)
        {
            return OperationResult<Booking>.Fail("This renter already has an active booking.");
        }

        OperationResult roomResult = room.MarkBooked();
        if (!roomResult.Success)
        {
            return OperationResult<Booking>.Fail(roomResult.Message);
        }

        Booking booking = new Booking(renter, room, DateTime.Today);
        _bookings.Add(booking);
        _logAction($"Booking created: {booking.BookingId} | Renter: {renter.Name} | Room: {room.RoomId}");
        return OperationResult<Booking>.Ok(booking, "Booking created successfully.");
    }

    public OperationResult<Booking> GetBookingByNumber(int bookingNumber)
    {
        if (bookingNumber < 1 || bookingNumber > _bookings.Count)
        {
            return OperationResult<Booking>.Fail("Invalid booking number.");
        }

        return OperationResult<Booking>.Ok(_bookings[bookingNumber - 1], "Booking found.");
    }

    public IReadOnlyList<Booking> GetActiveBookings()
    {
        // Lambda function used to filter active bookings.
        return _bookings.Where(booking => booking.Status == BookingStatus.Active).ToList();
    }

    public OperationResult EndBooking(int bookingNumber)
    {
        OperationResult<Booking> bookingResult = GetBookingByNumber(bookingNumber);
        if (!bookingResult.Success || bookingResult.Value == null)
        {
            return OperationResult.Fail(bookingResult.Message);
        }

        OperationResult endResult = bookingResult.Value.EndBooking();
        if (endResult.Success)
        {
            _logAction($"Booking ended: {bookingResult.Value.BookingId}");
        }

        return endResult;
    }
}
