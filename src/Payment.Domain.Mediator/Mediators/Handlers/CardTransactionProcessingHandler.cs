using MediatR;
using Payment.Domain.Contracts;
using Payment.Domain.Interfaces.Services;
using Payment.Domain.Mediator.Mediators.Requests;
using System.Threading;
using System.Threading.Tasks;

namespace Payment.Domain.Mediator.Mediators.Handlers
{
    internal class CardTransactionProcessingHandler : IRequestHandler<CardTransactionProcessingRequest, TransactionContract>
    {
        private readonly ITransactionService _service;

        public CardTransactionProcessingHandler(ITransactionService service)
        {
            _service = service;
        }

        public Task<TransactionContract> Handle(CardTransactionProcessingRequest request, CancellationToken cancellationToken)
        {
            return _service.ProcessAsync(request.Contract, cancellationToken);
        }
    }
}