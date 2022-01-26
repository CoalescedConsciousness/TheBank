using System;

namespace Bank1
{
    public class Bank
    {
        public string bankName { get; }
        public List<Account> accountList { get; set; }

        public static int accID;

        public Bank()
        {
            bankName = "EUC Syd Bank";
            accountList = new List<Account>(); 
        }

        /// <summary>
        /// Creates an account based on user input. 
        /// </summary>
        public Account CreateAccount()
        {
            //// Variables:
            decimal accountBalance = 0;
            string accountName;
            bool balanceCheck;
            Account newAccount;

            Console.WriteLine("Please designate Accountholder name: ");
            accountName = Console.ReadLine();
            
            //// Check if user desires to input starting balance
            balanceCheck = _InitBalanceCheck();

            //// Check input and dynamically create using overridden methods
            if (balanceCheck == true)
            {
                
                Console.WriteLine("Please designate Account starting balance: ");
                string input = Console.ReadLine();
                
                try
                {
                    accountBalance = decimal.Parse(input);
                }
                catch (FormatException e) 
                {
                    Console.WriteLine("Invalid starting balance. Setting starting balance to 0.");

                }
                // Create Account with initial balance:
                finally 
                { 
                    newAccount = new Account(accountName, accountBalance, ref accID);
                    accountList.Add(newAccount);
                  
                
                }
                Console.WriteLine($"Account ID: {newAccount.accountNumber} created for {accountName}, with a starting balance of {accountBalance}");
                accountList.Add(newAccount);
                
                return newAccount;


            }
            else
            { 
                newAccount = new Account(accountName, ref accID);
                Console.WriteLine($"Account (ID: {newAccount.accountNumber} created for {accountName}, with a starting balance of {accountBalance}");
                return newAccount;
            }

            
           
        }

        /// <summary>
        /// Deposit a set number to the account given in the parameter
        /// </summary>
        /// <param name="workingAccount"></param>
        /// <returns></returns>
        public void Deposit(Account workingAccount)
        {
            decimal deposit = 0;
            
            decimal init = workingAccount.balance;
            Console.WriteLine("Please designate the amount you wish to deposit: ");
            string input = Console.ReadLine();

            deposit = _getAmount(input);

            workingAccount.balance += deposit;

            Console.WriteLine($"Account belonging to {workingAccount.name} added {deposit} to {init}.. Current balance = {workingAccount.balance}");

        }

        /// <summary>
        /// Withdraws an amount from the balance and prints the result(s)
        /// </summary>
        /// <param name="workingAccount"></param>
        public void Withdraw(Account workingAccount)
        {
            decimal withdrawal = 0;
            decimal init = workingAccount.balance;
            Console.WriteLine("Please designate the amount you wish to deposit: ");
            string input = Console.ReadLine();

            withdrawal = _getAmount(input);

            workingAccount.balance -= withdrawal;

            Console.WriteLine($"Account belonging to {workingAccount.name} subtracted {withdrawal} to {init}.. Current balance = {workingAccount.balance}");

        }

        public void Balance(Account workingAccount)
        {
            Console.WriteLine($"Your balance is currently: {workingAccount.balance}");
        }

        /// <summary>
        /// Used to sanitize the input from the user to ensure data integrity of supplied values.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Decimal value</returns>
        private decimal _getAmount(string input)
        {
            bool validInput = false;
            decimal amount = 0;

            while (!validInput)
            {

                try
                {
                    amount = decimal.Parse(input);
                    validInput = true;
                }
                catch (FormatException)
                {

                    Console.WriteLine("Input is invalid. Please try again, or press [x] to exit");
                    input = Console.ReadLine();
                    if (input == "x" || input == "X")
                    {
                        validInput = true;

                    }
                }
            }

            return amount;

        }

        /// <summary>
        /// Method to check users desired action upon Account-creation.
        /// Checks whether the users input y/Y (yes), or any other character (no)
        /// </summary>
        /// <returns>Boolean value</returns>
        private bool _InitBalanceCheck()
        {
            Console.WriteLine("Do you wish to add a balance now?");
            char reply = Console.ReadKey().KeyChar;
            Console.WriteLine("\n");
            if (reply == char.Parse("Y") || reply == char.Parse("y")) { return true; }
            else return false;
        }

    }
    
}
