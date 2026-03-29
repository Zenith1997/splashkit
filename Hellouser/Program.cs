public class Program
{
    public static void Main(string[] args)
    {
        string name;
        string inputText;
        int heightInCM;
        double weightInKG;
        double heightInMeters;
        double bmi;

        Console.WriteLine("Enter your name");
        name = Console.ReadLine();
        Console.WriteLine($"Hello {name}");
        Console.WriteLine("Enter height in centimeters");
        inputText = Console.ReadLine();
        heightInCM =Convert.ToInt32(inputText);
        heightInMeters =heightInCM/100.0;
        Console.WriteLine($"Height in meters:{heightInMeters}");
        Console.WriteLine("Enter weight in kg ");
        inputText =Console.ReadLine();
        weightInKG =Convert.ToDouble(inputText);
        Console.WriteLine($"Weight is {weightInKG}kg");
        
        bmi = weightInKG/(heightInMeters*heightInMeters);
        Console.WriteLine($"{name} your BMI is {bmi}");


    }
}

