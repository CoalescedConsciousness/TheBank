using System.Collections.Generic;

namespace IBank1
{
    using Account = Account.Account;
    public interface IFileRepository
    {
        
        int AddAccount(Account account);
        Account GetAccount(int id);
        List<Account> GetAllAccounts();
        List<Account> LoadBank();
        void SaveBank();
        void UpdateAccount(Account acc);
    }
}
