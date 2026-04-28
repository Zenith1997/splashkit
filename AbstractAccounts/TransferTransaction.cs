using System;

public class TransferTransaction : Transaction
{
    private Account _fromAccount;
    private Account _toAccount;

    private WithdrawTransaction _withdraw;
    private DepositTransaction _deposit;

    public override bool Success
    {
        get { return _withdraw.Success && _deposit.Success; }
    }

    public TransferTransaction(Account fromAccount, Account toAccount, decimal amount)
        : base(amount)
    {
        _fromAccount = fromAccount;
        _toAccount = toAccount;

        _withdraw = new WithdrawTransaction(_fromAccount, _amount);
        _deposit = new DepositTransaction(_toAccount, _amount);
    }

    public override void Execute()
    {
        base.Execute();

        _withdraw.Execute();

        if (!_withdraw.Success)
        {
            throw new Exception("Transfer failed. Withdraw was not successful.");
        }

        try
        {
            _deposit.Execute();

            if (!_deposit.Success)
            {
                _withdraw.Rollback();
                throw new Exception("Transfer failed. Deposit was not successful.");
            }
        }
        catch
        {
            if (_withdraw.Success && !_withdraw.Reversed)
            {
                _withdraw.Rollback();
            }

            throw;
        }
    }

    public override void Rollback()
    {
        base.Rollback();

        if (!Success)
        {
            throw new Exception("Cannot rollback. Transfer was not successful.");
        }

        _deposit.Rollback();
        _withdraw.Rollback();

        if (_deposit.Reversed && _withdraw.Reversed)
        {
            _reversed = true;
        }
        else
        {
            throw new Exception("Transfer rollback failed.");
        }
    }

    public override void Print()
    {
        Console.WriteLine("Transfer Transaction");
        Console.WriteLine($"From: {_fromAccount.Name}");
        Console.WriteLine($"To: {_toAccount.Name}");
        Console.WriteLine($"Amount: {_amount}");
        Console.WriteLine($"Success: {Success}");
        Console.WriteLine($"Executed: {_executed}");
        Console.WriteLine($"Reversed: {_reversed}");
        Console.WriteLine($"Date: {DateStamp}");

        Console.WriteLine();
        _withdraw.Print();

        Console.WriteLine();
        _deposit.Print();
    }
}