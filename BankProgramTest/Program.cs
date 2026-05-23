namespace BankProgramTest{



public enum MenuOption
    {
        Deposit,
        Withdraw,
        Print,
        Quit
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
                    acc.Deposit();
                    break;
                    case MenuOption.Withdraw:
                    acc.Withdraw();
                    break;
                    case MenuOption.Print:
                    acc.Print();
                    break;
                    case MenuOption.Quit:
                    Console.WriteLine("Thank you for using Zenith bank services. Have a nice day!");
                     
                }

            }
    while(menuOption!=(MenuOption.Quit));


    }

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
}
}

