using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Payment.Application.WebApi.Models.ResultModel
{
    public class TransactionJson : IActionResult
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool BankConfirmation { get; set; }
        public decimal Amount { get; set; }

        public TransactionJson()
        { }

        public TransactionJson(
            string id,
            DateTime createdAt,
            bool bankConfirmation,
            decimal amount)
        {
            Id = id;
            CreatedAt = createdAt;
            BankConfirmation = bankConfirmation;
            Amount = amount;
        }

        public TransactionJson(Transactions transaction)
        {
            Id = transaction.Id.ToString();
            CreatedAt = transaction.TransactionDate;
            BankConfirmation = transaction.BankConfirmation;
            Amount = transaction.Amount;
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            return new JsonResult(this).ExecuteResultAsync(context);
        }
    }
}