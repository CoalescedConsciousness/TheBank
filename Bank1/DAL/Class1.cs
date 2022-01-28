using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank1
{
    public class FileRepository : IFileRepository
    {
        public const string fileName = "data.txt";

        public List<Account> accountList { get; set; }

        public static int accID;

        public FileRepository()
        {
            List<Account> accList = new List<Account>();
        }

        public int AddAccount(Account account)
        {
            accountList.Add(account);
            accID++;
            return account.AccountNumber;
        }

        public Account GetAccount(int id)
        {
            throw new NotImplementedException();
        }

        public List<Account> GetAllAccounts()
        {
            throw new NotImplementedException();
        }

        public List<Account> LoadBank()
        {
            throw new NotImplementedException();
        }

        public void SaveBank()
        {
            if (!File.Exists(fileName))
            {
                File.Create(fileName);
            }

            string[] data = default;
            foreach (Account x in accountList)
            {
                List<string> accData = new List<string>() { x.AccountNumber.ToString(), x.Name, x.Balance.ToString(), x.InterestApplied.ToString(), x.InterestDate.ToString() };
                string result = String.Join(";", accData.ToArray());
                data.Append(result);
            }
            File.AppendAllLinesAsync(fileName, data);
        }

        public void UpdateAccount(Account acc)
        {
            throw new NotImplementedException();
        }
    }
}
