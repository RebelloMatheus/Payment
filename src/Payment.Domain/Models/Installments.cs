using Payment.Domain.Models.Base;
using System;

namespace Payment.Domain.Models
{
    public class Installments : EntityBase
    {
        public int PaymentId { get; private set; }
        public decimal Amount { get; private set; }
        public decimal NetAmount { get; private set; }
        public int Number { get; private set; }
        public decimal? AntecipationValue { get; private set; }
        public DateTime ExpectedPaymentDate { get; private set; }
        public DateTime? AntecipationDate { get; private set; }
        public virtual Transactions Payment { get; private set; }

        protected Installments()
        {
        }

        public Installments(
            int id,
            int paymentId,
            decimal amount,
            decimal netAmount,
            int number,
            decimal? antecipationValue,
            DateTime expectedPaymentDate,
            DateTime? antecipationDate,
            Transactions payment)
        {
            Id = id;
            PaymentId = paymentId;
            Amount = amount;
            NetAmount = netAmount;
            Number = number;
            AntecipationValue = antecipationValue;
            ExpectedPaymentDate = expectedPaymentDate;
            AntecipationDate = antecipationDate;
            Payment = payment;
        }

        public Installments(
            decimal amount,
            DateTime expectedPaymentDate,
            decimal netAmount,
            int number)
        {
            Amount = amount;
            ExpectedPaymentDate = expectedPaymentDate;
            NetAmount = netAmount;
            Number = number;
        }

        public void UpdateAntecipationDate(DateTime? antecipationDate)
        {
            AntecipationDate = antecipationDate;
        }

        public void UpdateAntecipationValue(decimal? antecipationValue)
        {
            AntecipationValue = antecipationValue;
        }
    }
}