using System;
using System.Text.Json.Serialization;

namespace Payment.Application.WebApi.Models
{
    public class Installments
    {
        public int Id { get; set; }
        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
        public decimal NetAmount { get; set; }
        public int Number { get; set; }
        public decimal? AntecipationValue { get; set; }
        public DateTime ExpectedPaymentDate { get; set; }
        public DateTime? AntecipationDate { get; set; }

        [JsonIgnore]
        public Transactions Payment { get; set; }
    }
}