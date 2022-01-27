using System;
using System.Collections.Generic;

namespace Bank1
{
    public abstract class Account
    {
        // Fields:
        public abstract string name { get; set; }
        public abstract decimal balance { get; set; }
        public abstract int accountNumber { get; }

        internal abstract DateTime interestDate { get; set; }
        internal abstract bool interestApplied { get; set; }
        // Methods
        public abstract void ChargeInterest();
    }
    public class CheckingAccount : Account
    {
        public override string name { get; set; }
        public override decimal balance { get; set; }

        public override int accountNumber { get; }

        internal override DateTime interestDate { get; set; }
        internal override bool interestApplied { get; set; }

        #region CRUD
        /// <summary>
        /// [C]RUD Overload with name and balance.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="balance"></param>
        public CheckingAccount(string name, decimal balance, ref int id)
        {
            this.name = name;
            this.balance = balance;
            accountNumber = id;
            id++;
        }

        /// <summary>
        /// Standard [C]RUD creation, only needing name.
        /// </summary>
        /// <param name="name"></param>
        public CheckingAccount(string name, ref int id)
        {
            this.name = name;
            accountNumber = id;
            id++;
        }
        #endregion

        public override void ChargeInterest()
        {
            if (interestDate > DateTime.Now.AddYears(-1))
            {
                if (!interestApplied)
                {
                    balance = (decimal)(float.Parse(s: balance.ToString()) * 1.005);
                    interestApplied = true;
                }
            }
        }
    }
    public class SavingsAccount : Account
    {
        public override string name { get; set; }
        public override decimal balance { get; set; }

        public override int accountNumber { get; }

        internal override DateTime interestDate { get; set; }
        internal override bool interestApplied { get; set; }
        #region CRUD
        /// <summary>
        /// [C]RUD Overload with name and balance.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="balance"></param>
        public SavingsAccount(string name, decimal balance, ref int id)
        {
            this.name = name;
            this.balance = balance;
            accountNumber = id;
            id++;
        }

        /// <summary>
        /// Standard [C]RUD creation, only needing name.
        /// </summary>
        /// <param name="name"></param>
        public SavingsAccount(string name, ref int id)
        {
            this.name = name;
            accountNumber = id;
            id++;
        }
        #endregion

        /// <summary>
        /// +1% if below or exactly 50,000 
        /// +2% if above 50,000 and below or exactly 100,000
        /// +3% if above 100,000
        /// </summary>
        public override void ChargeInterest()
        {
            if (interestDate > DateTime.Now.AddYears(-1))
            {
                if (!interestApplied)
                {
                    if (balance <= 50000)
                    {
                        balance = (decimal)(float.Parse(s: balance.ToString()) * 1.01);
                    }
                    if (50000 < balance && balance <= 100000)
                    {
                        balance = (decimal)(float.Parse(s: balance.ToString()) * 1.02);
                    }
                    if (balance > 100000)
                    {
                        balance = (decimal)(float.Parse(s: balance.ToString()) * 1.03);
                    }
                    interestApplied = true;
                }
            }
        }
    }
    public class MasterCardAccount : Account
    {
        public override string name { get; set; }
        public override decimal balance { get; set; }

        public override int accountNumber { get; }

        internal override DateTime interestDate { get; set; }
        internal override bool interestApplied { get; set; }

        #region CRUD
        /// <summary>
        /// [C]RUD Overload with name and balance.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="balance"></param>
        public MasterCardAccount(string name, decimal balance, ref int id)
        {
            this.name = name;
            this.balance = balance;
            accountNumber = id;
            id++;
        }

        /// <summary>
        /// Standard [C]RUD creation, only needing name.
        /// </summary>
        /// <param name="name"></param>
        public MasterCardAccount(string name, ref int id)
        {
            this.name = name;
            accountNumber = id;
            id++;
        }
        #endregion

        /// <summary>
        /// +0.1% if Positive
        /// -20% if Negative balance
        /// </summary>
        public override void ChargeInterest()
        {
            if (interestDate > DateTime.Now.AddYears(-1))
            {
                if (!interestApplied)
                {
                    if (balance >= 0)
                    {
                        balance = (decimal)(float.Parse(s: balance.ToString()) * 1.001);
                    }
                    else
                    {
                        balance = (decimal)(float.Parse(s: balance.ToString()) * 1.2);
                    }
                }
            }
        }
    }
}