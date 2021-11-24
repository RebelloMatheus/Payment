namespace Payment.Application.WebApi.Models.ViewModel
{
    public class CardPayment
    {
        public long CardNumber { get; set; }
        public decimal Amount { get; set; }
        public int InstallmentsNumber { get; set; }

        public CardPayment()
        {
        }
    }
}