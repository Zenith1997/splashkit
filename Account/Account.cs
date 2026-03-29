using System;

public class Account
{
    private decimal _balance;
    private string _name;


    public Account(string name, decimal startingBalance)
    {
        _name = name;
        _balance = startingBalance;
    }

    public void Deposit(decimal amountToAdd)
    {
        _balance = _balance + amountToAdd;
    }
    public void Withdraw(decimal amountToWithDraw)
    {
        _balance = _balance - amountToWithDraw;
    }

    public string Name
    {
        get { return _name; }

    }
    public void Print()
    {
        Console.WriteLine($"{_name} balance is {_balance}");
    }
}
