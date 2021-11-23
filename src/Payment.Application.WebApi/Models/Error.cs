using System.Net;

namespace Payment.Application.WebApi.Models
{
    public class Error
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public string Uri { get; set; }
    }
}