using System;
using System.Collections.Generic;

namespace Bank1
{
    public abstract class Account
    {
        // Fields:
        public abstract string Name { get; set; }
        public abstract decimal Balance { get; set; }
        public abstract int AccountNumber { get; }
     
        

        internal abstract DateTime InterestDate { get; set; }
        internal abstract bool InterestApplied { get; set; }
        // Methods
        public abstract void ChargeInterest();
    }
    public class CheckingAccount : Account
    {
        decimal _balance;

        public override string Name { get; set; }
        
        public override decimal Balance
        {
            get => _balance;
            set
            {
                if (value < 0)
                {
                    throw new OverdraftException(Name, AccountNumber);
                }
                _balance = value;
            }
        }

        public override int AccountNumber { get; }

        internal override DateTime InterestDate { get; set; }
        internal override bool InterestApplied { get; set; }

        #region CRUD
        /// <summary>
        /// [C]RUD Overload with name and balance.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="balance"></param>
        public CheckingAccount(string name, decimal balance)
        {
            this.Name = name;
            this.Balance = balance;
            AccountNumber = Globals.AccID;
            Globals.AccID++;
        }

        /// <summary>
        /// Standard [C]RUD creation, only needing name.
        /// </summary>
        /// <param name="name"></param>
        public CheckingAccount(string name)
        {
            this.Name = name;
            AccountNumber = Globals.AccID;
            Globals.AccID++;
        }
        #endregion

        public override void ChargeInterest()
        {
            if (InterestDate > DateTime.Now.AddYears(-1))
            {
                if (!InterestApplied)
                {
                    Balance = (decimal)(float.Parse(s: Balance.ToString()) * 1.005);
                    InterestApplied = true;
                }
            }
        }
    }
    public class SavingsAccount : Account
    {
        decimal _balance;
        public override string Name { get; set; }
        public override decimal Balance
        {
            get => _balance;
            set
            {
                if (value < 0)
                {
                    throw new OverdraftException(Name, AccountNumber);
                }
            }
        }

        public override int AccountNumber { get; }

        internal override DateTime InterestDate { get; set; }
        internal override bool InterestApplied { get; set; }
        #region CRUD
        /// <summary>
        /// [C]RUD Overload with name and balance.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="balance"></param>
        public SavingsAccount(string name, decimal balance)
        {
            this.Name = name;
            this.Balance = balance;
            AccountNumber = Globals.AccID;
            Globals.AccID++;
        }

        /// <summary>
        /// Standard [C]RUD creation, only needing name.
        /// </summary>
        /// <param name="name"></param>
        public SavingsAccount(string name)
        {
            this.Name = name;
            AccountNumber = Globals.AccID;
            Globals.AccID++;
        }
        #endregion

        /// <summary>
        /// +1% if below or exactly 50,000 
        /// +2% if above 50,000 and below or exactly 100,000
        /// +3% if above 100,000
        /// </summary>
        public override void ChargeInterest()
        {
            if (InterestDate > DateTime.Now.AddYears(-1))
            {
                if (!InterestApplied)
                {
                    if (Balance <= 50000)
                    {
                        Balance = (decimal)(float.Parse(s: Balance.ToString()) * 1.01);
                    }
                    if (50000 < Balance && Balance <= 100000)
                    {
                        Balance = (decimal)(float.Parse(s: Balance.ToString()) * 1.02);
                    }
                    if (Balance > 100000)
                    {
                        Balance = (decimal)(float.Parse(s: Balance.ToString()) * 1.03);
                    }
                    InterestApplied = true;
                }
            }
        }
    }
    public class MasterCardAccount : Account
    {
        decimal _balance;
        public override string Name { get; set; }
        public override decimal Balance
        {
            get => _balance;
            set
            {
                if (value < 0)
                {
                    throw new OverdraftException(Name, AccountNumber);
                }
            }
        }

        public override int AccountNumber { get; }

        internal override DateTime InterestDate { get; set; }
        internal override bool InterestApplied { get; set; }

        #region CRUD
        /// <summary>
        /// [C]RUD Overload with name and balance.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="balance"></param>
        public MasterCardAccount(string name, decimal balance)
        {
            this.Name = name;
            this.Balance = balance;
            AccountNumber = Globals.AccID;
            Globals.AccID++;
        }

        /// <summary>
        /// Standard [C]RUD creation, only needing name.
        /// </summary>
        /// <param name="name"></param>
        public MasterCardAccount(string name)
        {
            this.Name = name;
            AccountNumber = Globals.AccID;
            Globals.AccID++;
        }
        #endregion

        /// <summary>
        /// +0.1% if Positive
        /// -20% if Negative balance
        /// </summary>
        public override void ChargeInterest()
        {
            if (InterestDate > DateTime.Now.AddYears(-1))
            {
                if (!InterestApplied)
                {
                    if (Balance >= 0)
                    {
                        Balance = (decimal)(float.Parse(s: Balance.ToString()) * 1.001);
                    }
                    else
                    {
                        Balance = (decimal)(float.Parse(s: Balance.ToString()) * 1.2);
                    }
                }
            }
        }
    }
}