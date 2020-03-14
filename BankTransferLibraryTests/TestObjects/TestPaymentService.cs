using BankTransferLibrary.Data;
using BankTransferLibrary.Services;
using BankTransferLibrary.Types;

namespace BankTransferLibraryTests.TestObjects
{
    internal class TestPaymentService : PaymentService
    {
        public TestPaymentService()
        {
            DataStore = new TestDataStore();
        }
        public TestDataStore DataStore { get; private set; }
        public Account Account
        {
            get
            {
                return DataStore.Account;
            }
        }
        public override DataStore GetDataStore()
        {
            return DataStore;
        }
    }
}
