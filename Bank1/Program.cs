using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Bank1
{
    class Program
    { 
        static void Main()
        {
            Run();
        }

        /// <summary>
        /// Run is used to execute the majority of the example code. It is called from Main. This is for the sake of extendability (should it have been needed)
        /// </summary>
        static void Run()
        {
            Bank firstBank = new Bank();
            Console.WriteLine($"******** Velkommen til {firstBank.bankName} - Bank 1 ********* \n");
            Account workingAccount = firstBank.CreateAccount();
            Console.WriteLine("\n");
            
            Menu(workingAccount, firstBank);
        }

        static void Menu(Account workingAccount, Bank bank)
        {
            bool runMenu = true;
            string userSelect;
            
            while (runMenu)
            {
                Console.WriteLine("\n");
                Console.WriteLine($"Logged in as {workingAccount.name}, ID: {workingAccount.accountNumber}.");
                Console.WriteLine("\n");
                Console.WriteLine("[A]: Change Account");
                Console.WriteLine("[D]: Deposit");
                Console.WriteLine("[W]: Withdraw");
                Console.WriteLine("[B]: View Balance");
                Console.WriteLine("---");
                Console.WriteLine("[X]: Exit");
                Console.WriteLine("\n");
                /// User selection:
                userSelect = Console.ReadLine();
                Console.WriteLine(userSelect);

                // Bit messy, but select function calls the function and returns a bool to justify whether the operation should continue or not.
                runMenu = Select(userSelect, workingAccount, bank);
                
            }
        }

        /// <summary>
        /// Calls a function dependent on the letter entered.
        /// </summary>
        /// <param name="userSelect"></param>
        /// <param name="workingAccount"></param>
        /// <param name="bank"></param>
        /// <returns></returns>
        static bool Select(string userSelect, Account workingAccount, Bank bank)
        {
            // A
            if (userSelect == "A" || userSelect == "a")
            { ChangeAccount(bank, workingAccount); return true; }

            // D
            if (userSelect == "D" || userSelect == "d")
            { bank.Deposit(workingAccount); return true; }

            // W
            if (userSelect == "W" || userSelect == "w")
            { bank.Withdraw(workingAccount); return true; }

            // B
            if (userSelect == "B" || userSelect == "b")
            { bank.Balance(workingAccount); return true; }

            else
            { return false; }
        }

        static Account ChangeAccount(Bank bank, Account workingAccount)
        {
            Console.WriteLine(bank.accountList);
            Console.WriteLine($"Available Accounts:\n");
            foreach (Account acc in bank.accountList)

                Console.Write($" {acc.AccountNumber}, {acc.name}");
            return workingAccount;
        }
    }

}