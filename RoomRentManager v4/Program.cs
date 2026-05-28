using System;
using System.Collections.Generic;

public enum MenuOption
{
    AddHouse = 1,
    AddRoomToHouse = 2,
    RegisterRenter = 3,
    ViewHouses = 4,
    ViewRenters = 5,
    ViewAvailableRooms = 6,
    CreateBooking = 7,
    ViewBookings = 8,
    Quit = 9
}

class Program
{
    static List<House> houses = new List<House>();
    static List<Renter> renters = new List<Renter>();
    static List<Booking> bookings = new List<Booking>();

    static void Main()
    {
        MenuOption option;

        do
        {
            option = ReadUserOption();

            switch (option)
            {
                case MenuOption.AddHouse:
                    AddHouse();
                    break;

                case MenuOption.AddRoomToHouse:
                    AddRoomToHouse();
                    break;

                case MenuOption.RegisterRenter:
                    RegisterRenter();
                    break;

                case MenuOption.ViewHouses:
                    ViewHouses();
                    break;

                case MenuOption.ViewRenters:
                    ViewRenters();
                    break;

                case MenuOption.ViewAvailableRooms:
                    ViewAvailableRooms();
                    break;

                case MenuOption.CreateBooking:
                    CreateBooking();
                    break;

                case MenuOption.ViewBookings:
                    ViewBookings();
                    break;

                case MenuOption.Quit:
                    Console.WriteLine("Goodbye.");
                    break;
            }

        } while (option != MenuOption.Quit);
    }

    static MenuOption ReadUserOption()
    {
        int option;

        do
        {
            Console.WriteLine("\n==============================");
            Console.WriteLine("       ROOMRENT MANAGER");
            Console.WriteLine("==============================");
            Console.WriteLine("1. Add House");
            Console.WriteLine("2. Add Room to House");
            Console.WriteLine("3. Register Renter");
            Console.WriteLine("4. View Houses");
            Console.WriteLine("5. View Renters");
            Console.WriteLine("6. View Available Rooms");
            Console.WriteLine("7. Create Booking");
            Console.WriteLine("8. View Bookings");
            Console.WriteLine("9. Quit");
            Console.Write("Choose option: ");

            bool valid = int.TryParse(Console.ReadLine(), out option);

            if (!valid || option < 1 || option > 9)
            {
                Console.WriteLine("Invalid option. Please enter a number from 1 to 9.");
            }

        } while (option < 1 || option > 9);

        return (MenuOption)option;
    }

    static void AddHouse()
    {
        Console.WriteLine("\n--- ADD HOUSE ---");

        string address = ReadText("Enter house address: ");
        string suburb = ReadText("Enter house suburb: ");

        House house = new House(address, suburb);
        houses.Add(house);

        Console.WriteLine("House added successfully.");
    }

    static void AddRoomToHouse()
    {
        Console.WriteLine("\n--- ADD ROOM TO HOUSE ---");

        if (houses.Count == 0)
        {
            Console.WriteLine("Add a house before adding rooms.");
            return;
        }

        Console.WriteLine("Choose house:");
        for (int i = 0; i < houses.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {houses[i].Address}, {houses[i].Suburb}");
        }

        int houseNumber = ReadInt("Choose house number: ");
        if (houseNumber < 1 || houseNumber > houses.Count)
        {
            Console.WriteLine("Invalid house number.");
            return;
        }

        House selectedHouse = houses[houseNumber - 1];

        string roomId = ReadText("Enter room ID: ");
        decimal price = ReadDecimal("Enter price per week: ");

        Console.WriteLine("Select room type: 1) Single 2) Master");
        int type = ReadInt("Enter type number: ");

        Room newRoom;
        if (type == 2)
        {
            newRoom = new MasterRoom(roomId, price);
        }
        else
        {
            newRoom = new SingleRoom(roomId, price);
        }

        selectedHouse.AddRoom(newRoom);
        Console.WriteLine("Room added to house successfully.");
    }

    static void RegisterRenter()
    {
        Console.WriteLine("\n--- REGISTER RENTER ---");

        string name = ReadText("Enter renter name: ");
        string contact = ReadText("Enter renter contact: ");
        decimal budget = ReadDecimal("Enter weekly budget: ");

        Renter renter = new Renter(name, contact, budget);
        renters.Add(renter);

        Console.WriteLine("Renter registered successfully.");
    }

    static void ViewHouses()
    {
        Console.WriteLine("\n--- ALL HOUSES ---");

        if (houses.Count == 0)
        {
            Console.WriteLine("No houses have been added yet.");
            return;
        }

        for (int i = 0; i < houses.Count; i++)
        {
            Console.WriteLine($"House #{i + 1}:");
            houses[i].PrintDetails();
        }
    }

    static void ViewRenters()
    {
        Console.WriteLine("\n--- ALL RENTERS ---");

        if (renters.Count == 0)
        {
            Console.WriteLine("No renters have been registered yet.");
            return;
        }

        foreach (Renter renter in renters)
        {
            renter.PrintDetails();
        }
    }

    static void ViewAvailableRooms()
    {
        Console.WriteLine("\n--- AVAILABLE ROOMS ---");

        bool found = false;
        for (int i = 0; i < houses.Count; i++)
        {
            House house = houses[i];
            foreach (Room room in house.Rooms)
            {
                if (room.IsAvailable)
                {
                    Console.WriteLine($"House #{i + 1} - ");
                    room.PrintDetails();
                    found = true;
                }
            }
        }

        if (!found)
        {
            Console.WriteLine("No available rooms found.");
        }
    }

    static void CreateBooking()
    {
        Console.WriteLine("\n--- CREATE BOOKING ---");

        List<(House house, Room room)> available = new List<(House, Room)>();

        for (int i = 0; i < houses.Count; i++)
        {
            foreach (Room room in houses[i].Rooms)
            {
                if (room.IsAvailable)
                {
                    available.Add((houses[i], room));
                }
            }
        }

        if (available.Count == 0)
        {
            Console.WriteLine("No available rooms. Add rooms to houses first.");
            return;
        }

        Console.WriteLine("Available rooms:");
        for (int i = 0; i < available.Count; i++)
        {
            Console.Write($"{i + 1}. ");
            Console.WriteLine($"House: {available[i].house.Address}, {available[i].house.Suburb} - ");
            available[i].room.PrintDetails();
        }

        int choice = ReadInt("Enter available room number to book: ");
        if (choice < 1 || choice > available.Count)
        {
            Console.WriteLine("Invalid room number.");
            return;
        }

        var selected = available[choice - 1];
        Room selectedRoom = selected.room;

        if (renters.Count == 0)
        {
            Console.WriteLine("You need to register renters first.");
            return;
        }

        Console.WriteLine("\nChoose renter:");
        for (int i = 0; i < renters.Count; i++)
        {
            Console.Write($"{i + 1}. ");
            renters[i].PrintDetails();
        }

        int renterNumber = ReadInt("Enter renter number: ");
        if (renterNumber < 1 || renterNumber > renters.Count)
        {
            Console.WriteLine("Invalid renter number.");
            return;
        }

        Renter selectedRenter = renters[renterNumber - 1];

        if (selectedRenter.Budget < selectedRoom.PricePerWeek)
        {
            Console.WriteLine("This renter's budget is lower than the room price.");
            return;
        }

        Booking booking = new Booking(selectedRenter, selectedRoom, DateTime.Today);
        bookings.Add(booking);

        Console.WriteLine("Booking created successfully.");
    }

    static void ViewBookings()
    {
        Console.WriteLine("\n--- ALL BOOKINGS ---");

        if (bookings.Count == 0)
        {
            Console.WriteLine("No bookings have been created yet.");
            return;
        }

        foreach (Booking booking in bookings)
        {
            booking.PrintDetails();
        }
    }

    static string ReadText(string message)
    {
        string input;

        do
        {
            Console.Write(message);
            input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Input cannot be empty.");
            }

        } while (string.IsNullOrWhiteSpace(input));

        return input;
    }

    static int ReadInt(string message)
    {
        int number;
        bool valid;

        do
        {
            Console.Write(message);
            valid = int.TryParse(Console.ReadLine(), out number);

            if (!valid)
            {
                Console.WriteLine("Please enter a valid number.");
            }

        } while (!valid);

        return number;
    }

    static decimal ReadDecimal(string message)
    {
        decimal number;
        bool valid;

        do
        {
            Console.Write(message);
            valid = decimal.TryParse(Console.ReadLine(), out number);

            if (!valid || number <= 0)
            {
                Console.WriteLine("Please enter a valid amount greater than 0.");
            }

        } while (!valid || number <= 0);

        return number;
    }
}
