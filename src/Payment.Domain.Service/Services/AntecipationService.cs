using Payment.Domain.Contracts;
using Payment.Domain.Enumerators;
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
    internal class AntecipationService : IAntecipationService
    {
        private readonly IRepositoryBase _repositoryBase;
        private readonly ITransactionService _transactionService;
        private readonly IConvertersAntecipation _convertersAntecipation;

        public AntecipationService(
            IRepositoryBase repositoryBase,
            ITransactionService transactionService,
            IConvertersAntecipation convertersAntecipation)
        {
            _repositoryBase = repositoryBase;
            _transactionService = transactionService;
            _convertersAntecipation = convertersAntecipation;
        }

        public async Task<IEnumerable<AntecipationsContract>> GetListHistoryAsync(string status, CancellationToken cancellationToken = default)
        {
            var antecipationsContract = Enumerable.Empty<AntecipationsContract>();
            Expression<Func<Antecipations, bool>> filtro = x => false;
            switch (status)
            {
                case "pending":
                    filtro = antecipation => !antecipation.AnalysisStartDate.HasValue;
                    break;

                case "analysis":
                    filtro = antecipation => antecipation.AnalysisStartDate.HasValue && !antecipation.AnalysisEndDate.HasValue;
                    break;

                case "finished":
                    filtro = antecipation => antecipation.AnalysisResult.HasValue;
                    break;

                default:
                    return antecipationsContract;
            }

            var antecipations = await _repositoryBase.GetAllAsync(filtro).ConfigureAwait(false);

            return _convertersAntecipation.ConvertEntityToContract(antecipations).ToArray();
        }

        public async Task<IEnumerable<AntecipationsContract>> ModifyStatusAsync(ModifyStatusAntecipationContract contract, CancellationToken cancellationToken = default)
        {
            var transactions = await _transactionService.GetIdsAssync(contract.TransactionsId);

            var antecipationsIds = transactions
                .GroupBy(p => p.AntecipationId)
                .Select(g => g.Key);

            Expression<Func<Antecipations, bool>> filtro =
                antecipation => contract.TransactionsId.Contains(antecipation.Id)
                    && antecipation.AnalysisStartDate.HasValue;

            var antecipations = await _repositoryBase.GetAllListAsync(filtro).ConfigureAwait(false);

            return await ModifyStatusProcessingAsync(antecipations, contract);
        }

        public async Task<AntecipationsContract> StartAnalyzisAsync(int antecipationId, CancellationToken cancellationToken = default)
        {
            var antecipation = await _repositoryBase.GetIdAsync<Antecipations>(antecipationId).ConfigureAwait(false);

            if (antecipation == null || antecipation.AnalysisStartDate.HasValue)
            {
                throw new PaymentException(
                    message: SR.PAYMENT_BLOCKED_APPROVED,
                    statusCodigo: HttpStatusCode.BadRequest);
            }

            antecipation.UpdateAnalysisStartDate(DateTime.Now);

            await _repositoryBase.UpdateAsync(antecipation).ConfigureAwait(false);

            return _convertersAntecipation.ConvertEntityToContract(antecipation);
        }

        public async Task<IEnumerable<AntecipationsContract>> RequestAntecipationAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default)
        {
            Expression<Func<Transactions, bool>> filtro =
                t => t.BankConfirmation
                    && !t.AntecipationId.HasValue
                    && ids.Contains(t.Id);

            var transactions = await _repositoryBase.GetAllAsync(filtro).ConfigureAwait(false);

            if (!transactions.Any())
            {
                throw new PaymentException(
                    message: SR.PAYMENT_BLOCKED_APPROVED,
                    statusCodigo: HttpStatusCode.BadRequest);
            }

            Antecipations antecipations = new Antecipations(
                requestDate: DateTime.Now,
                requestedAmount: transactions.Sum(p => p.NetAmount),
                analysisStartDate: null,
                requestedTransactions: transactions);

            await _repositoryBase.AddAsync(antecipations);
            foreach (var transaction in transactions)
            {
                transaction.UpdateAntecipationId(antecipations.Id);
                await _repositoryBase.UpdateAsync(transaction);
            }
            var antecipationContract = _convertersAntecipation.ConvertEntityToContract(antecipations);
            return new List<AntecipationsContract> { antecipationContract };
        }

        private async Task<IEnumerable<AntecipationsContract>> ModifyStatusProcessingAsync(IList<Antecipations> entitys, ModifyStatusAntecipationContract contract)
        {
            foreach (var antecipation in entitys)
            {
                var allTransactions = await _transactionService.GetAllAntecipationIdAsync(antecipation.Id);

                foreach (var requestedTransaction in antecipation.RequestedTransactions
                                    .Where(rt => contract.TransactionsId.Contains(rt.Id)))
                {
                    requestedTransaction.UpdateAntecipationStatus((AntecipationStatus?)contract.AntecipationStatus);

                    foreach (var installment in requestedTransaction?.Installments)
                    {
                        if (contract.AntecipationStatus == AntecipationStatus.Approved)
                        {
                            installment.UpdateAntecipationDate(DateTime.Now);
                            installment.UpdateAntecipationValue(Math.Round(installment.NetAmount * Convert.ToDecimal(1 - (3.8 / 100)), 2));
                        }
                        else
                        {
                            installment.UpdateAntecipationDate(null);
                            installment.UpdateAntecipationValue(null);
                        }

                        await _repositoryBase.UpdateAsync(installment);
                    }
                }

                bool canFinalizeAntecipation = allTransactions.All(t => t.AntecipationStatus.HasValue);

                if (canFinalizeAntecipation)
                {
                    AnalysisResult analysisResult = AnalysisResult.Rejected;

                    int numberPayments = antecipation.RequestedTransactions.Count();
                    int numberPaymentsApproved = antecipation.RequestedTransactions.Count(p => p.AntecipationStatus == AntecipationStatus.Approved);
                    int numberPaymentsRejected = antecipation.RequestedTransactions.Count(p => p.AntecipationStatus == AntecipationStatus.Rejected);

                    if (numberPaymentsApproved == numberPayments)
                    {
                        analysisResult = AnalysisResult.Approved;
                        antecipation.UpdateGrantedAmount(allTransactions.Sum(p => p.Installments.Sum(i => i.AntecipationValue)));
                    }
                    else if (numberPaymentsRejected == numberPayments)
                    {
                        analysisResult = AnalysisResult.Rejected;
                    }
                    else if (numberPaymentsApproved > 0 && numberPaymentsRejected > 0)
                    {
                        analysisResult = AnalysisResult.PartiallyApproved;
                        antecipation.UpdateGrantedAmount(allTransactions.Sum(p => p.Installments.Sum(i => i.AntecipationValue)));
                    }

                    antecipation.UpdateAnalysisResult(analysisResult);
                    antecipation.UpdateAnalysisEndDate(DateTime.Now);
                }

                await _repositoryBase.UpdateAsync(antecipation);
            }

            return _convertersAntecipation.ConvertEntityToContract(entitys).ToArray();
        }
    }
}