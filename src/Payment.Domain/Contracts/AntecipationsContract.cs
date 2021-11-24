using Payment.Domain.Enumerators;
using Payment.Domain.Models;
using System;
using System.Collections.Generic;

namespace Payment.Domain.Contracts
{
    public class AntecipationsContract
    {
        public int Id { get; }
        public DateTime RequestDate { get; }
        public DateTime? AnalysisStartDate { get; }
        public DateTime? AnalysisEndDate { get; }
        public AnalysisResult? AnalysisResult { get; }
        public decimal RequestedAmount { get; }
        public decimal? GrantedAmount { get; }
        public IEnumerable<Transactions> RequestedTransactions { get; }

        public AntecipationsContract(
            int id,
            DateTime requestDate,
            DateTime? analysisStartDate,
            DateTime? analysisEndDate,
            AnalysisResult? analysisResult,
            decimal requestedAmount,
            decimal? grantedAmount)
        {
            Id = id;
            RequestDate = requestDate;
            AnalysisStartDate = analysisStartDate;
            AnalysisEndDate = analysisEndDate;
            AnalysisResult = analysisResult;
            RequestedAmount = requestedAmount;
            GrantedAmount = grantedAmount;
        }
    } 
}