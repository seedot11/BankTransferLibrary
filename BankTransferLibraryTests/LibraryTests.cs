using BankTransferLibrary.Data;
using BankTransferLibrary.Services;
using BankTransferLibrary.Types;
using BankTransferLibraryTests.TestObjects;
using NUnit.Framework;
using System;

namespace BankTransferLibraryTests
{
    [TestFixture]
    public class LibraryTests
    {
        [TestCase(DataStoreType.BackupAccount, true)]
        [TestCase(DataStoreType.Account, false)]
        public void UseBackupWhenAppropriate(DataStoreType appSetting, bool expectedOutcome)
        {
            TestAppSettings.SetDataStoreType(appSetting);
            var payment = new PaymentService();
            Assert.AreEqual(expectedOutcome, payment.GetDataStore() is BackupAccountDataStore);
        }

        [TestCase(PaymentScheme.Bacs, PaymentScheme.Bacs, -10, AccountStatus.Disabled)]
        [TestCase(PaymentScheme.Chaps, PaymentScheme.Chaps, -10, AccountStatus.Live)]
        [TestCase(PaymentScheme.FasterPayments, PaymentScheme.FasterPayments, 50, AccountStatus.Disabled)]
        [TestCase(PaymentScheme.Bacs, PaymentScheme.Bacs | PaymentScheme.FasterPayments, 50, AccountStatus.InboundPaymentsOnly)]
        [TestCase(PaymentScheme.FasterPayments, PaymentScheme.Bacs | PaymentScheme.FasterPayments | PaymentScheme.Chaps, 50, AccountStatus.Live)]
        public void SuccessfulPayment(PaymentScheme paymentScheme, PaymentScheme allowedPaymentSchemes, decimal balance, AccountStatus accountStatus)
        {
            var payment = new TestPaymentService();
            payment.Account.AllowedPaymentSchemes = allowedPaymentSchemes;
            payment.Account.Balance = balance;
            payment.Account.Status = accountStatus;
            var request = CreateStandardRequest(paymentScheme);
            Assert.IsTrue(payment.MakePayment(request).Success);
        }

        [TestCase(PaymentScheme.Chaps, PaymentScheme.Bacs | PaymentScheme.FasterPayments, 50, AccountStatus.Live)]
        [TestCase(PaymentScheme.Bacs, PaymentScheme.Chaps | PaymentScheme.FasterPayments, 50, AccountStatus.Live)]
        [TestCase(PaymentScheme.Chaps, PaymentScheme.Chaps | PaymentScheme.FasterPayments, 50, AccountStatus.InboundPaymentsOnly)]
        [TestCase(PaymentScheme.Chaps, PaymentScheme.Chaps, 50, AccountStatus.Disabled)]
        [TestCase(PaymentScheme.FasterPayments, PaymentScheme.Chaps, 49, AccountStatus.Live)]
        public void FailedPayment(PaymentScheme paymentScheme, PaymentScheme allowedPaymentSchemes, decimal balance, AccountStatus accountStatus)
        {
            var payment = new TestPaymentService();
            payment.Account.AllowedPaymentSchemes = allowedPaymentSchemes;
            payment.Account.Balance = balance;
            payment.Account.Status = accountStatus;
            var request = CreateStandardRequest(paymentScheme);
            Assert.IsFalse(payment.MakePayment(request).Success);
        }

        [TestCase(10, 7, 3)] // Take 7 pound from 10, ending up with 3.
        [TestCase(20, 50, 20)] // I don't have enough money to take 50 from 20, so it should remain 20.
        public void TakesPaymentCorrectly(decimal startAmmount, decimal paymentAmount, decimal endAmmount)
        {
            var payment = new TestPaymentService();
            payment.Account.AllowedPaymentSchemes = PaymentScheme.Bacs | PaymentScheme.FasterPayments | PaymentScheme.Chaps;
            payment.Account.Balance = startAmmount;
            var request = CreateStandardRequest(PaymentScheme.FasterPayments);
            request.Amount = paymentAmount;
            payment.MakePayment(request);
            Assert.AreEqual(payment.Account.Balance, endAmmount);
        }

        /// <summary>
        /// Creates a payment request for 50 pounds to credit account 111111 from 222222 now,
        /// using the specified payment scheme.
        /// </summary>
        private MakePaymentRequest CreateStandardRequest(PaymentScheme paymentScheme)
        {
            return new MakePaymentRequest
            {
                Amount = 50.0M,
                CreditorAccountNumber = "11111111",
                DebtorAccountNumber = "22222222",
                PaymentDate = DateTime.Now,
                PaymentScheme = paymentScheme
            };
        }
    }
}
