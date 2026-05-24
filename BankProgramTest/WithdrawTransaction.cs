public class WithdrawTransaction
{
    



   
    public Account _account;
    public decimal _amount;


    public WithdrawTransaction(Account account, decimal amount)
    {
        _account = account;
        _amount = amount;
    }

    public void Execute()
    {
        _account.Withdraw(_amount);
    }


    


}