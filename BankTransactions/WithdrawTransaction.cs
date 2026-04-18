using System;

public class WithdrawTransaction
{
    private Account _account;
    private decimal _amount;
    private bool _executed;
    private bool _success;
    private bool _reversed;

    public bool Success
    {
        get { return _success; }
    }

    public bool Executed
    {
        get { return _executed; }
    }

    public bool Reversed
    {
        get { return _reversed; }
    }

    public WithdrawTransaction(Account account, decimal amount)
    {
        _account = account;
        _amount = amount;
        _executed = false;
        _success = false;
        _reversed = false;
    }

    public void Execute()
    {
        if (_executed)
        {
            throw new Exception("Cannot execute this transaction twice.");
        }

        _executed = true;
        _success = _account.Withdraw(_amount);

        if (!_success)
        {
            throw new Exception("Withdraw transaction failed.");
        }
    }

    public void Rollback()
    {
        if (!_executed||!_success)
        {
            throw new Exception("Transaction has not been executed.");
        }

        if (_reversed)
        {
            throw new Exception("Transaction has already been reversed.");
        }

                 if (_account.Deposit(_amount))
        {
           _reversed=true;
            _executed=false;
            _success=false;
        }
        else
        {
           _reversed=false;
            _executed=true;
            _success=true;  
        }
    }


    public void Print()
    {
        if (_success)
        {
            
        Console.WriteLine($"Withdraw Transaction: {_amount} from {_account.Name}");
        Console.WriteLine($"Success: {_success}");
        Console.WriteLine($"Reversed: {_reversed}");
        }
        else
        {
             Console.WriteLine("withdraw was not successful");
             if(_reversed){
                Console.WriteLine("withdraw was reversed");
             }

        }
    }
}