using MediatR;
using Payment.Domain.Contracts;
using System.Collections.Generic;

namespace Payment.Domain.Mediator.Mediators.Requests
{
    public class AntecipationProcessingGetListAvailableRequest : IRequest<IEnumerable<TransactionContract>>
    {
        public AntecipationProcessingGetListAvailableRequest()
        {
        }
    }
}