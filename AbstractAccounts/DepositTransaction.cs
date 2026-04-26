using System.Transactions;
using System;

public class DepositTransaction:Transaction
{
    private Account _account;

    private bool _success;

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

    public DepositTransaction(Account account, decimal amount)
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
        _success = _account.Deposit(_amount);

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

                 if (_account.Withdraw(_amount))
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
            
        Console.WriteLine($"Deposit Transaction: {_amount} from {_account.Name}");
        Console.WriteLine($"Success: {_success}");
        Console.WriteLine($"Reversed: {_reversed}");
        }
        else
        {
             Console.WriteLine("Deposit was not successful");
             if(_reversed){
                Console.WriteLine("Deposit was reversed");
             }

        }
    }
}