using Payment.Domain.Contracts;

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
    }
}