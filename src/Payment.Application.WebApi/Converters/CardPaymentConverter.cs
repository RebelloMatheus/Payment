using Payment.Application.WebApi.Converters.Interfaces;
using Payment.Application.WebApi.Models.ViewModel;
using Payment.Domain.Contracts;

namespace Payment.Application.WebApi.Converters
{
    internal class CardPaymentConverter : ICardPaymentConverter
    {
        public CardPaymentContract ConvertViewModelToContract(CardPayment viewModel)
        {
            return new CardPaymentContract(
                cardNumber: viewModel.CardNumber,
                amount: viewModel.Amount,
                installmentsNumber: viewModel.InstallmentsNumber);
        }
    }
}