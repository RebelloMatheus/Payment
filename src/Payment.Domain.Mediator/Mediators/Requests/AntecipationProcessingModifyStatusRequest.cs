using MediatR;
using Payment.Domain.Contracts;
using System.Collections.Generic;

namespace Payment.Domain.Mediator.Mediators.Requests
{
    public class AntecipationProcessingModifyStatusRequest : IRequest<IEnumerable<AntecipationsContract>>
    {
        public ModifyStatusAntecipationContract Contract { get; private set; }

        public AntecipationProcessingModifyStatusRequest(ModifyStatusAntecipationContract contract)
        {
            Contract = contract;
        }
    }
}