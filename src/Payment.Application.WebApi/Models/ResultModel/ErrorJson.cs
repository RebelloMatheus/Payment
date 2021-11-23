using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Payment.Application.WebApi.Models.ResultModel
{
    public class ErrorJson : IActionResult
    {
        public Error Error { get; set; }

        public ErrorJson()
        { }

        public ErrorJson(Error error)
        {
            Error = error;
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            return new JsonResult(this).ExecuteResultAsync(context);
        }
    }
}