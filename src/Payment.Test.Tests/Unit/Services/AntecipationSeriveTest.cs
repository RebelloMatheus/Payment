using FluentAssertions;
using NUnit.Framework;
using Payment.Domain.Contracts;
using Payment.Domain.Interfaces.Services;
using Payment.Test.Tests.Factories;
using Payment.Test.Tests.Functional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.Test.Tests.Unit.Services
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
            var cardPayment = GivenAPaymentValid();

            await _serviceTransaction.ProcessAsync(cardPayment).ConfigureAwait(false);
            var antecipation = await _serviceAntecipation.RequestAntecipationAsync(new List<int> { 1 });
            await _serviceAntecipation.StartAnalyzisAsync(antecipation.ElementAt(0).Id);
            var ret = await _serviceAntecipation.GetListHistoryAsync("analysis").ConfigureAwait(false);

            ret.Should().NotBeNull();
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