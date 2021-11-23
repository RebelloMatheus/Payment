using Payment.Application.WebApi.Converters.Interfaces;
using Payment.Application.WebApi.Models.ResultModel;
using Payment.Domain.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Payment.Application.WebApi.Converters
{
    internal class ConvertersTransaction : IConvertersTransaction
    {
        public TransactionJson ConvertContractToJson(TransactionContract contract)
        {
            return new TransactionJson(
                  id: contract.Id.ToString(),
                  createdAt: contract.TransactionDate,
                  bankConfirmation: contract.BankConfirmation,
                  amount: contract.Amount);
        }

        public TransactionListJson ConvertContractToJson(IEnumerable<TransactionContract> contracts)
        {
            var transactionListJson = new TransactionListJson
            {
                Transactions = contracts.Select(ConvertContractToJson).ToList(),
                Count = contracts.Count()
            };

            return transactionListJson;
        }
    }
}