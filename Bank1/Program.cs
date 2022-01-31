using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

static class Globals
{
    public static int AccID;
    public static int ActiveAccID;
}

namespace Bank1
{
    using Account = Account.Account;

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
            FileRepository fileRepo = new FileRepository();
            Console.WriteLine($"******** Velkommen til {firstBank.bankName} - Bank 1 ********* \n");
            Account acc = firstBank.CreateAccount();
            fileRepo.AddAccount(acc);
            Console.WriteLine("\n");

            Menu(firstBank.accountList[Globals.ActiveAccID], firstBank);
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
            char userSelect;
            
            while (runMenu)
            {
                MsgBuffer();
                Console.WriteLine($"Logged in as {bank.accountList[Globals.ActiveAccID].Name}, ID: {bank.accountList[Globals.ActiveAccID].AccountNumber}.");
                Console.WriteLine("\n");
                Console.WriteLine("[A]: Account Management");
                Console.WriteLine("[B]: Bank Management");
                Console.WriteLine("---");
                Console.WriteLine("[X]: Exit");
                Console.WriteLine("\n");
                /// User selection:
                userSelect = Console.ReadKey().KeyChar;
                
                // Bit messy, but select function calls the function and returns a bool to justify whether the operation should continue or not.
                runMenu = Select(userSelect, bank.accountList[Globals.ActiveAccID], bank);
                
            }
        }

        /// <summary>
        /// Calls a function dependent on the letter entered.
        /// </summary>
        /// <param name="userSelect"></param>
        /// <param name="workingAccount"></param>
        /// <param name="bank"></param>
        /// <returns></returns>
        static bool Select(char userSelect, Account workingAccount, Bank bank)
        {
            Console.Clear();
            // A
            if (QuickResponse("A", userSelect))
            { AccMenu(bank.accountList[Globals.ActiveAccID], bank); return true; }

            // B
            if (QuickResponse("B", userSelect))
            { BankMenu(bank.accountList[Globals.ActiveAccID], bank); return true; }

            else
            {
                bank.fileRepository.SaveBank();
                return false;
            }
        }
        #endregion

        #region Account-centric Menu
        static void AccMenu(Account workingAccount, Bank bank)
        {
            bool runMenu = true;
            char userSelect;

            while (runMenu)
            {
                Console.Clear();
                Console.WriteLine("[[ Account Management ]]");
                Console.WriteLine("[C]: Change Account");
                Console.WriteLine("[D]: Deposit");
                Console.WriteLine("[W]: Withdraw");
                Console.WriteLine("[B]: View Balance");
                Console.WriteLine("---");
                Console.WriteLine("[X]: Return");
                Console.WriteLine("\n");

                userSelect = Console.ReadKey().KeyChar;
                
                // Bit messy, but select function calls the function and returns a bool to justify whether the operation should continue or not.
                runMenu = AccSelect(userSelect, workingAccount, bank);
             
            }
        }
        static bool AccSelect(char userSelect, Account workingAccount, Bank bank)
        {
            Console.Clear();
            // A
            if (QuickResponse("C", userSelect))
            { Globals.ActiveAccID = bank.ChangeAccount(bank, bank.accountList[Globals.ActiveAccID]); MsgBuffer(); return true; }

            // D
            if (QuickResponse("D", userSelect))
            { bank.Deposit(bank.accountList[Globals.ActiveAccID]); MsgBuffer(); return true; }

            // W
            if (QuickResponse("W", userSelect))
            { 
                try 
                {
                    bank.Withdraw(bank.accountList[Globals.ActiveAccID]);
                }
                catch(OverdraftException e)
                {
                    Console.WriteLine(e);
                    Console.ReadKey();
                }
                return true; 
            }

            // B
            if (QuickResponse("B", userSelect))
            { bank.Balance(bank.accountList[Globals.ActiveAccID]); MsgBuffer(); return true; }

            else
            {

                return false;
            }
        }
        #endregion

        #region Bank-centric Menu
        static void BankMenu(Account workingAccount, Bank bank)
        {
            bool runMenu = true;
            char userSelect;

            while (runMenu)
            {
                Console.Clear();
                Console.WriteLine("[[ Bank Management ]]");
                Console.WriteLine("[A]: Create Account");
                Console.WriteLine("[B]: View Bank Balance");
                Console.WriteLine("[C]: Charge Interest");
                Console.WriteLine("[G]: Show Log");
                Console.WriteLine("---");
                Console.WriteLine("[X]: Return");
                Console.WriteLine("\n");

                userSelect = Console.ReadKey().KeyChar;

                // Bit messy, but select function calls the function and returns a bool to justify whether the operation should continue or not.
                runMenu = BankSelect(userSelect, workingAccount, bank);
          
            }
        }
        static bool BankSelect(char userSelect, Account workingAccount, Bank bank)
        {
            Console.Clear();
            // A
            if (QuickResponse("A", userSelect))
            {
                Console.WriteLine("[[ Select Account Type ]]");
                Console.WriteLine("check == Checking account");
                Console.WriteLine("save == Savings account");
                Console.WriteLine("cons or nothing == MasterCard Account");
                string userInput = Console.ReadLine();
                
                bank.fileRepository.AddAccount(bank.CreateAccount(userInput));

                MsgBuffer();
                return true; 
            }

            // D
            if (QuickResponse("B", userSelect))
            { bank.BankBalance(bank); MsgBuffer(); return true; }

            // C
            if (QuickResponse("C", userSelect))
            { bank.ChargeInterest(bank); MsgBuffer(); return true; }

            if (QuickResponse("G", userSelect))
            { Console.WriteLine(FileLogger.ReadFromLog()); MsgBuffer(); return true; } 

            else
            { return false; }
        }
        #endregion

        #region Helper methods
        /// <summary>
        /// Method that simply translates char keypresses to text mappings.
        /// </summary>
        /// <param name="letter"></param>
        /// <param name="reply"></param>
        /// <returns></returns>
        static bool QuickResponse(string letter, char reply)
        {
            return (reply == char.Parse(letter.ToUpper()) || reply == char.Parse(letter.ToLower())) ? true : false;
        }

        static void MsgBuffer()
        {
            Console.WriteLine("\n\nPress any key to continue.");
            Console.ReadKey();
            Console.Clear();
        }
        #endregion
    }

}