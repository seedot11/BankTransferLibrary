using System;

namespace BankTransferLibrary.Types
{
    [Flags]
    public enum PaymentScheme
    {
        FasterPayments = 1 << 0,
        Bacs = 1 << 1,
        Chaps = 1 << 2
    }
}
