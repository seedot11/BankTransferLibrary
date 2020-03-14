using BankTransferLibrary.Types;

namespace BankTransferLibrary.Data
{
    /// <summary>
    /// A representation of a DataStore to implement.
    /// </summary>
    public abstract class DataStore
    {
        /// <summary>
        /// Returns an account using the specified account number.
        /// </summary>
        /// <param name="accountNumber">A number representing a unique account identifier.</param>
        public virtual Account GetAccount(string accountNumber)
        {
            return new Account();
        }

        /// <summary>
        /// Updates an account from the DataStore using the passed Account object.
        /// </summary>
        public virtual void UpdateAccount(Account account)
        {

        }
    }
}
