using Payment.Domain.Contracts;
using Payment.Domain.Models;
using System.Collections.Generic;

namespace Payment.Domain.Interfaces.Converters
{
    public interface IConvertersTransaction
    {
        TransactionContract ConvertEntityToContract(Transactions entity);

        IEnumerable<TransactionContract> ConvertEntityToContract(IEnumerable<Transactions> entitys);
    }
}