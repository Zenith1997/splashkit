using System.Security.Principal;

public class Program
{
    public static void Main(string[] args)
    {
       Account account = new Account("Jakes Account",2000);
        account.Print();
        account.Deposit(100);
        account.Print();
        account.Withdraw(100);
        account.Print();

        Account account1 = new Account("Zeniths account",3000);
        account1.Print();
        account1.Deposit(100);
        account1.Print();
        account1.Withdraw(120);
        account1.Print();
    }
}