using System;
using System.Net;

namespace Payment.Domain.Execption
{
    public class PaymentException : Exception
    {
        public HttpStatusCode StatusCodigo { get; set; }
        public string Uri { get; set; }

        public PaymentException()
        {
        }

        public PaymentException(string message)
            : base(message)
        {
        }

        public PaymentException(string message, HttpStatusCode statusCodigo, string uri)
           : base(message)
        {
            StatusCodigo = statusCodigo;
            Uri = uri;
        }

        public PaymentException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}