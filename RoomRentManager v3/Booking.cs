using System;

public class Booking
{
    private Renter _renter;
    private Room _room;
    private DateTime _startDate;
    private bool _isActive;

    public Booking(Renter renter, Room room, DateTime startDate)
    {
        _renter = renter;
        _room = room;
        _startDate = startDate;
        _isActive = true;
        _room.MarkBooked();
    }

    public void PrintDetails()
    {
        Console.WriteLine($"Booking: {_renter.Name} booked room {_room.RoomId} from {_startDate.ToShortDateString()}. Active: {_isActive}");
    }
}
