using System;

public enum MenuOption
{
    TestName,
    GuessThatNumber,
    Quit
}

class Program
{
    public static void Main()
    {
        MenuOption userSelection;

        do
        {
            userSelection = ReadUserOption();

            switch (userSelection)
            {
                case MenuOption.TestName:
                    TestName();
                    break;

                case MenuOption.GuessThatNumber:
                    RunGuessThatNumber();
                    break;

                case MenuOption.Quit:
                    Console.WriteLine("Goodbye!");
                    break;
            }

        } while (userSelection != MenuOption.Quit);
    }

    // MENU
    private static MenuOption ReadUserOption()
    {
        int option = -1;

        do
        {
            Console.WriteLine("******** MENU ********");
            Console.WriteLine("1 - Test Name");
            Console.WriteLine("2 - Guess That Number");
            Console.WriteLine("3 - Quit");
            Console.Write("Choose option [1-3]: ");

            try
            {
                option = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Invalid input. Enter a number.");
                option = -1;
            }

            if (option < 1 || option > 3)
            {
                Console.WriteLine("Invalid option. Please enter a number between 1 and 3.");
            }

        } while (option < 1 || option > 3);

        return (MenuOption)(option - 1);
    }

    // TEST NAME
    private static void TestName()
    {
        Console.Write("Enter your name: ");
        string name = Console.ReadLine();

        Console.WriteLine("Hello " + name);

        if (name.ToLower() == "zenith") // change to your name
        {
            Console.WriteLine("Welcome legend 🔥");
        }
        else if (name.ToLower() == "jake") // friend name
        {
            Console.WriteLine("Hey Jake! Good to see you!");
        }
        else
        {
            Console.WriteLine("Nice to meet you!");
        }
    }

    // GUESS GAME
    private static void RunGuessThatNumber()
    {
        Random rand = new Random();
        int target = rand.Next(100) + 1;

        int guess = 0;
        int min = 1;
        int max = 100;

        Console.WriteLine("Guess a number between 1 and 100");

        while (guess != target)
        {
            guess = ReadGuess(min, max);

            if (guess < target)
            {
                Console.WriteLine("Too low!");
                min = guess + 1;
            }
            else if (guess > target)
            {
                Console.WriteLine("Too high!");
                max = guess - 1;
            }
            else
            {
                Console.WriteLine("Correct! 🎉");
            }
        }
    }

    // READ GUESS
    private static int ReadGuess(int min, int max)
    {
        int guess = -1;

        do
        {
            Console.Write($"Enter guess ({min}-{max}): ");

            try
            {
                guess = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Invalid input. Enter a number.");
                guess = -1;
            }

            if (guess < min || guess > max)
            {
                Console.WriteLine($"Invalid guess. Please enter a number between {min} and {max}.");
            }

        } while (guess < min || guess > max);

        return guess;
    }
}