using Payment.Domain.Contracts;
using System;
using System.Text;

namespace Payment.Test.Tests.Factories
{
    internal class CardPaymentContractBuilder
    {
        private long cardNumber;
        private decimal amount;
        private int installmentsNumber;

        public CardPaymentContractBuilder()
        {
            cardNumber = 123456789012345;
            amount = 1;
            installmentsNumber = 1;
        }

        public CardPaymentContract Build()
        {
            return new CardPaymentContract(
                cardNumber: cardNumber,
                amount: amount,
                installmentsNumber: installmentsNumber);
        }

        public CardPaymentContractBuilder SetCardNumber(long cardNumber)
        {
            this.cardNumber = cardNumber;
            return this;
        }

        public CardPaymentContractBuilder SetGenerateCardNumberValid()
        {
            this.cardNumber = GenerateCarNumberValid();
            return this;
        }

        public CardPaymentContractBuilder SetAmount(decimal amount)
        {
            this.amount = amount;
            return this;
        }

        public CardPaymentContractBuilder SetInstallmentsNumber(int installmentsNumber)
        {
            this.installmentsNumber = installmentsNumber;
            return this;
        }

        public CardPaymentContractBuilder SetGenerateAmount()
        {
            this.amount = GenerateAmount();
            return this;
        }

        public CardPaymentContractBuilder SetGenerateCardNumberNotValid()
        {
            this.cardNumber = GenerateCarNumberNotValid();
            return this;
        }

        private decimal GenerateAmount()
        {
            Random random = new Random();
            return Math.Round(new decimal(random.Next(12, 10000) + random.NextDouble()), 2);
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
    }
}