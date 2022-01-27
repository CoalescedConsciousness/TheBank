using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class Globals
{
    public static int AccID;
}

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

        #region Primary Menu
        /// <summary>
        /// Menu function; simply there to give an overview of available user actions, and send the selection to processing in Select()
        /// </summary>
        /// <param name="workingAccount"></param>
        /// <param name="bank"></param>
        static void Menu(Account workingAccount, Bank bank)
        {
            bool runMenu = true;
            string userSelect;
            
            while (runMenu)
            {
                Console.WriteLine("\n");
                Console.WriteLine($"Logged in as {workingAccount.name}, ID: {workingAccount.accountNumber}.");
                Console.WriteLine("\n");
                Console.WriteLine("[A]: Account Management");
                Console.WriteLine("[B]: Bank Management");
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
            { AccMenu(workingAccount, bank); return true; }

            // B
            if (userSelect == "B" || userSelect == "b")
            { BankMenu(workingAccount, bank); return true; }

            else
            { return false; }
        }
        #endregion

        #region Account-centric Menu
        static void AccMenu(Account workingAccount, Bank bank)
        {
            bool runMenu = true;
            string userSelect;

            while (runMenu)
            {
                Console.Clear();
                Console.WriteLine("[[ Account Management ]]");
                Console.WriteLine("[C]: Change Account");
                Console.WriteLine("[D]: Deposit");
                Console.WriteLine("[W]: Withdraw");
                Console.WriteLine("[B]: View Balance");
                Console.WriteLine("---");
                Console.WriteLine("[X]: Exit");
                Console.WriteLine("\n");

                userSelect = Console.ReadLine();
                Console.WriteLine(userSelect);

                // Bit messy, but select function calls the function and returns a bool to justify whether the operation should continue or not.
                runMenu = AccSelect(userSelect, workingAccount, bank);
            }
        }
        static bool AccSelect(string userSelect, Account workingAccount, Bank bank)
        {
            // A
            if (userSelect == "C" || userSelect == "c")
            { bank.ChangeAccount(bank, workingAccount); return true; }

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
        #endregion

        #region Bank-centric Menu
        static void BankMenu(Account workingAccount, Bank bank)
        {
            bool runMenu = true;
            string userSelect;

            while (runMenu)
            {
                Console.Clear();
                Console.WriteLine("[[ Account Management ]]");
                Console.WriteLine("[A]: Create Account");
                Console.WriteLine("[B]: View Balance");
                Console.WriteLine("[C]: Charge Interest");
                Console.WriteLine("---");
                Console.WriteLine("[X]: Return");
                Console.WriteLine("\n");

                userSelect = Console.ReadLine();
                Console.WriteLine(userSelect);

                // Bit messy, but select function calls the function and returns a bool to justify whether the operation should continue or not.
                runMenu = BankSelect(userSelect, workingAccount, bank);
            }
        }
        static bool BankSelect(string userSelect, Account workingAccount, Bank bank)
        {
            // A
            if (userSelect == "A" || userSelect == "a")
            {
                Console.Clear();
                Console.WriteLine("[[ Select Account Type ]]");
                Console.WriteLine("check == Checking account");
                Console.WriteLine("save == Savings account");
                Console.WriteLine("cons or nothing == MasterCard Account");
                string userInput = Console.ReadLine();
                
                bank.CreateAccount(userInput); return true; }

            // D
            if (userSelect == "D" || userSelect == "d")
            { bank.BankBalance(bank); return true; }

            // C
            if (userSelect == "C" || userSelect == "c")
            { bank.ChargeInterest(bank); return true; }
            else
            { return false; }
        }
        #endregion
    }

}