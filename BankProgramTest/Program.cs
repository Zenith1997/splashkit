namespace BankProgramTest{
public class Program()


{
    


    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Zenith Bank");

        Account acc = new Account("Zenith",5000);
        acc.Print() ;

    }
}
}