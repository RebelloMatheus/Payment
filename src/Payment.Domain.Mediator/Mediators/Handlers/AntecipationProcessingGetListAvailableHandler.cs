using MediatR;
using Payment.Domain.Contracts;
using Payment.Domain.Interfaces.Services;
using Payment.Domain.Mediator.Mediators.Requests;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Payment.Domain.Mediator.Mediators.Handlers
{
    internal class AntecipationProcessingGetListAvailableHandler : IRequestHandler<AntecipationProcessingGetListAvailableRequest, IEnumerable<TransactionContract>>
    {
        private readonly ITransactionService _service;

        public AntecipationProcessingGetListAvailableHandler(ITransactionService service)
        {
            _service = service;
        }

        public Task<IEnumerable<TransactionContract>> Handle(AntecipationProcessingGetListAvailableRequest request, CancellationToken cancellationToken)
        {
            return _service.ListAvailableAssync();
        }
    }
}