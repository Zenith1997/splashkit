public class Program
{
    public static void Main()
    {
        double sum = 0, temperature, averageTemperature = 0;
        string line;
        int count = 0;
        FileStream fs = new FileStream("data.txt", FileMode.Open);

        using (StreamReader reader = new StreamReader(fs))
        {
            while ((line = reader.ReadLine()) != null)
            {
                count++;
                temperature = Convert.ToDouble(line);
                sum = sum + temperature;
                Console.WriteLine("line " + count + " has temperature of: " + temperature);
                line = reader.ReadLine();
            }
            averageTemperature = sum / count;
            Console.WriteLine("The average temperature is: " + averageTemperature);
        }
    }
}