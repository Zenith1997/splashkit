using System;


namespace List
{
    public enum UserOption
    {
        NEW_VALUE,
        SUM,
        PRINT,
        QUIT
    }

    public class Program
    {
        private static List<double> _values = new List<double>();

        public static void Main()
        {
            UserOption userSelection;
            {
                do
                {
                    userSelection = ReadUserOption();

                    switch (userSelection)
                    {
                        case UserOption.NEW_VALUE:
                            addValueToList();
                            break;

                        case UserOption.SUM:
                            CalcSum();
                            break;

                        case UserOption.PRINT:
                            PrintList();
                            break;

                        case UserOption.QUIT:
                            Console.WriteLine("Goodbye!");
                            break;
                    }

                }
                while (userSelection != UserOption.QUIT);
            }


        }
        public static UserOption ReadUserOption()
        {
            int option = 4;
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("1 Add new value");
            Console.WriteLine("2 Calculate sum");
            Console.WriteLine("3 Print");
            Console.WriteLine("4 Quit");
            Console.WriteLine("---------------------------------------------------");
            do
            {
                try
                {

                    Console.WriteLine("Choose an option [1-4]");
                    option = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception:" + ex.Message);
                    Console.WriteLine("There was a problem parsing your message");
                    option = -1;
                }
            }
            while (option < 1 || option > 4);
            return (UserOption)(option - 1);
        }
        public static int readInteger(string prompt)
        {
            Console.WriteLine(prompt);

            while (true)
            {
                try
                {
                    return Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Enter a valid integer number");
                }
            }
        }
        private static void addValueToList()
        {
            _values.Add(readDouble("Enter new value to be added inside the list"));
        }


        private static void addValueToList_v2()
        {
            _values.Add(readDouble("Enter new value to be added inside the list"));
        }

        private static void CalcSum()
        {
            double sum = 0.0;
            foreach (double value in _values)
            {
                sum += value;
            }
            Console.WriteLine($"The summation is {sum}");
        }
        private static void PrintList()
        {
            for (int i = 0; i < _values.Count; i++)
            {
                Console.WriteLine($"the {i + 1}th element is {_values[i]}");
            }
        }

        public static double readDouble(string prompt)
        {
            Console.WriteLine(prompt);

            while (true)
            {
                string? input = Console.ReadLine();
                if (double.TryParse(input, out double value))
                {
                    return value;
                }

                Console.WriteLine("Enter a valid double number");
            }
        }

        public static void readArray(double[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = readDouble($"Please enter element {i + 1}:");
            }
        }

        public static void showArray(double[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                Console.WriteLine($"Element {i} is: {values[i]}");
            }
        }

        public static void showArraySum(double[] values)
        {
            double sum = 0.0;

            for (int i = 0; i < values.Length; i++)
            {
                sum += values[i];
            }

            Console.WriteLine($"Summation of the array is: {sum}");
        }
    }

}