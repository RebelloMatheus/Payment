using MediatR;
using Payment.Domain.Contracts;

namespace Payment.Domain.Mediator.Mediators.Requests
{
    public class AntecipationProcessingStartAnalyzisRequest : IRequest<AntecipationsContract>
    {
        public int TransactionsId { get; private set; }

        public AntecipationProcessingStartAnalyzisRequest(int transactionsId)
        {
            TransactionsId = transactionsId;
        }
    }
}