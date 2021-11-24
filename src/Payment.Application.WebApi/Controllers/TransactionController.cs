using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Payment.Application.WebApi.Converters.Interfaces;
using Payment.Application.WebApi.Models;
using Payment.Application.WebApi.Models.ResultModel;
using Payment.Application.WebApi.Models.ViewModel;
using Payment.Domain.Execption;
using Payment.Domain.Mediator.Mediators.Requests;
using System.Threading.Tasks;

namespace Payment.Application.WebApi.Controllers
{
    [ApiController]
    [Route("api/v2/[controller]")]
    public class TransactionController : Controller
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly IMediator _mediator;
        private readonly ICardPaymentConverter _cardPaymentConverter;
        private readonly IConvertersTransaction _convertersTransaction;

        public TransactionController(
            ILogger<TransactionController> logger,
            IMediator mediator,
            ICardPaymentConverter cardPaymentConverter,
            IConvertersTransaction convertersTransaction)
        {
            _mediator = mediator;
            _logger = logger;
            _cardPaymentConverter = cardPaymentConverter;
            _convertersTransaction = convertersTransaction;
        }

        /// <summary>
        /// Pay by credit card
        /// </summary>
        [HttpPost, Route("process")]
        public async Task<IActionResult> Process([FromBody] CardPayment cardPayment)
        {
            var contract = _cardPaymentConverter.ConvertViewModelToContract(cardPayment);
            try
            {
                var request = new CardTransactionProcessingRequest(contract);
                var response = await _mediator.Send(request);

                return _convertersTransaction.ConvertContractToJson(response);
            }
            catch (PaymentException ex)
            {
                return new ErrorJson(
                    new Error
                    {
                        Message = ex.Message,
                        StatusCode = ex.StatusCodigo,
                        Uri = "api/v2/transaction/process"
                    });
            }
        }

        /// <summary>
        /// View all transactions
        /// </summary>
        [HttpGet, Route("get")]
        public async Task<IActionResult> Get()
        {
            var request = new TransactionGetIdRequest(null);
            var response = await _mediator.Send(request);

            return _convertersTransaction.ConvertContractToJson(response);
        }

        /// <summary>
        /// Query a transaction and its installments from the transaction identifier.
        /// </summary>
        [HttpGet, Route("get/{id:int}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var request = new TransactionGetIdRequest(id);
            var response = await _mediator.Send(request);

            return _convertersTransaction.ConvertContractToJson(response);
        }
    }
}