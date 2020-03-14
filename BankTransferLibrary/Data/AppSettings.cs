using BankTransferLibrary.Types;
using System.Configuration;

namespace BankTransferLibrary.Data
{
    public class AppSettings
    {
        private static DataStoreType? dataStoreType;

        /// <summary>
        /// An app setting representing which DataStore to use to retrieve accounts.
        /// </summary>
        public static DataStoreType DataStoreType
        {
            get 
            { 
                if (dataStoreType == null)
                {
                    dataStoreType = (ConfigurationManager.AppSettings["Backup"]) switch
                    {
                        "Backup" => DataStoreType.BackupAccount,
                        _ => DataStoreType.Account,
                    };
                }
                return dataStoreType.Value;
            }
            protected set 
            {
                dataStoreType = value;
            }
        }
    }
}
