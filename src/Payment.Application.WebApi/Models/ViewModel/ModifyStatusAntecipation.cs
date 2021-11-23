using Payment.Application.WebApi.Enumerators;
using System.Collections.Generic;

namespace Payment.Application.WebApi.Models.ViewModel
{
    public class ModifyStatusAntecipation
    {
        public AntecipationStatus AntecipationStatus { get; set; }
        public IEnumerable<int> TransactionsId { get; set; }
    }
}