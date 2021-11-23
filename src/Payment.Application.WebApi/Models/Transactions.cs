using Payment.Domain.Enumerators;

using System;
using System.Collections.Generic;

namespace Payment.Application.WebApi.Models
{
    public class Transactions
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime? NotApprovedDate { get; set; }
        public AntecipationStatus? AntecipationStatus { get; set; }
        public bool BankConfirmation { get; set; }
        public decimal Amount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal FlatRate { get; set; }
        public int InstallmentsNumber { get; set; }
        public string FourLastCardNumber { get; set; }
        public int? AntecipationId { get; set; }
        public IEnumerable<Installments> Installments { get; set; }
        public Antecipations? Antecipation { get; set; }
    }
}