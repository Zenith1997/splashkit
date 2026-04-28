using System;
using System.Transactions;

public class Bank
{
    private List<Account> _accounts;
    private List <Transaction> _transactions;

    public Bank()
    {
        _accounts = new List<Account>();
        _transactions = new List<Transaction>();
    }

    public void AddAccount(Account account)
    {
        _accounts.Add(account);
    }

    public Account? GetAccount(string name)
    {
        string searchName = name.ToLower().Trim();

        foreach (Account acc in _accounts)
        {
            if (acc.Name.ToLower().Trim() == searchName)
            {
                return acc;
            }
        }

        return null;
    }

    public void ExecuteTransaction(Transaction transaction)
    {
        this._transactions.Add(transaction);
        transaction.Execute();
    }

    public void TransactionHistory()
    {
        foreach(Transaction transaction in _transactions)
        {
            transaction.Print();
        }
    }
    
}

