using System;

public enum MenuOption
{
    Deposit,
    Withdraw,
    Transfer,
    Print,
    Quit
}

public class Program
{
    public static void Main(string[] args)
    {
        Account account1 = new Account("Zenith Account", 3000);
        Account account2 = new Account("Savings Account", 1000);

        MenuOption userOption;

        do
        {
            userOption = ReadUserOption();

            switch (userOption)
            {
                case MenuOption.Deposit:
                    DoDeposit(account1);
                    break;

                case MenuOption.Withdraw:
                    DoWithdraw(account1);
                    break;

                case MenuOption.Transfer:
                    DoTransfer(account1, account2);
                    break;

                case MenuOption.Print:
                    DoPrint(account1, account2);
                    break;

                case MenuOption.Quit:
                    Console.WriteLine("Goodbye!");
                    break;
            }

        } while (userOption != MenuOption.Quit);
    }

    private static MenuOption ReadUserOption()
    {
        int option = -1;

        do
        {
            Console.WriteLine();
            Console.WriteLine("************************");
            Console.WriteLine("*      BANK MENU       *");
            Console.WriteLine("************************");
            Console.WriteLine("1. Deposit");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Transfer");
            Console.WriteLine("4. Print");
            Console.WriteLine("5. Quit");
            Console.Write("Choose an option [1-5]: ");

            try
            {
                option = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 5.");
                option = -1;
            }

        } while (option < 1 || option > 5);

        return (MenuOption)(option - 1);
    }

    private static void DoDeposit(Account account)
    {
        Console.Write("Enter amount to deposit: ");

        try
        {
            decimal amount = Convert.ToDecimal(Console.ReadLine());
            DepositTransaction deposit = new DepositTransaction(account, amount);
            deposit.Execute();
            deposit.Print();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Deposit failed: {ex.Message}");
        }
    }

    private static void DoWithdraw(Account account)
    {
        Console.Write("Enter amount to withdraw: ");

        try
        {
            decimal amount = Convert.ToDecimal(Console.ReadLine());
            WithdrawTransaction withdraw = new WithdrawTransaction(account, amount);
            withdraw.Execute();
            withdraw.Print();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Withdrawal failed: {ex.Message}");
        }
    }

    private static void DoTransfer(Account fromAccount, Account toAccount)
    {
        Console.Write("Enter amount to transfer: ");

        try
        {
            decimal amount = Convert.ToDecimal(Console.ReadLine());
            TransferTransaction transfer = new TransferTransaction(fromAccount, toAccount, amount);
            transfer.Execute();
            transfer.Print();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Transfer failed: {ex.Message}");
        }
    }

    private static void DoPrint(Account account1, Account account2)
    {
        account1.Print();
        account2.Print();
    }
}