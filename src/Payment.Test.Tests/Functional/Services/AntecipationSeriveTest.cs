using NUnit.Framework;
using Payment.Domain.Contracts;
using Payment.Domain.Interfaces.Services;
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

        protected override void SetUpPayment()
        {
            _serviceAntecipation = GetService<IAntecipationService>();
            _serviceTransaction = GetService<ITransactionService>();
        }

        [Test]
        public async Task RequestAntecipationIsValid()
        {
            for (int i = 0; i < 30; i++)
            {
                var cardPayment = GivenAPaymentValid();

                var ret = await _serviceTransaction.ProcessAsync(cardPayment).ConfigureAwait(false);
            }
            var a = new List<int> { 1, 2, 3, 4 };
            await _serviceAntecipation.RequestAntecipationAsync(a).ConfigureAwait(false);
        }

        [Test]
        public async Task RequestAntecipationNotValid()
        {
            for (int i = 0; i < 30; i++)
            {
                var cardPayment = GivenAPaymentValid();

                var ret = await _serviceTransaction.ProcessAsync(cardPayment).ConfigureAwait(false);
            }
            var transactionsAntecipation = new List<int> { 1, 2, 3, 4 };
            await _serviceAntecipation.RequestAntecipationAsync(transactionsAntecipation).ConfigureAwait(false);

            await _serviceAntecipation.RequestAntecipationAsync(new List<int> { 3 }).ConfigureAwait(false);
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