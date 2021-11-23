using Payment.Domain.Contracts;
using Payment.Domain.Models;
using System.Collections.Generic;

namespace Payment.Domain.Interfaces.Converters
{
    public interface IConvertersInstallments
    {
        InstallmentsContract ConvertEntityToContract(Installments entity);

        IEnumerable<InstallmentsContract> ConvertEntityToContract(IEnumerable<Installments> entitys);
    }
}