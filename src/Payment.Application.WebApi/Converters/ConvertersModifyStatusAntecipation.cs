using Payment.Application.WebApi.Converters.Interfaces;
using Payment.Application.WebApi.Models.ViewModel;
using Payment.Domain.Contracts;
using EnumContract = Payment.Domain.Enumerators;

namespace Payment.Application.WebApi.Converters
{
    internal class ConvertersModifyStatusAntecipation : IConvertersModifyStatusAntecipation
    {
        public ModifyStatusAntecipationContract ConvertViewModelToContract(ModifyStatusAntecipation viewModel)
        {
            return new ModifyStatusAntecipationContract(
                antecipationStatus: (EnumContract.AntecipationStatus)viewModel.AntecipationStatus,
                transactionsId: viewModel.TransactionsId);
        }
    }
}