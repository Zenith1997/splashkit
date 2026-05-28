public class DemoDataSeeder
{
    private readonly HouseService _houseService;
    private readonly RenterService _renterService;
    private readonly BookingService _bookingService;
    private readonly PaymentService _paymentService;

    public DemoDataSeeder(HouseService houseService, RenterService renterService, BookingService bookingService, PaymentService paymentService)
    {
        _houseService = houseService;
        _renterService = renterService;
        _bookingService = bookingService;
        _paymentService = paymentService;
    }

    public OperationResult LoadDemoData()
    {
        if (_houseService.Houses.Count > 0 || _renterService.Renters.Count > 0 || _bookingService.Bookings.Count > 0)
        {
            return OperationResult.Fail("Demo data can only be loaded when the system is empty.");
        }

        _houseService.AddHouse("25 Beach Road", "Torquay");
        _houseService.AddHouse("10 Station Street", "Geelong");

        OperationResult<Room> room1 = _houseService.AddRoomToHouse(1, RoomType.Single, "R1", 180m);
        _houseService.AddRoomToHouse(1, RoomType.Master, "M1", 320m);
        _houseService.AddRoomToHouse(2, RoomType.Single, "R2", 210m);

        OperationResult<Renter> renter1 = _renterService.RegisterRenter("Alex Chen", "0400 111 222", 250m);
        _renterService.RegisterRenter("Maria Smith", "0400 333 444", 350m);

        if (renter1.Success && renter1.Value != null && room1.Success && room1.Value != null)
        {
            OperationResult<Booking> booking = _bookingService.CreateBooking(renter1.Value, room1.Value);
            if (booking.Success && booking.Value != null)
            {
                _paymentService.RecordPayment(booking.Value, 180m, "Bank Transfer");
            }
        }

        return OperationResult.Ok("Demo data loaded successfully.");
    }
}
