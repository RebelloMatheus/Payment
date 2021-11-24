using Payment.Application.WebApi.Converters.Interfaces;
using Payment.Application.WebApi.Enumerators;
using Payment.Application.WebApi.Models.ResultModel;
using Payment.Domain.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Payment.Application.WebApi.Converters
{
    internal class ConvertersAntecipation : IConvertersAntecipation
    {
        public AntecipationJson ConvertContractToJson(AntecipationsContract contract)
        {
            return new AntecipationJson(
                id: contract.Id.ToString(),
                createdAt: contract.RequestDate,
                analysisResult: (AnalysisResult)contract.AnalysisResult,
                analysisStartDate: contract.AnalysisStartDate,
                requestedAmount: contract.RequestedAmount,
                grantedAmount: contract.GrantedAmount);
        }

        public IEnumerable<AntecipationJson> ConvertContractToJson(IEnumerable<AntecipationsContract> contracts)
        {
            foreach (var contract in contracts)
            {
                yield return ConvertContractToJson(contract);
            }
        }

        public AntecipationListJson ConvertContractToListJson(IEnumerable<AntecipationsContract> contracts)
        {
            var antecipations = ConvertContractToJson(contracts).ToArray();

            return new AntecipationListJson
            {
                Antecipations = antecipations,
                Count = contracts.Count()
            };
        }
    }
}