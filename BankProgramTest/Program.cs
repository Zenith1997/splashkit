using System.Security.Cryptography.X509Certificates;
using System.Transactions;
using BankProgramTest;

namespace BankProgramTest{



public enum MenuOption
    {
        Deposit,
        Withdraw,
        Print,
        Quit
    }

public enum WithdrawOptions
    {
        
        FIVEHUNDRED,
        THOUSAND,
        TWOTHOUSAND,

        Other
    }

public class Program()


{
    


    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Zenith Bank");

        Account acc = new Account("Zenith",5000);
MenuOption menuOption;
            do
            {
              menuOption=  ReadUserOption();

                switch (menuOption)
                {
                    case MenuOption.Deposit:
                    DoDeposit(acc);
                    break;
                    case MenuOption.Withdraw:
                    DoWithdraw(acc);
                    break;
                    case MenuOption.Print:
                    acc.Print();
                    break;
                    case MenuOption.Quit:
                    Console.WriteLine("Thank you for using Zenith bank services. Have a nice day!");
                    break;
                     
                }

            }
    while(menuOption!=(MenuOption.Quit));
    }


    public static void DoWithdraw(Account account)
        {
            WithdrawOptions    userOption;

            do
            {
                
         userOption = ReadWithdrawOption();


           switch (userOption)
                {
                    case WithdrawOptions.FIVEHUNDRED:
                        account.Deposit(500);
                        break;
                    case WithdrawOptions.THOUSAND:
                        account.Deposit(1000);
                        break;
                    case WithdrawOptions.TWOTHOUSAND:
                        account.Deposit(2000);
                        break;
                }
            }
            while(userOption!=(WithdrawOptions.Other));
   {
                int opt =-1;
            do{
         
                   Console.WriteLine("Enter the amount to withdraw:");
            try
            {
                
            decimal amount = Convert.ToInt32(Console.ReadLine());
               account.Deposit(amount);
opt = 1;
    
            }  
            catch (Exception)
            {
                Console.WriteLine("Enter a valid number");
          opt = -1;
            }

            }while(opt<1);
         
         

   }}


    

    private static MenuOption ReadUserOption()
        {
            int option;
           
               Console.Write("Choose an option [1-4]: ");
              Console.WriteLine();
            Console.WriteLine("************************");
            Console.WriteLine("*      BANK MENU       *");
            Console.WriteLine("************************");
            Console.WriteLine("1. Deposit");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Print");
            Console.WriteLine("4. Quit");
           

   try
{
    option = Convert.ToInt32(Console.ReadLine());

    if (option < 1 || option > 4)
    {
        //Stop the normal path here. Something invalid happened. Send control to the matching catch
        throw new Exception("Number out of range.");
    }
}
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 4.");
                option = -1;
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid option. Please enter a number between 1 and 4.");
                option = -1;
            }


            //returns the enum by casting an enum to the options integer 
            return (MenuOption)(option - 1);
        }












 private static DepositOption ReadWithdrawOption()
        {
            int option;
            do
            {
                Console.Write("Select an option from below");
                Console.WriteLine("[1]:500");
                Console.WriteLine("[2]:1000");
                Console.WriteLine("[3]:2000");
                Console.WriteLine("[4]:Other");
                try
                {
                    option=Convert.ToInt32(Console.ReadLine());
                    if (option < 1 || option > 4)
                    {
                        throw new Exception("Number out of range");
                    }
                }
                catch (Exception )
                {
                    Console.WriteLine($"Choose from 1-4,");
                    option = -1;
                }
              

                      return (DepositOption)(option - 1);
                
            }while(option!=4);
}


}
}
