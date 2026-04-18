using System;
using System.Data;
using SplashKitSDK;


public enum MenuOption
{
    TestName,
    GuessThatNumber,
    Quit
}
namespace NameTester
{
    public class Program
    {
         
        public static void Main()
 
        {
                   MenuOption userSelection;
            do
            {
                 

            userSelection=ReadUserOption();
           switch (userSelection)
            {
                case MenuOption.TestName:
                Console.WriteLine("Test Name...");
                break;
                case MenuOption.GuessThatNumber:
                Console.WriteLine("Guess Number..");
                break;
                case MenuOption.Quit:
                Console.WriteLine("Quit...");

             break;

            } 
            }
            while(userSelection!=MenuOption.Quit);


         

        }

        public static MenuOption ReadUserOption()
        {
            int option ;
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("1 will run Test");
            Console.WriteLine("2 will play Guess that Number");
            Console.WriteLine("3 will Quit");
            Console.WriteLine("---------------------------------------------------");
            do
            {
                Console.WriteLine("Choose an option [1-3]");
                option = Convert.ToInt32(Console.ReadLine());

            }
            while (option < 1 || option > 3);
            return (MenuOption)(option - 1);
        }
    }
}
