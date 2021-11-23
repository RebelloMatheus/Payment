using Payment.Application.WebApi.Models.ResultModel;
using Payment.Domain.Contracts;
using System.Collections.Generic;

namespace Payment.Application.WebApi.Converters.Interfaces
{
    public interface IConvertersAntecipation
    {
        public AntecipationJson ConvertContractToJson(AntecipationsContract contract);

        public AntecipationListJson ConvertContractToListJson(IEnumerable<AntecipationsContract> contracts);
    }
}