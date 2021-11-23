using MediatR;
using Payment.Domain.Contracts;

namespace Payment.Domain.Mediator.Mediators.Requests
{
    public class CardTransactionProcessingRequest : IRequest<TransactionContract>
    {
        public CardPaymentContract Contract { get; private set; }

        public CardTransactionProcessingRequest(CardPaymentContract contract)
        {
            Contract = contract;
        }
    }
}