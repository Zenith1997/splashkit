public class PaymentService
{
    private readonly LogAction _logAction;

    public PaymentService(LogAction logAction)
    {
        _logAction = logAction;
    }

    public OperationResult RecordPayment(Booking booking, decimal amount, string method)
    {
        if (booking == null)
        {
            return OperationResult.Fail("Booking is required.");
        }

        if (amount <= 0)
        {
            return OperationResult.Fail("Payment amount must be greater than zero.");
        }

        // Interface usage: the booking is treated through the IPayable contract.
        IPayable payableBooking = booking;
        OperationResult result = payableBooking.RecordPayment(amount, method);

        if (result.Success)
        {
            _logAction($"Payment recorded for booking {booking.BookingId}: {amount:C} via {method}");
        }

        return result;
    }
}
