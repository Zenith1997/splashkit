using System.Transactions;
using System;

public class DepositTransaction : Transaction
{
    private Account _account;

    private bool _success;

    public override bool Success
    {
        get { return _success; }
    }

    public DepositTransaction(Account account, decimal amount) : base(amount)
    {
        _account = account;


    }

    public override void Execute()
    {
        base.Execute();

        _success = _account.Deposit(_amount);

        if (!_success)
        {
            throw new Exception("Withdraw transaction failed.");
        }
    }


    public override void Rollback()
    {
        base.Rollback();

        if (_account.Withdraw(_amount))
        {
            _reversed = true;
            _executed = false;
            _success = false;
        }
        else
        {
            _reversed = false;
            _executed = true;
            _success = true;
        }
    }


    public override void Print()
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
            if (_reversed)
            {
                Console.WriteLine("Deposit was reversed");
            }

        }
    }
}