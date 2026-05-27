using System;

public class Renter
{
    private string _name;
    private string _contact;
    private decimal _budget;

    public Renter(string name, string contact, decimal budget)
    {
        _name = name;
        _contact = contact;
        _budget = budget;
    }

    public string Name => _name;
    public decimal Budget => _budget;

    public void PrintDetails()
    {
        Console.WriteLine($"Renter: {_name}, Contact: {_contact}, Budget: {_budget:C}");
    }
}
