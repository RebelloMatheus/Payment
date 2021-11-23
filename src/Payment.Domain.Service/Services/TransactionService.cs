using Payment.Domain.Contracts;
using Payment.Domain.Execption;
using Payment.Domain.Interfaces.Converters;
using Payment.Domain.Interfaces.Services;
using Payment.Domain.Models;
using Payment.Infra.DataBase.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Payment.Domain.Service.Services
{
    public class TransactionService : ITransactionService
    {
        private const decimal FlatRate = 0.9M;
        private readonly IRepositoryBase _repositoryBase;
        private readonly IConvertersTransaction _convertersTransaction;

        public TransactionService(IRepositoryBase repositoryBase, IConvertersTransaction convertersTransaction)
        {
            _repositoryBase = repositoryBase;
            _convertersTransaction = convertersTransaction;
        }

        public async Task<IEnumerable<TransactionContract>> GetAllAntecipationIdAsync(int id)
        {
            Expression<Func<Transactions, bool>> filtro = x => x.AntecipationId == id;

            var transactions = await _repositoryBase.GetAllAsync(filtro).ConfigureAwait(false);

            return _convertersTransaction.ConvertEntityToContract(transactions).ToArray();
        }

        public async Task<IEnumerable<TransactionContract>> GetIdAssync(int? id)
        {
            Expression<Func<Transactions, bool>> filtro = x => x.Id == id || id == null;

            var transactions = await _repositoryBase.GetAllAsync(filtro).ConfigureAwait(false);

            return _convertersTransaction.ConvertEntityToContract(transactions).ToArray();
        }

        public async Task<IEnumerable<TransactionContract>> GetIdsAssync(IEnumerable<int> ids)
        {
            Expression<Func<Transactions, bool>> filtro = x => ids.Contains(x.Id);

            var transactions = await _repositoryBase.GetAllAsync(filtro).ConfigureAwait(false);

            return _convertersTransaction.ConvertEntityToContract(transactions).ToArray();
        }

        public async Task<IEnumerable<TransactionContract>> ListAvailableAssync()
        {
            Expression<Func<Transactions, bool>> filtro = x => x.AntecipationId.HasValue;

            var transactions = await _repositoryBase.GetAllAsync(filtro).ConfigureAwait(false);

            return _convertersTransaction.ConvertEntityToContract(transactions).ToArray();
        }

        public async Task<TransactionContract> ProcessAsync(CardPaymentContract contract, CancellationToken cancellationToken = default)
        {
            string cardNumber = contract.CardNumber.ToString();
            bool approvPayment = true;
            if (cardNumber.EndsWith("5999") || cardNumber.Length != 16)
            {
                approvPayment = false;
            }

            decimal netAmount = contract.Amount - FlatRate;
            var transaction = new Transactions(
                transactionDate: DateTime.Now,
                bankConfirmation: approvPayment,
                amount: contract.Amount,
                netAmount: netAmount,
                flatRate: FlatRate,
                installmentsNumber: contract.InstallmentsNumber,
                fourLastCardNumber: cardNumber.Length != 16 ? null : cardNumber.Substring(12, 4));

            if (approvPayment)
            {
                transaction.UpdateApprovedDate(DateTime.Now);
                var installments = await FillInstallments(transaction);
                transaction.UpdateInstallments(installments);
            }
            else
            {
                transaction.UpdateNotApprovedDate(DateTime.Now);
            }

            await _repositoryBase.AddAsync(transaction).ConfigureAwait(false);

            if (!approvPayment)
            {
                throw new PaymentException(
                    message: "Transação recusada, cartão inválido.",
                    statusCodigo: HttpStatusCode.BadRequest,
                    uri: "api/v2/transaction/process");
            }

            return _convertersTransaction.ConvertEntityToContract(transaction);
        }

        private async Task<IEnumerable<Installments>> FillInstallments(Transactions transaction)
        {
            decimal amountPerInstallment = Math.Round(transaction.Amount / transaction.InstallmentsNumber, 2);
            decimal netAmountPerInstallment = Math.Round(transaction.NetAmount / transaction.InstallmentsNumber, 2);

            return Enumerable.Range(1, transaction.InstallmentsNumber)
                .Select(i => new Installments(
                    amount: amountPerInstallment,
                    expectedPaymentDate: GenerateExpectedPaymentDate(i),
                    netAmount: netAmountPerInstallment,
                    number: i)).ToList();
        }

        private static DateTime GenerateExpectedPaymentDate(int i)
        {
            var date = DateTime.Today.AddMonths(i);
            var dayLastMonth = LastDayOfMonth(date);
            if (dayLastMonth < 30)
                return new DateTime(date.Year, date.Month, dayLastMonth);

            return new DateTime(date.Year, date.Month, 30);
        }

        private static int LastDayOfMonth(DateTime date)
        {
            return DateTime.DaysInMonth(date.Year, date.Month);
        }
    }
}