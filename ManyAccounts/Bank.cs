using System;

public class Bank
{
    private List<Account> _accounts;

    public Bank()
    {
        _accounts = new List<Account>();
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

    public void ExecuteTransaction(WithdrawTransaction transaction)
    {
        transaction.Execute();
    }
    public void ExecuteTransaction(TransferTransaction transaction)
    {
      transaction.Execute();
    }
    public void ExecuteTransaction(DepositTransaction transaction)
    {
      transaction.Execute();
    }
}

