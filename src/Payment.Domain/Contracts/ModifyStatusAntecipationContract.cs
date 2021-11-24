using Payment.Domain.Enumerators;
using System.Collections.Generic;

namespace Payment.Domain.Contracts
{
    public class ModifyStatusAntecipationContract
    {
        public AntecipationStatus AntecipationStatus { get; }
        public IEnumerable<int> TransactionsId { get; }

        public ModifyStatusAntecipationContract(
            AntecipationStatus antecipationStatus,
            IEnumerable<int> transactionsId)
        {
            AntecipationStatus = antecipationStatus;
            TransactionsId = transactionsId;
        }
    }
}