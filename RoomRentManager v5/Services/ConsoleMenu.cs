public class ConsoleMenu
{
    private readonly HouseService _houseService;
    private readonly RenterService _renterService;
    private readonly BookingService _bookingService;
    private readonly PaymentService _paymentService;

    public ConsoleMenu(HouseService houseService, RenterService renterService, BookingService bookingService, PaymentService paymentService)
    {
        _houseService = houseService;
        _renterService = renterService;
        _bookingService = bookingService;
        _paymentService = paymentService;
    }

    public void Run()
    {
        int option;
        do
        {
            PrintMenu();
            bool validInput = int.TryParse(Console.ReadLine(), out option);

            if (!validInput)
            {
                Console.WriteLine("Please enter a valid number.");
                option = 0;
                continue;
            }

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
                    ViewAvailableRooms();
                    break;
                case 5:
                    CreateBooking();
                    break;
                case 6:
                    RecordPayment();
                    break;
                case 7:
                    PrintSummary();
                    break;
                case 8:
                    Console.WriteLine("Goodbye.");
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }

        } while (option != 8);
    }

    private void PrintMenu()
    {
        Console.WriteLine("\n========== ROOMRENT MANAGER ==========");
        Console.WriteLine("1. Add House");
        Console.WriteLine("2. Add Room to House");
        Console.WriteLine("3. Register Renter");
        Console.WriteLine("4. View Available Rooms");
        Console.WriteLine("5. Create Booking");
        Console.WriteLine("6. Record Payment");
        Console.WriteLine("7. Print Summary");
        Console.WriteLine("8. Quit");
        Console.Write("Choose option: ");
    }

    private void AddHouse()
    {
        Console.Write("Enter house address: ");
        string address = Console.ReadLine() ?? "Unknown Address";

        Console.Write("Enter suburb: ");
        string suburb = Console.ReadLine() ?? "Unknown Suburb";

        _houseService.AddHouse(address, suburb);
    }

    private void AddRoomToHouse()
    {
        if (_houseService.Houses.Count == 0)
        {
            Console.WriteLine("Please add a house first.");
            return;
        }

        PrintHouses();
        Console.Write("Choose house number: ");
        int houseIndex = ReadNumber() - 1;

        Console.Write("Enter room ID: ");
        string roomId = Console.ReadLine() ?? "Unknown";

        Console.Write("Enter price per week: ");
        decimal price = ReadDecimal();

        Console.WriteLine("Choose room type:");
        Console.WriteLine("1. Single Room");
        Console.WriteLine("2. Master Room");
        Console.Write("Option: ");
        int type = ReadNumber();

        Room room = type == 1 ? new SingleRoom(roomId, price) : new MasterRoom(roomId, price);

        bool added = _houseService.AddRoomToHouse(houseIndex, room);
        Console.WriteLine(added ? "Room added." : "Invalid house number.");
    }

    private void RegisterRenter()
    {
        Console.Write("Enter renter name: ");
        string name = Console.ReadLine() ?? "Unknown";

        Console.Write("Enter contact: ");
        string contact = Console.ReadLine() ?? "Unknown";

        Console.Write("Enter weekly budget: ");
        decimal budget = ReadDecimal();

        _renterService.RegisterRenter(name, contact, budget);
    }

    private void ViewAvailableRooms()
    {
        Console.WriteLine("\nAVAILABLE ROOMS");
        List<Room> availableRooms = _houseService.GetAllAvailableRooms();

        if (availableRooms.Count == 0)
        {
            Console.WriteLine("No rooms available.");
            return;
        }

        foreach (Room room in availableRooms)
        {
            room.PrintDetails();
        }
    }

    private void CreateBooking()
    {
        if (_renterService.Renters.Count == 0)
        {
            Console.WriteLine("Please register a renter first.");
            return;
        }

        PrintRenters();
        Console.Write("Choose renter number: ");
        int renterIndex = ReadNumber() - 1;

        if (renterIndex < 0 || renterIndex >= _renterService.Renters.Count)
        {
            Console.WriteLine("Invalid renter number.");
            return;
        }

        ViewAvailableRooms();
        Console.Write("Enter room ID to book: ");
        string roomId = Console.ReadLine() ?? "";

        Room? selectedRoom = _houseService.FindAvailableRoomById(roomId);
        if (selectedRoom == null)
        {
            Console.WriteLine("Room not found or not available.");
            return;
        }

        bool success = _bookingService.CreateBooking(_renterService.Renters[renterIndex], selectedRoom);
        Console.WriteLine(success ? "Booking created." : "Booking failed. Check availability or renter budget.");
    }

    private void RecordPayment()
    {
        if (_bookingService.Bookings.Count == 0)
        {
            Console.WriteLine("No bookings available.");
            return;
        }

        PrintBookings();
        Console.Write("Choose booking number: ");
        int bookingIndex = ReadNumber() - 1;

        if (bookingIndex < 0 || bookingIndex >= _bookingService.Bookings.Count)
        {
            Console.WriteLine("Invalid booking number.");
            return;
        }

        Console.Write("Enter payment amount: ");
        decimal amount = ReadDecimal();
        _paymentService.RecordPayment(_bookingService.Bookings[bookingIndex], amount);
    }

    private void PrintSummary()
    {
        Console.WriteLine("\n========== ROOMRENT SUMMARY ==========");

        Console.WriteLine("\nHOUSES");
        foreach (House house in _houseService.Houses)
        {
            house.PrintDetails();
        }

        Console.WriteLine("\nRENTERS");
        foreach (Renter renter in _renterService.Renters)
        {
            renter.PrintDetails();
        }

        Console.WriteLine("\nBOOKINGS");
        foreach (Booking booking in _bookingService.Bookings)
        {
            booking.PrintDetails();
        }
    }

    private void PrintHouses()
    {
        for (int i = 0; i < _houseService.Houses.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_houseService.Houses[i].Address}, {_houseService.Houses[i].Suburb}");
        }
    }

    private void PrintRenters()
    {
        for (int i = 0; i < _renterService.Renters.Count; i++)
        {
            Console.Write($"{i + 1}. ");
            _renterService.Renters[i].PrintDetails();
        }
    }

    private void PrintBookings()
    {
        for (int i = 0; i < _bookingService.Bookings.Count; i++)
        {
            Console.Write($"{i + 1}. ");
            _bookingService.Bookings[i].PrintDetails();
        }
    }

    private int ReadNumber()
    {
        int value;
        while (!int.TryParse(Console.ReadLine(), out value))
        {
            Console.Write("Enter a valid number: ");
        }
        return value;
    }

    private decimal ReadDecimal()
    {
        decimal value;
        while (!decimal.TryParse(Console.ReadLine(), out value))
        {
            Console.Write("Enter a valid amount: ");
        }
        return value;
    }
}
