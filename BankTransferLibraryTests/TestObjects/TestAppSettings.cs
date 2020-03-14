using BankTransferLibrary.Data;
using BankTransferLibrary.Types;

namespace BankTransferLibraryTests.TestObjects
{
    internal class TestAppSettings : AppSettings
    {
        public static void SetDataStoreType(DataStoreType dataStoreType) => DataStoreType = dataStoreType;
    }
}
