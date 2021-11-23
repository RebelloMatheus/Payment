using FluentAssertions;
using NUnit.Framework;
using Payment.Domain.Contracts;
using Payment.Domain.Execption;
using Payment.Domain.Interfaces.Services;
using Payment.Test.Tests.Factories;
using System;
using System.Text;
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
                .SetCardNumber(GenerateCarNumberValid())
                .SetAmount(GenerateAmount())
                .SetInstallmentsNumber(new Random().Next(12))
                .Build();
        }

        private CardPaymentContract GivenAPaymentNotValid()
        {
            return new CardPaymentContractBuilder()
                .SetCardNumber(GenerateCarNumberNotValid())
                .SetAmount(GenerateAmount())
                .SetInstallmentsNumber(new Random().Next(12))
                .Build();
        }

        private decimal GenerateAmount()
        {
            Random random = new Random();
            return Math.Round(new decimal(random.Next(12, 10000) + random.NextDouble()), 2);
        }

        private long GenerateCarNumberNotValid()
        {
            Random random = new Random();

            var builder = new StringBuilder();
            while (builder.Length < 16)
            {
                builder.Append(random.Next(9).ToString());
            }

            string cardNumber = builder.ToString();

            if (cardNumber.StartsWith('0'))
            {
                cardNumber = cardNumber.Remove(0) + random.Next(1, 9).ToString();
            }

            if (cardNumber.Length < 16)
            {
                return GenerateCarNumberNotValid();
            }

            string fourLastNumber = cardNumber.Substring(12, 4);
            cardNumber = cardNumber.Replace(fourLastNumber, "5999");

            return Convert.ToInt64(cardNumber);
        }

        private long GenerateCarNumberValid()
        {
            Random random = new Random();

            var builder = new StringBuilder();
            while (builder.Length < 16)
            {
                builder.Append(random.Next(9).ToString());
            }

            string cardNumber = builder.ToString();

            if (cardNumber.StartsWith('0'))
            {
                cardNumber = cardNumber.Remove(0) + random.Next(1, 9).ToString();
            }

            if (cardNumber.Length < 16)
            {
                return GenerateCarNumberValid();
            }

            return Convert.ToInt64(cardNumber);
        }
    }
}