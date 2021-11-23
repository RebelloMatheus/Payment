using Payment.Domain.Enumerators;
using Payment.Domain.Models.Base;
using System;
using System.Collections.Generic;

namespace Payment.Domain.Models
{
    public class Transactions : EntityBase
    {
        public DateTime TransactionDate { get; private set; }
        public DateTime? ApprovedDate { get; private set; }
        public DateTime? NotApprovedDate { get; private set; }
        public AntecipationStatus? AntecipationStatus { get; private set; }
        public bool BankConfirmation { get; private set; }
        public decimal Amount { get; private set; }
        public decimal NetAmount { get; private set; }
        public decimal FlatRate { get; private set; }
        public int InstallmentsNumber { get; private set; }
        public string FourLastCardNumber { get; private set; }
        public int? AntecipationId { get; private set; }
        public virtual IEnumerable<Installments> Installments { get; private set; }
        public Antecipations? Antecipation { get; private set; }

        protected Transactions()
        {
        }

        public Transactions(
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
            IEnumerable<Installments> installments,
            Antecipations? antecipation)
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

        public Transactions(
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
            IList<Installments> installments,
            Antecipations? antecipation)
        {
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

        public Transactions(
            DateTime transactionDate,
            bool bankConfirmation,
            decimal amount,
            decimal netAmount,
            decimal flatRate,
            int installmentsNumber,
            string fourLastCardNumber)
        {
            TransactionDate = transactionDate;
            BankConfirmation = bankConfirmation;
            Amount = amount;
            NetAmount = netAmount;
            FlatRate = flatRate;
            InstallmentsNumber = installmentsNumber;
            FourLastCardNumber = fourLastCardNumber;
        }

        public void UpdateApprovedDate(DateTime approvedDate)
        {
            ApprovedDate = approvedDate;
        }

        public void UpdateNotApprovedDate(DateTime? notApprovedDate)
        {
            NotApprovedDate = notApprovedDate;
        }

        public void UpdateInstallments(IEnumerable<Installments> installments)
        {
            Installments = installments;
        }

        public void UpdateAntecipationStatus(AntecipationStatus? antecipationStatus)
        {
            AntecipationStatus = antecipationStatus;
        }
    }
}