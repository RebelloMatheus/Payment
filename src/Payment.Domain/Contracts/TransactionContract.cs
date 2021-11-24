using Payment.Domain.Enumerators;
using System;
using System.Collections.Generic;

namespace Payment.Domain.Contracts
{
    public class TransactionContract
    {
        public int Id { get; }
        public DateTime TransactionDate { get; }
        public DateTime? ApprovedDate { get; }
        public DateTime? NotApprovedDate { get; }
        public AntecipationStatus? AntecipationStatus { get; }
        public bool BankConfirmation { get; }
        public decimal Amount { get; }
        public decimal NetAmount { get; }
        public decimal FlatRate { get; }
        public int InstallmentsNumber { get; }
        public string FourLastCardNumber { get; }
        public int? AntecipationId { get; }
        public virtual IEnumerable<InstallmentsContract> Installments { get; }
        public AntecipationsContract? Antecipation { get; }

        public TransactionContract(
            int id,
            DateTime transactionDate,
            DateTime? approvedDate,
            DateTime? notApprovedDate,
            AntecipationStatus? antecipationStatus,
            bool bankConfirmation,
            decimal amount,
            decimal netAmount,
            decimal flatRate,
            int installmentsNumber,
            string fourLastCardNumber,
            int? antecipationId,
            IEnumerable<InstallmentsContract> installments,
            AntecipationsContract antecipation)
        {
            Id = id;
            TransactionDate = transactionDate;
            ApprovedDate = approvedDate;
            NotApprovedDate = notApprovedDate;
            AntecipationStatus = antecipationStatus;
            BankConfirmation = bankConfirmation;
            Amount = amount;
            NetAmount = netAmount;
            FlatRate = flatRate;
            InstallmentsNumber = installmentsNumber;
            FourLastCardNumber = fourLastCardNumber;
            AntecipationId = antecipationId;
            Installments = installments;
            Antecipation = antecipation;
        }
    }
}