
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

        public bool Withdraw( decimal amount)
        {
              this._balance-=amount;
            if (_balance < 500)
            {
                Console.WriteLine("Withdrawal declined. Balance should be more than 500.");
                return false;
            }
            else
            {
                Console.WriteLine($"{amount} withdrawn successfully");
                return true;
            } 
            
          
            
        }

        public bool Deposit( decimal amount)
        {
            this._balance+=amount;
            return true;
        }
    }
