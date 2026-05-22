namespace BankProgramTest
{
    public class Account
    {
        private decimal _balance;
        private string  _name;


        public Account( string name, decimal balance )
        {
            this._balance = balance;
            this._name = name;
        }

        public void Print( )
        {
            Console.WriteLine($"{_name} your balance is {_balance}");
        }

        public string Name{get {return _name;}}

        public void Withdraw( decimal amount)
        {
            this._balance=amount;
        }

        public void Deposit( decimal amount)
        {
            this._balance+=amount;
        }
    }
}