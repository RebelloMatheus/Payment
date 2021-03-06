using Payment.Application.WebApi.Models.ViewModel;
using Payment.Domain.Contracts;

namespace Payment.Application.WebApi.Converters.Interfaces
{
    public interface ICardPaymentConverter
    {
        CardPaymentContract ConvertViewModelToContract(CardPayment viewModel);
    }
}