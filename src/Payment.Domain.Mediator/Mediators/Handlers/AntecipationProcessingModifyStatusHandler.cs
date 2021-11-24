using MediatR;
using Payment.Domain.Contracts;
using Payment.Domain.Interfaces.Services;
using Payment.Domain.Mediator.Mediators.Requests;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Payment.Domain.Mediator.Mediators.Handlers
{
    internal class AntecipationProcessingModifyStatusHandler : IRequestHandler<AntecipationProcessingModifyStatusRequest, IEnumerable<AntecipationsContract>>
    {
        private readonly IAntecipationService _service;

        public AntecipationProcessingModifyStatusHandler(IAntecipationService service)
        {
            _service = service;
        }

        public Task<IEnumerable<AntecipationsContract>> Handle(AntecipationProcessingModifyStatusRequest request, CancellationToken cancellationToken)
        {
            return _service.ModifyStatusAsync(request.Contract, cancellationToken);
        }
    }
}