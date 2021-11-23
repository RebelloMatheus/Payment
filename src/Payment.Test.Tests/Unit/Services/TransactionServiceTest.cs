using FluentAssertions;
using NUnit.Framework;
using Payment.Domain.Contracts;
using Payment.Domain.Execption;
using Payment.Domain.Interfaces.Services;
using Payment.Domain.Models;
using Payment.Infra.DataBase.Repository.Base;
using Payment.Test.Tests.Factories;
using Payment.Test.Tests.Functional;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Test.Tests.Unit.Services
{
    internal class TransactionServiceTest : ModelTestBase
    {
        private ITransactionService _service;
        private IRepositoryBase _repositoryBase;
        private decimal _flatRate = 0.9m;

        protected override void SetUpPayment()
        {
            _service = GetService<ITransactionService>();
            _repositoryBase = GetService<IRepositoryBase>();
        }

        [Test]
        public async Task ProcessTransactionIsValid()
        {
            var cardPayment = GivenAPaymentValid();

            var ret = await _service.ProcessAsync(cardPayment).ConfigureAwait(false);

            Assert.IsNotNull(ret);
            ret.Amount.Should().Be(cardPayment.Amount);
            ret.NetAmount.Should().Be(CalcChargeFlarRate(cardPayment));
            ret.InstallmentsNumber.Should().Be(cardPayment.InstallmentsNumber);
            ret.FourLastCardNumber.Should().Be(GetFourLastCardNumber(cardPayment.CardNumber));
        }

        [TestCase(1)]
        [TestCase(10)]
        [TestCase(100)]
        public async Task ProcessTransactionIsRangeValid(int rangeTest)
        {
            for (int i = 0; i < rangeTest; i++)
            {
                var cardPayment = GivenAPaymentValid();

                var ret = await _service.ProcessAsync(cardPayment).ConfigureAwait(false);
            }

            var transactions = await _repositoryBase.GetAllAsync<Transactions>();

            transactions.Count.Should().Be(rangeTest);
        }

        [TestCase(1)]
        [TestCase(10)]
        [TestCase(100)]
        public async Task InstallmentCalcIsValid(int rangeTest)
        {
            for (int i = 0; i < rangeTest; i++)
            {
                var cardPayment = GivenAPaymentValid();

                await _service.ProcessAsync(cardPayment).ConfigureAwait(false);
            }

            var transactions = await _repositoryBase.GetAllAsync<Transactions>();

            foreach (var transaction in transactions)
            {
                decimal amountPerInstallment = Math.Round(transaction.Amount / transaction.InstallmentsNumber, 2);
                decimal netAmountPerInstallment = Math.Round(transaction.NetAmount / transaction.InstallmentsNumber, 2);
                foreach (var installment in transaction.Installments)
                {
                    var dayLast = DateTime.DaysInMonth(installment.ExpectedPaymentDate.Year, installment.ExpectedPaymentDate.Month);
                    if (dayLast < 30)
                        installment.ExpectedPaymentDate.Day.Should().Be(dayLast);
                    else
                        installment.ExpectedPaymentDate.Day.Should().Be(30);

                    installment.Amount.Should().Be(amountPerInstallment);
                    installment.NetAmount.Should().Be(netAmountPerInstallment);
                }
            }
        }

        [Test]
        public async Task ProcessTransactionIsNotValid()
        {
            var cardPayment = GivenAPaymentNotValid();

            Func<Task> ret = async () => await _service.ProcessAsync(cardPayment);

            ret.Should().Throw<PaymentException>();
            var transactions = await _repositoryBase.GetAllAsync<Transactions>();
            var transaction = transactions.First();
            transaction.Installments.Should().BeNull();
            transaction.ApprovedDate.Should().BeNull();
            transaction.NotApprovedDate.Should().BeCloseTo(DateTime.Now, precision: 10000);
        }

        private string GetFourLastCardNumber(long cardNumber)
        {
            string cardNumberString = cardNumber.ToString();
            return cardNumberString.Length != 16 ? null : cardNumberString.Substring(12, 4);
        }

        private decimal CalcChargeFlarRate(CardPaymentContract cardPayment)
        {
            return cardPayment.Amount - _flatRate;
        }

        private CardPaymentContract GivenAPaymentValid()
        {
            var installmentsNumber = new Random().Next(12);
            if (installmentsNumber == 0)
                installmentsNumber = 1;
            return new CardPaymentContractBuilder()
                .SetCardNumber(GenerateCarNumberValid())
                .SetAmount(GenerateAmount())
                .SetInstallmentsNumber(installmentsNumber)
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
                return GenerateCarNumberValid();
            return Convert.ToInt64(cardNumber);
        }
    }
}