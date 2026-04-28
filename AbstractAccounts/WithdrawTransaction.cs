using System;
using System.Transactions;

public class WithdrawTransaction : Transaction
{
    private Account _account;

    private bool _success = false;


    public override bool Success
    {
        get { return this._success; }
    }

    public WithdrawTransaction(Account account, decimal amount) : base(amount)
    {
        _account = account;


    }

    public override void Execute()
    {
        this._success = this._account.Withdraw(this._amount);
    }

    public override void Rollback()
    {
        base.Rollback();
        if (_account.Deposit(_amount))
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

            Console.WriteLine($"Withdraw Transaction: {_amount} from {_account.Name}");
            Console.WriteLine($"Success: {_success}");
            Console.WriteLine($"Reversed: {_reversed}");
        }
        else
        {
            Console.WriteLine("withdraw was not successful");
            if (_reversed)
            {
                Console.WriteLine("withdraw was reversed");
            }

        }
    }
}