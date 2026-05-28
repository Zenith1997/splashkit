public class PaymentService
{
    private readonly LogAction _logAction;

    public PaymentService(LogAction logAction)
    {
        _logAction = logAction;
    }

    public void RecordPayment(Booking booking, decimal amount)
    {
        booking.RecordPayment(amount);
        _logAction($"Payment recorded: {amount:C}");
    }
}
