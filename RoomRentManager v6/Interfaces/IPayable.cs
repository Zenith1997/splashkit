public interface IPayable
{
    OperationResult RecordPayment(decimal amount, string method);
}
