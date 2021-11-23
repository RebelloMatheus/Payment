using Microsoft.AspNetCore.Mvc;
using Payment.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.Application.WebApi.Models.ResultModel
{
    public class AntecipationListJson : IActionResult
    {
        public IEnumerable<AntecipationJson> Antecipations { get; set; }
        public long Count { get; set; }

        public AntecipationListJson()
        { }

        public AntecipationListJson(IEnumerable<Antecipations> antecipations)
        {
            Antecipations = antecipations.Select(a => new AntecipationJson(a)).ToList();
            Count = antecipations.Count();
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            return new JsonResult(this).ExecuteResultAsync(context);
        }
    }
}