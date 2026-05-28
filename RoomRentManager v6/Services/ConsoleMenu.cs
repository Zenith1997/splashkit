public class ConsoleMenu
{
    private readonly HouseService _houseService;
    private readonly RenterService _renterService;
    private readonly BookingService _bookingService;
    private readonly PaymentService _paymentService;
    private readonly ReportService _reportService;
    private readonly DemoDataSeeder _demoDataSeeder;
    private readonly ConsoleInput _input;
    private readonly LogAction _logAction;

    public ConsoleMenu(
        HouseService houseService,
        RenterService renterService,
        BookingService bookingService,
        PaymentService paymentService,
        ReportService reportService,
        DemoDataSeeder demoDataSeeder,
        ConsoleInput input,
        LogAction logAction)
    {
        _houseService = houseService;
        _renterService = renterService;
        _bookingService = bookingService;
        _paymentService = paymentService;
        _reportService = reportService;
        _demoDataSeeder = demoDataSeeder;
        _input = input;
        _logAction = logAction;
    }

    public void Run()
    {
        int option;
        do
        {
            PrintMenu();
            option = _input.ReadIntInRange("Choose option: ", 1, 10);

            try
            {
                switch (option)
                {
                    case 1:
                        AddHouse();
                        break;
                    case 2:
                        AddRoomToHouse();
                        break;
                    case 3:
                        RegisterRenter();
                        break;
                    case 4:
                        _reportService.PrintAvailableRooms();
                        break;
                    case 5:
                        CreateBooking();
                        break;
                    case 6:
                        RecordPayment();
                        break;
                    case 7:
                        EndBooking();
                        break;
                    case 8:
                        _reportService.PrintSummary();
                        break;
                    case 9:
                        ShowResult(_demoDataSeeder.LoadDemoData());
                        break;
                    case 10:
                        Console.WriteLine("Goodbye.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred. The program recovered safely.");
                _logAction($"ERROR: {ex.Message}");
            }

        } while (option != 10);
    }

    private void PrintMenu()
    {
        Console.WriteLine("\n========== ROOMRENT MANAGER - HD VERSION ==========");
        Console.WriteLine("1. Add House");
        Console.WriteLine("2. Add Room to House");
        Console.WriteLine("3. Register Renter");
        Console.WriteLine("4. View Available Rooms");
        Console.WriteLine("5. Create Booking");
        Console.WriteLine("6. Record Payment");
        Console.WriteLine("7. End Booking");
        Console.WriteLine("8. Print Summary");
        Console.WriteLine("9. Load Demo Data");
        Console.WriteLine("10. Quit");
    }

    private void AddHouse()
    {
        string address = _input.ReadRequiredText("Enter house address: ");
        string suburb = _input.ReadRequiredText("Enter suburb: ");
        ShowResult(_houseService.AddHouse(address, suburb));
    }

    private void AddRoomToHouse()
    {
        if (_houseService.Houses.Count == 0)
        {
            Console.WriteLine("Add a house before adding rooms.");
            return;
        }

        _reportService.PrintHouses();
        int houseNumber = _input.ReadIntInRange("Choose house number: ", 1, _houseService.Houses.Count);
        string roomId = _input.ReadRequiredText("Enter room ID: ");
        decimal price = _input.ReadPositiveDecimal("Enter price per week: ");

        Console.WriteLine("1. Single Room");
        Console.WriteLine("2. Master Room");
        int typeNumber = _input.ReadIntInRange("Choose room type: ", 1, 2);
        RoomType roomType = (RoomType)typeNumber;

        ShowResult(_houseService.AddRoomToHouse(houseNumber, roomType, roomId, price));
    }

    private void RegisterRenter()
    {
        string name = _input.ReadRequiredText("Enter renter name: ");
        string contact = _input.ReadRequiredText("Enter contact details: ");
        decimal budget = _input.ReadPositiveDecimal("Enter weekly budget: ");
        ShowResult(_renterService.RegisterRenter(name, contact, budget));
    }

    private void CreateBooking()
    {
        if (_renterService.Renters.Count == 0)
        {
            Console.WriteLine("Register a renter before creating a booking.");
            return;
        }

        if (_houseService.GetAllAvailableRooms().Count == 0)
        {
            Console.WriteLine("There are no available rooms to book.");
            return;
        }

        _reportService.PrintRenters();
        int renterNumber = _input.ReadIntInRange("Choose renter number: ", 1, _renterService.Renters.Count);
        OperationResult<Renter> renterResult = _renterService.GetRenterByNumber(renterNumber);

        _reportService.PrintAvailableRooms();
        string roomId = _input.ReadRequiredText("Enter room ID to book: ");
        Room? selectedRoom = _houseService.FindAvailableRoomById(roomId);

        if (!renterResult.Success || renterResult.Value == null)
        {
            Console.WriteLine(renterResult.Message);
            return;
        }

        if (selectedRoom == null)
        {
            Console.WriteLine("Room not found or not available.");
            return;
        }

        ShowResult(_bookingService.CreateBooking(renterResult.Value, selectedRoom));
    }

    private void RecordPayment()
    {
        if (_bookingService.Bookings.Count == 0)
        {
            Console.WriteLine("No bookings exist yet.");
            return;
        }

        _reportService.PrintBookings();
        int bookingNumber = _input.ReadIntInRange("Choose booking number: ", 1, _bookingService.Bookings.Count);
        OperationResult<Booking> bookingResult = _bookingService.GetBookingByNumber(bookingNumber);

        if (!bookingResult.Success || bookingResult.Value == null)
        {
            Console.WriteLine(bookingResult.Message);
            return;
        }

        decimal amount = _input.ReadPositiveDecimal("Enter payment amount: ");
        string method = _input.ReadRequiredText("Enter payment method: ");
        ShowResult(_paymentService.RecordPayment(bookingResult.Value, amount, method));
    }

    private void EndBooking()
    {
        if (_bookingService.Bookings.Count == 0)
        {
            Console.WriteLine("No bookings exist yet.");
            return;
        }

        _reportService.PrintBookings();
        int bookingNumber = _input.ReadIntInRange("Choose booking number to end: ", 1, _bookingService.Bookings.Count);
        ShowResult(_bookingService.EndBooking(bookingNumber));
    }

    private void ShowResult(OperationResult result)
    {
        Console.WriteLine(result.Success ? $"Success: {result.Message}" : $"Error: {result.Message}");
    }
}
