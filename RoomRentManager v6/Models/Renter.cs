public class Renter : IPrintable
{
    public Renter(string name, string contact, decimal weeklyBudget)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Renter name cannot be empty.");
        }

        if (string.IsNullOrWhiteSpace(contact))
        {
            throw new ArgumentException("Contact details cannot be empty.");
        }

        if (weeklyBudget <= 0)
        {
            throw new ArgumentException("Weekly budget must be greater than zero.");
        }

        RenterId = Guid.NewGuid().ToString("N")[..8].ToUpperInvariant();
        Name = name.Trim();
        Contact = contact.Trim();
        WeeklyBudget = weeklyBudget;
    }

    public string RenterId { get; }
    public string Name { get; }
    public string Contact { get; }
    public decimal WeeklyBudget { get; }

    public void PrintDetails()
    {
        Console.WriteLine($"Renter ID: {RenterId} | Name: {Name} | Contact: {Contact} | Budget: {WeeklyBudget:C}/week");
    }
}
