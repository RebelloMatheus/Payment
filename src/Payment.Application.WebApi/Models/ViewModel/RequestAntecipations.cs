using System.Collections.Generic;

namespace Payment.Application.WebApi.Models.ViewModel
{
    public class RequestAntecipations
    {
        public IEnumerable<int> TransactionsId { get; set; }
    }
}