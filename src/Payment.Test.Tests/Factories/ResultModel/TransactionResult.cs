using System;

namespace Payment.Test.Tests.Factories.ResultModel
{
    public class TransactionResult
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool BankConfirmation { get; set; }
        public decimal Amount { get; set; }
    }
}