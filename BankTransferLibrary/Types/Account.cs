﻿namespace BankTransferLibrary.Types
{
    public class Account
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public AccountStatus Status { get; set; }
        public PaymentScheme AllowedPaymentSchemes { get; set; }
    }
}
