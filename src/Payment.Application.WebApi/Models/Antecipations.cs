using Payment.Domain.Enumerators;

using System;
using System.Collections.Generic;

namespace Payment.Application.WebApi.Models
{
    public class Antecipations
    {
        public int Id { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? AnalysisStartDate { get; set; }
        public DateTime? AnalysisEndDate { get; set; }
        public AnalysisResult? AnalysisResult { get; set; }
        public decimal RequestedAmount { get; set; }
        public decimal? GrantedAmount { get; set; }
        public IEnumerable<Transactions> RequestedTransactions { get; set; }
    }
}