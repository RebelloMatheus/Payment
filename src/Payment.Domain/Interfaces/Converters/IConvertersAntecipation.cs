using Payment.Domain.Contracts;
using Payment.Domain.Models;
using System.Collections.Generic;

namespace Payment.Domain.Interfaces.Converters
{
    public interface IConvertersAntecipation
    {
        AntecipationsContract ConvertEntityToContract(Antecipations entity);

        IEnumerable<AntecipationsContract> ConvertEntityToContract(IEnumerable<Antecipations> entitys);
    }
}