using Microsoft.AspNetCore.Mvc;
using Payment.Application.WebApi.Enumerators;
using System;
using System.Threading.Tasks;

namespace Payment.Application.WebApi.Models.ResultModel
{
    public class AntecipationJson : IActionResult
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public AnalysisResult? AnalysisResult { get; set; }
        public DateTime? AnalysisStartDate { get; set; }
        public decimal RequestedAmount { get; set; }
        public decimal? GrantedAmount { get; set; }

        public AntecipationJson()
        { }

        public AntecipationJson(
            string id,
            DateTime createdAt,
            AnalysisResult? analysisResult,
            DateTime? analysisStartDate,
            decimal requestedAmount,
            decimal? grantedAmount)
        {
            Id = id;
            CreatedAt = createdAt;
            AnalysisResult = analysisResult;
            AnalysisStartDate = analysisStartDate;
            RequestedAmount = requestedAmount;
            GrantedAmount = grantedAmount;
        }

        public AntecipationJson(Antecipations antecipation)
        {
            Id = antecipation.Id.ToString();
            CreatedAt = antecipation.RequestDate;
            AnalysisResult = (AnalysisResult)antecipation.AnalysisResult;
            AnalysisStartDate = antecipation.AnalysisStartDate;
            RequestedAmount = antecipation.RequestedAmount;
            GrantedAmount = antecipation.GrantedAmount;
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            return new JsonResult(this).ExecuteResultAsync(context);
        }
    }
}