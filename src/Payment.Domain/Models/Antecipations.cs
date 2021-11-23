using Payment.Domain.Enumerators;
using Payment.Domain.Models.Base;
using System;
using System.Collections.Generic;

namespace Payment.Domain.Models
{
    public class Antecipations : EntityBase
    {
        public DateTime RequestDate { get; private set; }
        public DateTime? AnalysisStartDate { get; private set; }
        public DateTime? AnalysisEndDate { get; private set; }
        public AnalysisResult? AnalysisResult { get; private set; }
        public decimal RequestedAmount { get; private set; }
        public decimal? GrantedAmount { get; private set; }
        public virtual IEnumerable<Transactions> RequestedTransactions { get; private set; }

        protected Antecipations()
        {
        }

        public Antecipations(
            int id,
            DateTime requestDate,
            DateTime? analysisStartDate,
            DateTime? analysisEndDate,
            AnalysisResult? analysisResult,
            decimal requestedAmount,
            decimal? grantedAmount,
            IEnumerable<Transactions> requestedTransactions)
        {
            Id = id;
            RequestDate = requestDate;
            AnalysisStartDate = analysisStartDate;
            AnalysisEndDate = analysisEndDate;
            AnalysisResult = analysisResult;
            RequestedAmount = requestedAmount;
            GrantedAmount = grantedAmount;
            RequestedTransactions = requestedTransactions;
        }

        public void UpdateGrantedAmount(decimal? grantedAmount)
        {
            GrantedAmount = grantedAmount;
        }

        public Antecipations(DateTime requestDate, decimal requestedAmount, IEnumerable<Transactions> requestedTransactions)
        {
            RequestDate = requestDate;
            RequestedAmount = requestedAmount;
            RequestedTransactions = requestedTransactions;
        }

        public void UpdateAnalysisEndDate(DateTime? analysisEndDate)
        {
            AnalysisEndDate = analysisEndDate;
        }

        public void UpdateAnalysisResult(AnalysisResult? analysisResult)
        {
            AnalysisResult = analysisResult;
        }

        public void UpdateAnalysisStartDate(DateTime? analysisStartDate)
        {
            AnalysisStartDate = analysisStartDate;
        }
    }
}