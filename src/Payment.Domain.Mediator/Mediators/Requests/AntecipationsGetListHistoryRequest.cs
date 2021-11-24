using MediatR;
using Payment.Domain.Contracts;
using System.Collections.Generic;

namespace Payment.Domain.Mediator.Mediators.Requests
{
    public class AntecipationsGetListHistoryRequest : IRequest<IEnumerable<AntecipationsContract>>
    {
        public string Status { get; set; }

        public AntecipationsGetListHistoryRequest(string status)
        {
            Status = status;
        }
    }
}