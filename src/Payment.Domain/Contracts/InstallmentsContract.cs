using Payment.Domain.Models;
using System;

namespace Payment.Domain.Contracts
{
    public class InstallmentsContract
    {
        public int Id { get; }
        public int PaymentId { get; }
        public decimal Amount { get; }
        public decimal NetAmount { get; }
        public int Number { get; }
        public decimal? AntecipationValue { get; }
        public DateTime ExpectedPaymentDate { get; }
        public DateTime? AntecipationDate { get; }
        public Transactions Payment { get; }

        public InstallmentsContract(
            int id,
            int paymentId,
            decimal amount,
            decimal netAmount,
            int number,
            decimal? antecipationValue,
            DateTime expectedPaymentDate,
            DateTime? antecipationDate)
        {
            Id = id;
            PaymentId = paymentId;
            Amount = amount;
            NetAmount = netAmount;
            Number = number;
            AntecipationValue = antecipationValue;
            ExpectedPaymentDate = expectedPaymentDate;
            AntecipationDate = antecipationDate;
        }
    }
}