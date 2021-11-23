namespace Payment.Domain.Contracts
{
    public class CardPaymentContract
    {
        public long CardNumber { get; }
        public decimal Amount { get; }
        public int InstallmentsNumber { get; }

        public CardPaymentContract(
            long cardNumber,
            decimal amount,
            int installmentsNumber)
        {
            CardNumber = cardNumber;
            Amount = amount;
            InstallmentsNumber = installmentsNumber;
        }
    }
}