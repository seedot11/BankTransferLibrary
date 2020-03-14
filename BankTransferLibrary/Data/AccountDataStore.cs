using BankTransferLibrary.Types;

namespace BankTransferLibrary.Data
{
    public class AccountDataStore : DataStore
    {
        public override Account GetAccount(string accountNumber)
        {
            // Access database to retrieve account, code removed for brevity 
            return new Account();
        }

        public override void UpdateAccount(Account account)
        {
            // Update account in database, code removed for brevity
        }
    }
}
