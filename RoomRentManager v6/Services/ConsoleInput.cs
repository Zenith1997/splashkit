public class ConsoleInput
{
    public int ReadIntInRange(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int value) && value >= min && value <= max)
            {
                return value;
            }

            Console.WriteLine($"Please enter a whole number from {min} to {max}.");
        }
    }

    public decimal ReadPositiveDecimal(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();

            if (decimal.TryParse(input, out decimal value) && value > 0)
            {
                return value;
            }

            Console.WriteLine("Please enter an amount greater than zero.");
        }
    }

    public string ReadRequiredText(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input))
            {
                return input.Trim();
            }

            Console.WriteLine("This field cannot be empty.");
        }
    }
}
