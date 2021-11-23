using Microsoft.AspNetCore.Mvc;
using Payment.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.Application.WebApi.Models.ResultModel
{
    public class TransactionListJson : IActionResult
    {
        public IEnumerable<TransactionJson> Transactions { get; set; }
        public long Count { get; set; }

        public TransactionListJson()
        { }

        public TransactionListJson(IEnumerable<Transactions> transactions)
        {
            Transactions = transactions.Select(a => new TransactionJson(a)).ToList();
            Count = transactions.Count();
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            return new JsonResult(this).ExecuteResultAsync(context);
        }
    }
}