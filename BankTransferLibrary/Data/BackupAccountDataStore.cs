using BankTransferLibrary.Types;

namespace BankTransferLibrary.Data
{
    public class BackupAccountDataStore : DataStore
    {
        public override Account GetAccount(string accountNumber)
        {
            // Access backup data base to retrieve account, code removed for brevity 
            return new Account();
        }

        public override void UpdateAccount(Account account)
        {
            // Update account in backup database, code removed for brevity
        }
    }
}
