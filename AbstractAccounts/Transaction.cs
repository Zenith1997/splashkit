
using System;
public abstract class Transaction
{
    
 private Account _account;
    protected decimal _amount;
    protected bool _executed;

    protected bool _reversed;

    private DateTime _dateStamp;

    public bool Executed
    {
        get { return this._executed; }


    }
    
    public bool Reversed
    {
        get { return this._reversed; }

    }

    public DateTime DateStamp
    {
        get { return this._dateStamp; }
    }

    public abstract bool Success
    {
        get;
    }

    public Transaction(decimal amount)
    {
        this._amount = amount;
    }

    public virtual void Print()
    {
        
        Console.WriteLine(this.DateStamp);
    }

    public virtual void Execute()
    {
        if (this._executed)
        {
            throw new Exception("Transaction already executed");
        }
        this._executed = true;
        this._dateStamp = DateTime.Now; 
    }
     public virtual void Rollback()
    {
        if(!this._executed){
            throw new Exception("Rollback declined.Transaction never happened thus cannot rollback");

        }
if(this._reversed)
        {
            throw new Exception("Rollback declined.Transaction already reversed");
        }

    }
}