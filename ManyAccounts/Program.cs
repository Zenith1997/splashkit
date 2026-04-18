using System;
using System.ComponentModel;

public enum MenuOption
{
    AddAccount,
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
        Bank bank = new Bank();



        MenuOption userOption;

        do
        {
            userOption = ReadUserOption();

            switch (userOption)
            {
                case MenuOption.AddAccount:
                    DoAddAccount(bank);
                    break;
                case MenuOption.Deposit:
                    DoDeposit(bank);
                    break;

                case MenuOption.Withdraw:
                    DoWithdraw(bank);
                    break;

                case MenuOption.Transfer:
                    DoTransfer(bank);
                    break;

                case MenuOption.Print:
                    DoPrint(bank);
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
            Console.WriteLine("1. Add Account");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Withdraw");
            Console.WriteLine("4. Transfer");
            Console.WriteLine("5. Print");
            Console.WriteLine("6. Quit");
            Console.Write("Choose an option [1-6]: ");

            try
            {
                option = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 6.");
                option = -1;
            }

        } while (option < 1 || option > 6);

        return (MenuOption)(option - 1);
    }

    public static void DoAddAccount(Bank bank)
    {
        Console.WriteLine("Please add a name to the account");
        string accountName = Console.ReadLine() ?? string.Empty;
        while (true)
        {
            try
            {
                Console.WriteLine("please enter the opening balance");
                decimal openingBalance = Convert.ToDecimal(Console.ReadLine());
                if (openingBalance < 0)
                {
                    throw new Exception();

                }
                bank.AddAccount(new Account(accountName, openingBalance));
                return;

            }
            catch
            {
                Console.WriteLine("Enter a valid number");
            }
        }
    }
    public static Account? findAccount(Bank bank)
    {
        Console.WriteLine("Enter account name: ");
        string name = Console.ReadLine() ?? string.Empty;
        Account? result = bank.GetAccount(name);

        if (result == null)
        {
            Console.WriteLine("No account associated with the given name");
        }

        return result;

    }
    private static void DoDeposit(Bank bank)
    {
        Account? account = findAccount(bank);
        if (account == null)
        {
            return;
        }

        Console.Write("Enter amount to deposit: ");

        try
        {
            decimal amount = Convert.ToDecimal(Console.ReadLine());
            DepositTransaction deposit = new DepositTransaction(account, amount);
            bank.ExecuteTransaction(deposit);
            deposit.Print();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Deposit failed: {ex.Message}");
        }
    }

    private static void DoWithdraw(Bank bank)
    {
        Account? account = findAccount(bank);
        if (account == null)
        {
            return;
        }

        Console.Write("Enter amount to withdraw: ");

        try
        {
            decimal amount = Convert.ToDecimal(Console.ReadLine());
            WithdrawTransaction withdraw = new WithdrawTransaction(account, amount);
            bank.ExecuteTransaction(withdraw);

            withdraw.Print();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Withdrawal failed: {ex.Message}");
        }
    }

    private static void DoTransfer(Bank bank)
    {
        Console.WriteLine("Enter the account name");
        Account? fromAccount = findAccount(bank);
        if (fromAccount == null)
        {
            return;
        }


        Console.WriteLine("Enter the account name you want to transfer");

        Account? toAccount = findAccount(bank);
        if (toAccount == null)
        {
            return;
        }
if(fromAccount==toAccount){throw new Exception("same account");}
        Console.Write("Enter amount to transfer: ");

        try
        {
            decimal amount = Convert.ToDecimal(Console.ReadLine());
            TransferTransaction transfer = new TransferTransaction(fromAccount, toAccount, amount);
            bank.ExecuteTransaction(transfer);
            transfer.Print();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Transfer failed: {ex.Message}");
        }
    }

    private static void DoPrint(Bank bank)
    {
        Account? account1 = findAccount(bank);
        if (account1 == null)
        {
            return;
        }

        account1.Print();

    }
}