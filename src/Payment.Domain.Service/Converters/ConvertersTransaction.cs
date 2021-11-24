using Payment.Domain.Contracts;
using Payment.Domain.Interfaces.Converters;
using Payment.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Payment.Domain.Service.Converters
{
    internal class ConvertersTransaction : IConvertersTransaction
    {
        private readonly IConvertersInstallments _convertersInstallments;
        private readonly IConvertersAntecipation _convertersAntecipation;

        public ConvertersTransaction(
            IConvertersInstallments convertersInstallments,
            IConvertersAntecipation convertersAntecipation)
        {
            _convertersInstallments = convertersInstallments;
            _convertersAntecipation = convertersAntecipation;
        }

        public TransactionContract ConvertEntityToContract(Transactions entity)
        {
            var installments = _convertersInstallments.ConvertEntityToContract(entity.Installments).ToArray();
            var antecipation = _convertersAntecipation.ConvertEntityToContract(entity.Antecipation);

            return new TransactionContract(
                id: entity.Id,
                transactionDate: entity.TransactionDate,
                approvedDate: entity.ApprovedDate,
                notApprovedDate: entity.NotApprovedDate,
                antecipationStatus: entity.AntecipationStatus,
                bankConfirmation: entity.BankConfirmation,
                amount: entity.Amount,
                netAmount: entity.NetAmount,
                flatRate: entity.FlatRate,
                installmentsNumber: entity.InstallmentsNumber,
                fourLastCardNumber: entity.FourLastCardNumber,
                antecipationId: entity.AntecipationId,
                installments: installments,
                antecipation: antecipation);
        }

        public IEnumerable<TransactionContract> ConvertEntityToContract(IEnumerable<Transactions> entitys)
        {
            foreach (var entity in entitys)
            {
                yield return ConvertEntityToContract(entity);
            }
        }
    }
}