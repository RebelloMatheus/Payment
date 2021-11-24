using FluentAssertions;
using NUnit.Framework;
using Payment.Domain.Contracts;
using Payment.Domain.Execption;
using Payment.Domain.Interfaces.Services;
using Payment.Test.Tests.Factories;
using System;
using System.Threading.Tasks;

namespace Payment.Test.Tests.Functional.Services
{
    internal class TransactionServiceTest : ModelTestBase
    {
        private ITransactionService _service;

        protected override void SetUpPayment()
        {
            _service = GetService<ITransactionService>();
        }

        [Test]
        public async Task ProcessTransactionIsValid()
        {
            var cardPayment = GivenAPaymentValid();

            var ret = await _service.ProcessAsync(cardPayment).ConfigureAwait(false);

            Assert.IsNotNull(ret);
        }

        [Test]
        public async Task ProcessTransactionIsNotValid()
        {
            var cardPayment = GivenAPaymentNotValid();

            Func<Task> ret = async () => await _service.ProcessAsync(cardPayment).ConfigureAwait(false); ;

            ret.Should().Throw<PaymentException>();
        }

        private CardPaymentContract GivenAPaymentValid()
        {
            return new CardPaymentContractBuilder()
                .SetGenerateCardNumberValid()
                .SetGenerateAmount()
                .SetInstallmentsNumber(new Random().Next(12))
                .Build();
        }

        private CardPaymentContract GivenAPaymentNotValid()
        {
            return new CardPaymentContractBuilder()
                .SetGenerateCardNumberNotValid()
                .SetGenerateAmount()
                .SetInstallmentsNumber(new Random().Next(12))
                .Build();
        }
    }
}