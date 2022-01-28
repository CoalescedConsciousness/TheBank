using System;

namespace Bank1
{
    public class Bank
    {
        public string bankName { get; }
        public List<Account> accountList { get; set; }

        public FileRepository fileRepository { get; set; }

        public static int accID;

        public Bank()
        {
            bankName = "EUC Syd Bank";
            accountList = new List<Account>();
            fileRepository = new FileRepository();
        }

        #region Bank Management
        public void BankBalance(Bank bank)
        {
            decimal total = 0;
            foreach (Account x in accountList)
            {
                total += x.Balance;
            }
            Console.WriteLine($"Total Bank Balance: {total}");
        }
        
        public void ChargeInterest(Bank bank)
        {
            foreach (Account x in accountList)
            {
                x.ChargeInterest();
                FileLogger.WriteToLog("Interest applied.. How'u'doin'?");
            }
        }
        #endregion


        #region Account Management
        /// <summary>
        /// Deposit a set number to the account given in the parameter
        /// </summary>
        /// <param name="workingAccount"></param>
        /// <returns></returns>
        public void Deposit(Account workingAccount)
        {
            decimal deposit = 0;
            
            decimal init = workingAccount.Balance;
            Console.WriteLine("Please designate the amount you wish to deposit: ");
            string input = Console.ReadLine();

            deposit = _getAmount(input);

            workingAccount.Balance += deposit;

            string logMsg = $"Account belonging to {workingAccount.Name} added {deposit} to {init}.. Current balance = {workingAccount.Balance}";
            Console.WriteLine(logMsg);
            FileLogger.WriteToLog(logMsg);
        }

        /// <summary>
        /// Withdraws an amount from the balance and prints the result(s)
        /// </summary>
        /// <param name="workingAccount"></param>
        public void Withdraw(Account workingAccount)
        {
            decimal withdrawal = 0;
            decimal init = workingAccount.Balance;
            Console.WriteLine("Please designate the amount you wish to deposit: ");
            string input = Console.ReadLine();

            withdrawal = _getAmount(input);

            workingAccount.Balance -= withdrawal;
            string logMsg = $"Account belonging to {workingAccount.Name} subtracted {withdrawal} to {init}.. Current balance = {workingAccount.Balance}";
            Console.WriteLine(logMsg);
            FileLogger.WriteToLog(logMsg);
        }

        /// <summary>
        /// Shows ACCOUNT Balance
        /// </summary>
        /// <param name="workingAccount"></param>
        public void Balance(Account workingAccount)
        {
            Console.WriteLine($"Your balance is currently: {workingAccount.Balance}");
        }
        #endregion

        #region Account Administration
        /// <summary>
        /// Creates an account based on user input. 
        /// </summary>
        public Account CreateAccount(string accType = "cons")
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
                    newAccount = _CreateAccount(accType, accountName, accountBalance);
                    accountList.Add(newAccount);


                }
                string logMsg = $"Account ID: {newAccount.AccountNumber} created for {accountName}, with a starting balance of {accountBalance}";
                FileLogger.WriteToLog(logMsg);
                Console.WriteLine(logMsg);
                return newAccount;


            }
            else
            {
                newAccount = _CreateAccount(accType, accountName, accountBalance);
                accountList.Add(newAccount);
                string logMsg = $"Account ID: {newAccount.AccountNumber} created for {accountName}, with a starting balance of {accountBalance}";
                FileLogger.WriteToLog(logMsg);
                Console.WriteLine(logMsg);
                return newAccount;
            }



        }

        /// <summary>
        /// Changes the current account.
        /// </summary>
        /// <param name="bank"></param>
        /// <param name="workingAccount"></param>
        /// <returns></returns>
        public int ChangeAccount(Bank bank, Account workingAccount)
        {
            Console.WriteLine(bank.accountList);
            Console.WriteLine($"Available Accounts:");
            Console.WriteLine("-------------------");
            Console.WriteLine("[[ Account ID // Holder Name ]]");
            foreach (Account acc in bank.accountList)

                Console.WriteLine($"-- {acc.AccountNumber} // {acc.Name}");

            Console.WriteLine("\nPlease select the account you wish to log in to:");

            int index = 0;
            bool validSelect = false;
            while (!validSelect)
            {
                string userSelection = Console.ReadLine();
                if (int.TryParse(userSelection, out index))
                {

                    if (bank.accountList.Any(acc => acc.AccountNumber == index))
                    {
                        validSelect = true;
                    }
                    else { Console.WriteLine("Account not found. Please try again."); }
                }
                else { Console.WriteLine("Input is not an integer. Please refer to the account ID's."); }
            }
            Console.WriteLine("Account changed...");

            return index;
        }

        #endregion


        #region Helper Methods
        /// <summary>
        /// Takes care of handling account creation based on type. Note the default account is MasterCard (Consumer) Account.
        /// </summary>
        /// <param name="accType"></param>
        /// <param name="accountName"></param>
        /// <param name="accountBalance"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        private Account _CreateAccount(string accType, string accountName, decimal accountBalance)
        {

            Account newAccount;
            if (accType == "check")
            {
                newAccount = new CheckingAccount(accountName, accountBalance);
            }
            else if (accType == "saving")
            {
                newAccount = new SavingsAccount(accountName, accountBalance);
            }
            else  // The string will default to "cons", but using else here so there will never be a risk of returning a false value, such as in case of user-error.
            {
                newAccount = new MasterCardAccount(accountName, accountBalance);
            }
            return newAccount;
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
            Console.WriteLine("Do you wish to add a balance now? [Y/N]");
            char reply = Console.ReadKey().KeyChar;
            Console.WriteLine("\n");
            if (reply == char.Parse("Y") || reply == char.Parse("y")) { return true; }
            else return false;
        }

        
        #endregion
    }

}
