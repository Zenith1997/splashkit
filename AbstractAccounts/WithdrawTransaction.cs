using System;
using System.Transactions;

public class WithdrawTransaction:Transaction
{
    private Account _account;
    private decimal _amount;
    private bool _executed;
    private bool _success = false;
    private bool _reversed;

    public override bool Success
    {
        get { return  this._success; }
    }

    public bool Executed
    {
        get { return _executed; }
    }

    public bool Reversed
    {
        get { return _reversed; }
    }

    public WithdrawTransaction(Account account, decimal amount):base(amount)
    {
        _account = account;
       
     
    }

    public  override void Execute()
    {
       this._success = this._account.Withdraw(this._amount);
    }

    public override void Rollback()
    {
        base.Rollback();
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