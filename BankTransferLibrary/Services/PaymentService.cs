using BankTransferLibrary.Data;
using BankTransferLibrary.Types;

namespace BankTransferLibrary.Services
{
    /// <summary>
    /// A service which can make a payment based on a request.
    /// </summary>
    public class PaymentService
    {
        /// <summary>
        /// Feel free to change whatever you like during your refactoring challenge,
        /// BUT, only 1 rule, you CANNOT change this method signature.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var datastore = GetDataStore();
            var account = datastore.GetAccount(request.DebtorAccountNumber);

            var result = new MakePaymentResult
            {
                Success = IsPaymentAllowed(account, request)
            };

            if (result.Success)
            {
                account.Balance -= request.Amount;
                datastore.UpdateAccount(account);
            }

            return result;
        }

        /// <summary>
        /// Returns the appropriate data store as decided by app setting configuration.
        /// </summary>
        public virtual DataStore GetDataStore()
        {
            if (AppSettings.DataStoreType == DataStoreType.BackupAccount)
            {
                return new BackupAccountDataStore();
            }
            else
            {
                return new AccountDataStore();
            }
        }

        /// <summary>
        /// Depending on the parameters from the account and the request, either accepts or rejects the payment.
        /// </summary>
        private bool IsPaymentAllowed(Account account, MakePaymentRequest request)
        {
            if (account == null || !account.AllowedPaymentSchemes.HasFlag(request.PaymentScheme))
            {
                return false;
            }

            return request.PaymentScheme switch
            {
                PaymentScheme.Bacs => true,
                PaymentScheme.FasterPayments => account.Balance >= request.Amount,
                PaymentScheme.Chaps => account.Status == AccountStatus.Live,
                _ => false,
            };
        }
    }
}
