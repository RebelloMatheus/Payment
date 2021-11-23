using MediatR;
using Payment.Domain.Contracts;
using Payment.Domain.Interfaces.Services;
using Payment.Domain.Mediator.Mediators.Requests;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Payment.Domain.Mediator.Mediators.Handlers
{
    internal class TransactionGetIdHandler : IRequestHandler<TransactionGetIdRequest, IEnumerable<TransactionContract>>
    {
        private readonly ITransactionService _service;

        public TransactionGetIdHandler(ITransactionService service)
        {
            _service = service;
        }

        public Task<IEnumerable<TransactionContract>> Handle(TransactionGetIdRequest request, CancellationToken cancellationToken)
        {
            return _service.GetIdAssync(request.Id);
        }
    }
}