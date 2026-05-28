public class RenterService
{
    private readonly List<Renter> _renters;
    private readonly LogAction _logAction;

    public RenterService(LogAction logAction)
    {
        _renters = new List<Renter>();
        _logAction = logAction;
    }

    public IReadOnlyList<Renter> Renters => _renters.AsReadOnly();

    public OperationResult<Renter> RegisterRenter(string name, string contact, decimal weeklyBudget)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return OperationResult<Renter>.Fail("Renter name cannot be empty.");
        }

        if (string.IsNullOrWhiteSpace(contact))
        {
            return OperationResult<Renter>.Fail("Contact details cannot be empty.");
        }

        if (weeklyBudget <= 0)
        {
            return OperationResult<Renter>.Fail("Weekly budget must be greater than zero.");
        }

        bool duplicateContact = _renters.Any(renter => renter.Contact.Equals(contact.Trim(), StringComparison.OrdinalIgnoreCase));
        if (duplicateContact)
        {
            return OperationResult<Renter>.Fail("A renter with this contact already exists.");
        }

        Renter newRenter = new Renter(name, contact, weeklyBudget);
        _renters.Add(newRenter);
        _logAction($"Renter registered: {newRenter.Name} ({newRenter.RenterId})");
        return OperationResult<Renter>.Ok(newRenter, "Renter registered successfully.");
    }

    public OperationResult<Renter> GetRenterByNumber(int renterNumber)
    {
        if (renterNumber < 1 || renterNumber > _renters.Count)
        {
            return OperationResult<Renter>.Fail("Invalid renter number.");
        }

        return OperationResult<Renter>.Ok(_renters[renterNumber - 1], "Renter found.");
    }
}
