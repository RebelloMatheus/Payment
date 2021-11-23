using System;
using System.Text;

namespace Payment.Test.Tests.Factories
{
    public class CardPaymentIsValidData
    {
        public long CardNumber
        {
            get
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

                return Convert.ToInt64(cardNumber);
            }
        }

        public decimal Amount
        {
            get
            {
                Random random = new Random();
                return Math.Round(new decimal(random.Next(12, 10000) + random.NextDouble()), 2);
            }
        }

        public int InstallmentsNumber
        {
            get
            {
                return new Random().Next(12);
            }
        }
    }
}