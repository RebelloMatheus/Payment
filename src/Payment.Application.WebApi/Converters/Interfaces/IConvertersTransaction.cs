using Payment.Application.WebApi.Models.ResultModel;
using Payment.Domain.Contracts;
using System.Collections.Generic;

namespace Payment.Application.WebApi.Converters.Interfaces
{
    public interface IConvertersTransaction
    {
        TransactionJson ConvertContractToJson(TransactionContract contract);

        TransactionListJson ConvertContractToJson(IEnumerable<TransactionContract> contracts);
    }
}