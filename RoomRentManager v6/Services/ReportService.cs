public class ReportService
{
    private readonly HouseService _houseService;
    private readonly RenterService _renterService;
    private readonly BookingService _bookingService;

    public ReportService(HouseService houseService, RenterService renterService, BookingService bookingService)
    {
        _houseService = houseService;
        _renterService = renterService;
        _bookingService = bookingService;
    }

    public void PrintHouses()
    {
        Console.WriteLine("\nHOUSES");
        if (_houseService.Houses.Count == 0)
        {
            Console.WriteLine("No houses added yet.");
            return;
        }

        for (int i = 0; i < _houseService.Houses.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_houseService.Houses[i].Address}, {_houseService.Houses[i].Suburb}");
        }
    }

    public void PrintRenters()
    {
        Console.WriteLine("\nRENTERS");
        if (_renterService.Renters.Count == 0)
        {
            Console.WriteLine("No renters registered yet.");
            return;
        }

        for (int i = 0; i < _renterService.Renters.Count; i++)
        {
            Console.Write($"{i + 1}. ");
            _renterService.Renters[i].PrintDetails();
        }
    }

    public void PrintAvailableRooms()
    {
        Console.WriteLine("\nAVAILABLE ROOMS");
        IReadOnlyList<Room> availableRooms = _houseService.GetAllAvailableRooms();

        if (availableRooms.Count == 0)
        {
            Console.WriteLine("No rooms are currently available.");
            return;
        }

        foreach (Room room in availableRooms)
        {
            room.PrintDetails();
        }
    }

    public void PrintBookings()
    {
        Console.WriteLine("\nBOOKINGS");
        if (_bookingService.Bookings.Count == 0)
        {
            Console.WriteLine("No bookings created yet.");
            return;
        }

        for (int i = 0; i < _bookingService.Bookings.Count; i++)
        {
            Console.WriteLine($"{i + 1}.");
            _bookingService.Bookings[i].PrintDetails();
        }
    }

    public void PrintSummary()
    {
        Console.WriteLine("\n========== ROOMRENT MANAGER SUMMARY ==========");

        Console.WriteLine("\nFULL HOUSE DETAILS");
        if (_houseService.Houses.Count == 0)
        {
            Console.WriteLine("No houses added yet.");
        }
        else
        {
            foreach (House house in _houseService.Houses)
            {
                house.PrintDetails();
            }
        }

        PrintRenters();
        PrintBookings();
    }
}
