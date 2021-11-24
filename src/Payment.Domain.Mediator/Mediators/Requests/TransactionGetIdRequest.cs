using MediatR;
using Payment.Domain.Contracts;
using System.Collections.Generic;

namespace Payment.Domain.Mediator.Mediators.Requests
{
    public class TransactionGetIdRequest : IRequest<IEnumerable<TransactionContract>>
    {
        public int? Id { get; private set; }

        public TransactionGetIdRequest(int? id)
        {
            Id = id;
        }
    }
}