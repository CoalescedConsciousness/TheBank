using System;
using System.Collections.Generic;

namespace Bank1
{

    public class Account
    {
        public string name { get; set; }
        public decimal balance { get; set; }

        /// <summary>
        /// [C]RUD Overload with name and balance.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="balance"></param>
        public Account(string name, decimal balance)
        {
            this.name = name;
            this.balance = balance;
        }

        /// <summary>
        /// Standard [C]RUD creation, only needing name.
        /// </summary>
        /// <param name="name"></param>
        public Account(string name)
        {
            this.name = name;
        }
        
    }
}