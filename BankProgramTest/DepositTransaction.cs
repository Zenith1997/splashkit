
public class DepositTransaction
{
    public Account _account;
    public decimal _amount;


    public DepositTransaction(Account account, decimal amount)
    {
        _account = account;
        _amount = amount;
    }

    public void Execute()
    {
        _account.Deposit(_amount);
    }


    public void Print()
    {
        Console.WriteLine($"{this._amount} is deposited to account {this._account.Name}");
    }



}