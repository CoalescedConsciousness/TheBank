using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class OverdraftException : Exception
{
    public OverdraftException()
    {
    }

    public OverdraftException(string accName)
        : base($"Account balance for {accName} is negative")
    {
    }
    public OverdraftException(string accName, int accID)
        : base($"Balance for the account {accID}, belonging to {accName}, is negative")
    {
    }
}