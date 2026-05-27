using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<Room> rooms = new List<Room>();
        List<Renter> renters = new List<Renter>();
        List<Booking> bookings = new List<Booking>();

        rooms.Add(new Room("R1", 180));
        rooms.Add(new Room("R2", 220));

        Renter renter = new Renter("Zenith", "0400000000", 250);
        renters.Add(renter);

        Booking booking = new Booking(renter, rooms[0], DateTime.Today);
        bookings.Add(booking);

        Console.WriteLine("ROOMS");
        foreach (Room room in rooms)
        {
            room.PrintDetails();
        }

        Console.WriteLine("\nRENTERS");
        foreach (Renter r in renters)
        {
            r.PrintDetails();
        }

        Console.WriteLine("\nBOOKINGS");
        foreach (Booking b in bookings)
        {
            b.PrintDetails();
        }
    }
}
