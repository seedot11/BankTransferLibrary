using BankTransferLibrary.Data;
using BankTransferLibrary.Types;

namespace BankTransferLibraryTests.TestObjects
{
    internal class TestDataStore : DataStore
    {
        public TestDataStore()
        {
            Account = new Account();
        }
        public Account Account { get; private set; }
        public override Account GetAccount(string accountNumber)
        {
            Account.AccountNumber = accountNumber;
            return Account;
        }
    }
}
