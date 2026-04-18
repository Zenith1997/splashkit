using System;

public class Program
{
    public static void Main()
    {
        int numberOfValues = readInteger("Enter a number for the size of the array");
        double[] values = new double[numberOfValues];

        readArray(values);
        showArray(values);
        showArraySum(values);
    }

    public static int readInteger(string prompt)
    {
        Console.WriteLine(prompt);

        while (true)
        {
            try
            {
                return Int32.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Enter a valid integer number"); 
            }
        }
    }

    public static double readDouble(string prompt)
    {
        Console.WriteLine(prompt);

        while (true)
        {
            try
            {
                return Double.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Enter a valid double number");
            }
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