using FluentAssertions;
using NUnit.Framework;
using Payment.Domain.Contracts;
using Payment.Domain.Execption;
using Payment.Domain.Interfaces.Services;
using Payment.Domain.Models;
using Payment.Infra.DataBase.Repository.Base;
using Payment.Test.Tests.Factories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Payment.Test.Tests.Functional.Services
{
    internal class AntecipationSeriveTest : ModelTestBase
    {
        private IAntecipationService _serviceAntecipation;
        private ITransactionService _serviceTransaction;
        private IRepositoryBase _repositoryBase;

        protected override void SetUpPayment()
        {
            _serviceAntecipation = GetService<IAntecipationService>();
            _serviceTransaction = GetService<ITransactionService>();
            _repositoryBase = GetService<IRepositoryBase>();
        }

        [Test]
        public async Task RequestAntecipationIsValid()
        {
            await GivenAnyPaymentValid(30).ConfigureAwait(false);
            var a = new List<int> { 1, 2, 3, 4 };

            var ret = await _serviceAntecipation.RequestAntecipationAsync(a).ConfigureAwait(false);

            ret.Should().NotBeNull();
        }

        [Test]
        public async Task RequestAntecipationNotValid()
        {
            await GivenAnyPaymentValid(30).ConfigureAwait(false);
            var transactionsAntecipation = new List<int> { 1, 2, 3, 4 };
            await _serviceAntecipation.RequestAntecipationAsync(new List<int> { 1, 2, 3 }).ConfigureAwait(false);
            Func<Task> ret = async () => await _serviceAntecipation.RequestAntecipationAsync(new List<int> { 3 }).ConfigureAwait(false);

            ret.Should().Throw<PaymentException>().WithMessage(SR.PAYMENT_BLOCKED_APPROVED);
        }

        private async Task GivenAnyPaymentValid(int repet)
        {
            for (int i = 0; i < repet; i++)
            {
                var cardPayment = GivenAPaymentValid();

                await _serviceTransaction.ProcessAsync(cardPayment).ConfigureAwait(false);
            }
        }

        private CardPaymentContract GivenAPaymentValid()
        {
            var installmentsNumber = new Random().Next(12);
            if (installmentsNumber == 0)
                installmentsNumber = 1;
            return new CardPaymentContractBuilder()
                .SetGenerateCardNumberValid()
                .SetGenerateAmount()
                .SetInstallmentsNumber(installmentsNumber)
                .Build();
        }
    }
}