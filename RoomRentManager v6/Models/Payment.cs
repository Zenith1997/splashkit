public class Payment : IPrintable
{
    public Payment(decimal amount, string method)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Payment amount must be greater than zero.");
        }

        if (string.IsNullOrWhiteSpace(method))
        {
            throw new ArgumentException("Payment method cannot be empty.");
        }

        PaymentId = Guid.NewGuid().ToString("N")[..8].ToUpperInvariant();
        Amount = amount;
        Method = method.Trim();
        Date = DateTime.Today;
        Status = PaymentStatus.Successful;
    }

    public string PaymentId { get; }
    public decimal Amount { get; }
    public string Method { get; }
    public DateTime Date { get; }
    public PaymentStatus Status { get; }

    public void PrintDetails()
    {
        Console.WriteLine($"Payment ID: {PaymentId} | Amount: {Amount:C} | Method: {Method} | Date: {Date:d} | Status: {Status}");
    }
}
