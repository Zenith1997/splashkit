using System;

public enum MenuOption
{
    Deposit,
    Withdraw,
    Print,
    Quit
}

public class Program
{
    public static void Main(string[] args)
    {
        Account account = new Account("Zenith Account", 3000);

        MenuOption userOption;

        do
        {
            userOption = ReadUserOption();

            switch (userOption)
            {
                case MenuOption.Deposit:
                    DoDeposit(account);
                    break;

                case MenuOption.Withdraw:
                    DoWithdraw(account);
                    break;

                case MenuOption.Print:
                    DoPrint(account);
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
            Console.WriteLine("3. Print");
            Console.WriteLine("4. Quit");
            Console.Write("Choose an option [1-4]: ");

            try
            {
                option = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 4.");
                option = -1;
            }

        } while (option < 1 || option > 4);

        return (MenuOption)(option - 1);
    }

    private static void DoDeposit(Account account)
    {
        decimal amount;

        Console.Write("Enter amount to deposit: ");

        try
        {
            amount = Convert.ToDecimal(Console.ReadLine());
        }
        catch
        {
            Console.WriteLine("Invalid amount entered.");
            return;
        }

        if (account.Deposit(amount))
        {
            Console.WriteLine("Deposit successful.");
        }
        else
        {
            Console.WriteLine("Deposit failed. Amount must be greater than 0.");
        }
    }

    private static void DoWithdraw(Account account)
    {
        decimal amount;

        Console.Write("Enter amount to withdraw: ");

        try
        {
            amount = Convert.ToDecimal(Console.ReadLine());
        }
        catch
        {
            Console.WriteLine("Invalid amount entered.");
            return;
        }

        if (account.Withdraw(amount))
        {
            Console.WriteLine("Withdrawal successful.");
        }
        else
        {
            Console.WriteLine("Withdrawal failed. Amount must be greater than 0 and not exceed balance.");
        }
    }

    private static void DoPrint(Account account)
    {
        account.Print();
    }
}