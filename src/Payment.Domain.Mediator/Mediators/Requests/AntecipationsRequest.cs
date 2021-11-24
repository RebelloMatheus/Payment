using MediatR;
using Payment.Domain.Contracts;
using System.Collections.Generic;

namespace Payment.Domain.Mediator.Mediators.Requests
{
    public class AntecipationsRequest : IRequest<IEnumerable<AntecipationsContract>>
    {
        public IEnumerable<int> TransactionsId { get; private set; }

        public AntecipationsRequest(IEnumerable<int> transactionsId)
        {
            TransactionsId = transactionsId;
        }
    }
}