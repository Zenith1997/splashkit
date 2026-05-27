using System;

class Program
{
    static void Main()
    {
        string[] roomIds = new string[3];
        decimal[] prices = new decimal[3];
        bool[] available = new bool[3];
        int count = 0;

        int option;
        do
        {
            Console.WriteLine("\nROOMRENT MANAGER - VERSION 1");
            Console.WriteLine("1. Add Room");
            Console.WriteLine("2. View Rooms");
            Console.WriteLine("3. Quit");
            Console.Write("Choose option: ");

            bool validOption = int.TryParse(Console.ReadLine(), out option);
            if (!validOption)
            {
                Console.WriteLine("Please enter a valid number.");
                option = 0;
                continue;
            }

            if (option == 1)
            {
                if (count < roomIds.Length)
                {
                    Console.Write("Enter room ID: ");
                    roomIds[count] = Console.ReadLine() ?? "Unknown";

                    Console.Write("Enter price per week: ");
                    bool validPrice = decimal.TryParse(Console.ReadLine(), out decimal price);

                    if (!validPrice)
                    {
                        Console.WriteLine("Invalid price.");
                        continue;
                    }

                    prices[count] = price;
                    available[count] = true;
                    count++;

                    Console.WriteLine("Room added.");
                }
                else
                {
                    Console.WriteLine("Room list is full.");
                }
            }
            else if (option == 2)
            {
                Console.WriteLine("\nROOM LIST");
                for (int i = 0; i < count; i++)
                {
                    Console.WriteLine($"Room: {roomIds[i]}, Price: {prices[i]:C}, Available: {available[i]}");
                }
            }

        } while (option != 3);
    }
}
