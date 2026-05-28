public class Payment : IPrintable
{
    private readonly decimal _amount;
    private readonly DateTime _date;
    private readonly string _status;

    public Payment(decimal amount)
    {
        _amount = amount;
        _date = DateTime.Today;
        _status = "Paid";
    }

    public decimal Amount => _amount;
    public string Status => _status;

    public void PrintDetails()
    {
        Console.WriteLine($"Payment: {_amount:C} | Date: {_date.ToShortDateString()} | Status: {_status}");
    }
}
