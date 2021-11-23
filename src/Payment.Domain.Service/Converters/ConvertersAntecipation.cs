using Payment.Domain.Contracts;
using Payment.Domain.Interfaces.Converters;
using Payment.Domain.Models;
using System.Collections.Generic;

namespace Payment.Domain.Service.Converters
{
    internal class ConvertersAntecipation : IConvertersAntecipation
    {
        public AntecipationsContract ConvertEntityToContract(Antecipations entity)
        {
            if (entity is null)
                return null;
            return new AntecipationsContract(
                id: entity.Id,
                requestDate: entity.RequestDate,
                analysisStartDate: entity.AnalysisStartDate,
                analysisEndDate: entity.AnalysisEndDate,
                analysisResult: entity.AnalysisResult,
                requestedAmount: entity.RequestedAmount,
                grantedAmount: entity.GrantedAmount);
        }

        public IEnumerable<AntecipationsContract> ConvertEntityToContract(IEnumerable<Antecipations> entitys)
        {
            foreach (var entity in entitys)
            {
                yield return ConvertEntityToContract(entity);
            }
        }
    }
}