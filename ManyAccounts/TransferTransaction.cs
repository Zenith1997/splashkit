using System;

public class TransferTransaction
{
    private Account _fromAccount;
    private Account _toAccount;
    private decimal _amount;
    private WithdrawTransaction _withdraw;
    private DepositTransaction _deposit;
    private bool _executed;
    private bool _reversed;

    public bool Success
    {
        get { return _withdraw.Success && _deposit.Success; }
    }

    public bool Executed
    {
        get { return _executed; }
    }

    public bool Reversed
    {
        get { return _reversed; }
    }

    public TransferTransaction(Account fromAccount, Account toAccount, decimal amount)
    {
        _fromAccount = fromAccount;
        _toAccount = toAccount;
        _amount = amount;
        _withdraw = new WithdrawTransaction(_fromAccount, _amount);
        _deposit = new DepositTransaction(_toAccount, _amount);
        _executed = false;
        _reversed = false;
    }

    public void Execute()
    {
        if (_executed)
        {
            throw new Exception("Cannot execute this transfer twice.");
        }

        

        _withdraw.Execute();

        if (_withdraw.Success)
        {
            _deposit.Execute();
            if (_deposit.Success)
            {
                _executed=true;
            }
            else
            {
                _withdraw.Rollback();
            }
        }
        else
        {
            throw new Exception("Can not execute.The withdraw waas not successful");
        }
    }

    public void Rollback()
    {
        if (!_executed)
        {
            throw new Exception("Transfer has not been executed.");
        }

        if (_reversed)
        {
            throw new Exception("Transfer has already been reversed.");
        }

        if (_deposit.Success)
        {
            _deposit.Rollback();
        }

        if (_withdraw.Success)
        {
            _withdraw.Rollback();
        }

        if (_withdraw.Reversed && _deposit.Reversed)
        {
            _reversed=true;
            _executed=false;
        }
    }

    public void Print()

    {
        if(_withdraw.Success&& _deposit.Success)
        {
        Console.WriteLine($"Transferred {_amount} from {_fromAccount.Name} to {_toAccount.Name}");
        Console.Write("   ");
        _withdraw.Print();
        Console.Write("   ");
        _deposit.Print();
            
        }
        else
        {
            Console.WriteLine("transfer was not successful");
            if (_reversed)
            {
                Console.WriteLine("transfer was reversed");
            }
        }
    }
}