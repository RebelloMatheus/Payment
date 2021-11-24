namespace Payment.Domain.Models
{
    public static class SR
    {
        public const string TRANSACTION_RECUSED_INVALID_CARD = "Transaction declined, card invalid.";
        public const string PAYMENT_BLOCKED_APPROVED = "Payment has already been approved and cannot be changed.";
    }
}