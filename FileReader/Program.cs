public class Program
{
    public static void Main()
    {
        double sum = 0,error=0, temperature, averageTemperature = 0;
        string line;
        int count = 0;
        FileStream fs = new FileStream("data.txt", FileMode.Open);

        using (StreamReader reader = new StreamReader(fs))
        {
            while ((line = reader.ReadLine()) != null)
            {
                count++;
                try
                {
                     temperature = Convert.ToDouble(line);
                sum = sum + temperature;
                Console.WriteLine("line " + count + " has temperature of: " + temperature);
                }
                catch (Exception e)
                {
                    Console.WriteLine("line " + count + " is not a valid number: " + line);
                    error++; // don't include this line in the count
                }
               
                line = reader.ReadLine();
            }
            averageTemperature = sum / count-error;
            Console.WriteLine("The average temperature is: " + averageTemperature);
        }
    }
}