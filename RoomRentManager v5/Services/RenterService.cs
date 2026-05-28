public class RenterService
{
    private readonly List<Renter> _renters;
    private readonly LogAction _logAction;

    public RenterService(LogAction logAction)
    {
        _renters = new List<Renter>();
        _logAction = logAction;
    }

    public List<Renter> Renters => _renters;

    public void RegisterRenter(string name, string contact, decimal budget)
    {
        _renters.Add(new Renter(name, contact, budget));
        _logAction($"Renter registered: {name}");
    }
}
