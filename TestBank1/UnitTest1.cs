using Microsoft.VisualStudio.TestTools.UnitTesting;
using Account;

namespace TestBank1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AreThereAccounts()
        {
            // Arrange:
            string name = "Test";
            decimal balance = 0;

            // Act:
            Account.Account testAccount = new Account.MasterCardAccount(name, balance);

            // Asset
            Assert.IsInstanceOfType(testAccount, typeof(Account.Account));
        }
    }
}