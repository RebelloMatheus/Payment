using Payment.Domain.Contracts;
using Payment.Domain.Interfaces.Converters;
using Payment.Domain.Models;
using System.Collections.Generic;

namespace Payment.Domain.Service.Converters
{
    internal class ConvertersInstallments : IConvertersInstallments
    {
        public InstallmentsContract ConvertEntityToContract(Installments entity)
        {
            return new InstallmentsContract(
                id: entity.Id,
                paymentId: entity.PaymentId,
                amount: entity.Amount,
                netAmount: entity.NetAmount,
                number: entity.Number,
                antecipationValue: entity.AntecipationValue,
                expectedPaymentDate: entity.ExpectedPaymentDate,
                antecipationDate: entity.AntecipationDate);
        }

        public IEnumerable<InstallmentsContract> ConvertEntityToContract(IEnumerable<Installments> entitys)
        {
            foreach (var entity in entitys)
            {
                yield return ConvertEntityToContract(entity);
            }
        }
    }
}