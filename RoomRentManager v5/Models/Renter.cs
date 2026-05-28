public class Renter : IPrintable
{
    private readonly string _name;
    private readonly string _contact;
    private readonly decimal _budget;

    public Renter(string name, string contact, decimal budget)
    {
        _name = name;
        _contact = contact;
        _budget = budget;
    }

    public string Name => _name;
    public string Contact => _contact;
    public decimal Budget => _budget;

    public void PrintDetails()
    {
        Console.WriteLine($"Renter: {_name} | Contact: {_contact} | Budget: {_budget:C}");
    }
}
