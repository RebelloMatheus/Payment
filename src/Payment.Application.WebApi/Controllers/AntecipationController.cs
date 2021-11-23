using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Payment.Application.WebApi.Converters.Interfaces;
using Payment.Application.WebApi.Models.ViewModel;
using Payment.Domain.Mediator.Mediators.Requests;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.Application.WebApi.Controllers
{
    [ApiController]
    [Route("api/v2/[controller]")]
    public class AntecipationController : Controller
    {
        private readonly IConvertersTransaction _convertersTransaction;
        private readonly IConvertersAntecipation _convertersAntexipation;
        private readonly IConvertersModifyStatusAntecipation _convertersModifyStatusAntecipation;
        private readonly ILogger<AntecipationController> _logger;
        private readonly IMediator _mediator;

        public AntecipationController(
            IConvertersTransaction convertersTransaction,
            IConvertersAntecipation convertersAntexipation,
            IConvertersModifyStatusAntecipation convertersModifyStatusAntecipation,
            ILogger<AntecipationController> logger,
            IMediator mediator)
        {
            _convertersTransaction = convertersTransaction;
            _convertersAntexipation = convertersAntexipation;
            _convertersModifyStatusAntecipation = convertersModifyStatusAntecipation;
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Consult available transactions to request advance payment (no filters needed)
        /// </summary>
        [HttpGet, Route("list-available-transactions")]
        public async Task<IActionResult> ListAvailableTransactions()
        {
            var request = new AntecipationProcessingGetListAvailableRequest();
            var response = await _mediator.Send(request);
            return _convertersTransaction.ConvertContractToJson(response);
        }

        /// <summary>
        /// Request advance from a list of transactions
        /// </summary>
        [HttpPost, Route("request-antecipations")]
        public async Task<IActionResult> RequestAntecipation([FromBody] RequestAntecipations requestAntecipations)
        {
            var request = new AntecipationsRequest(requestAntecipations.TransactionsId);

            var response = await _mediator.Send(request);

            return _convertersAntexipation.ConvertContractToJson(response.First());
        }

        /// <summary>
        /// Start anticipation service
        /// </summary>
        [HttpPost, Route("start-analyzis")]
        public async Task<IActionResult> StartAnalyzis([FromBody] StartAnalyzis startAnalyzis)
        {
            var request = new AntecipationProcessingStartAnalyzisRequest(startAnalyzis.AntecipationId);

            var response = await _mediator.Send(request);

            return _convertersAntexipation.ConvertContractToJson(response);
        }

        /// <summary>
        /// Approve or disapprove one or more transactions in the prepayment (when all transactions are completed, the prepayment will be terminated)
        /// </summary>
        [HttpPut, Route("modify-status")]
        public async Task<IActionResult> ModifyStatus([FromBody] ModifyStatusAntecipation modifyStatusAntecipation)
        {
            var contract = _convertersModifyStatusAntecipation.ConvertViewModelToContract(modifyStatusAntecipation);
            var request = new AntecipationProcessingModifyStatusRequest(contract);

            var response = await _mediator.Send(request);

            return _convertersAntexipation.ConvertContractToListJson(response);
        }

        /// <summary>
        /// Consult prepayment history with filter by status (pending, under analysis, completed)
        /// </summary>
        [HttpGet, Route("list-history/{status=string}")]
        public async Task<IActionResult> ListHistory([FromRoute] string status)
        {
            var request = new AntecipationsGetListHistoryRequest(status);

            var response = await _mediator.Send(request);

            return _convertersAntexipation.ConvertContractToListJson(response);
        }
    }
}