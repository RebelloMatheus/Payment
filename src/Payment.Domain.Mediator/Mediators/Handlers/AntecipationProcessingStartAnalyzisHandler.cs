using MediatR;
using Payment.Domain.Contracts;
using Payment.Domain.Interfaces.Services;
using Payment.Domain.Mediator.Mediators.Requests;
using System.Threading;
using System.Threading.Tasks;

namespace Payment.Domain.Mediator.Mediators.Handlers
{
    internal class AntecipationProcessingStartAnalyzisHandler : IRequestHandler<AntecipationProcessingStartAnalyzisRequest, AntecipationsContract>
    {
        private readonly IAntecipationService _service;

        public AntecipationProcessingStartAnalyzisHandler(IAntecipationService service)
        {
            _service = service;
        }

        public Task<AntecipationsContract> Handle(AntecipationProcessingStartAnalyzisRequest request, CancellationToken cancellationToken)
        {
            return _service.StartAnalyzisAsync(request.TransactionsId, cancellationToken);
        }
    }
}