using System;
using System.Collections.Generic;

public enum MenuOption
{
    AddHouse = 1,
    AddRoom = 2,
    RegisterRenter = 3,
    ViewRooms = 4,
    ViewRenters = 5,
    ViewAvailableRooms = 6,
    CreateBooking = 7,
    ViewBookings = 8,
    Quit = 9
}

class Program
{
    static List<Room> rooms = new List<Room>();
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
                case MenuOption.AddRoom:
                    AddRoom();
                    break;

                case MenuOption.RegisterRenter:
                    RegisterRenter();
                    break;

                case MenuOption.ViewRooms:
                    ViewRooms();
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
            Console.WriteLine("2. Add Room");
            Console.WriteLine("3. Register Renter");
            Console.WriteLine("4. View Rooms");
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

    static void AddRoom()
    {
        Console.WriteLine("\n--- ADD ROOM ---");

        string roomId = ReadText("Enter room ID: ");

        if (FindRoomById(roomId) != null)
        {
            Console.WriteLine("A room with this ID already exists.");
            return;
        }

        decimal price = ReadDecimal("Enter price per week: ");

        Room room = new Room(roomId, price);
        rooms.Add(room);

        Console.WriteLine("Room added successfully.");
    }

    static void AddHouse()
    {
        Console.WriteLine("\n--- ADD HOUSE ---");

        string houseId = ReadText("Enter house ID: ");

        if (FindHouseById(houseId) != null)
        {
            Console.WriteLine("A house with this ID already exists.");
            return;
        }

        string address = ReadText("Enter house address: ");
        int numRooms = ReadInt("Enter number of rooms: ");
        decimal price = ReadDecimal("Enter price per week: ");

        House house = new House(houseId, address, numRooms, price);
        houses.Add(house);

        Console.WriteLine("House added successfully.");
    }

    static House FindHouseById(string houseId)
    {
        foreach (House house in houses)
        {
            if (house.HouseId.Equals(houseId, StringComparison.OrdinalIgnoreCase))
            {
                return house;
            }
        }

        return null;
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

    static void ViewRooms()
    {
        Console.WriteLine("\n--- ALL ROOMS ---");

        if (rooms.Count == 0)
        {
            Console.WriteLine("No rooms have been added yet.");
            return;
        }

        foreach (Room room in rooms)
        {
            room.PrintDetails();
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

        foreach (Room room in rooms)
        {
            if (room.IsAvailable)
            {
                room.PrintDetails();
                found = true;
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

        if (rooms.Count == 0)
        {
            Console.WriteLine("You need to add rooms first.");
            return;
        }

        if (renters.Count == 0)
        {
            Console.WriteLine("You need to register renters first.");
            return;
        }

        ViewAvailableRooms();

        string roomId = ReadText("Enter room ID to book: ");
        Room selectedRoom = FindRoomById(roomId);

        if (selectedRoom == null)
        {
            Console.WriteLine("Room not found.");
            return;
        }

        if (!selectedRoom.IsAvailable)
        {
            Console.WriteLine("This room is already booked.");
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

    static Room FindRoomById(string roomId)
    {
        foreach (Room room in rooms)
        {
            if (room.RoomId.Equals(roomId, StringComparison.OrdinalIgnoreCase))
            {
                return room;
            }
        }

        return null;
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