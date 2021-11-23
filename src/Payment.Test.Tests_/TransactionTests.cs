using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Payment.Application.WebApi;
using Payment.Application.WebApi.Models;
using Payment.Test.Tests.Factories;
using Payment.Test.Tests.Factories.ResultModel;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Payment.Test.Tests
{
    public class TransactionTests : IDisposable
    {
        private readonly TestServer _testServer;
        private readonly HttpClient _client;

        public TransactionTests()
        {
            var webBuilder = new WebHostBuilder()
            .UseStartup<Startup>();

            _testServer = new TestServer(webBuilder);
            _client = _testServer.CreateClient();
        }

        [Fact]
        public async Task ProcessTransactionIsValid()
        {
            CardPaymentIsValidData cardPaymentData = new CardPaymentIsValidData();

            HttpContent httpContent = new StringContent(JsonSerializer.Serialize(cardPaymentData));
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _client.PostAsync("/api/v2/transaction/process", httpContent);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            TransactionResult transactionResult = null;

            if (!string.IsNullOrEmpty(responseString))
            {
                transactionResult = JsonSerializer.Deserialize<TransactionResult>(responseString);
            }

            Assert.True(transactionResult != null);
        }

        [Fact]
        public async Task ProcessTransactionNotValid()
        {
            CardPaymentNotValidData cardPaymentData = new CardPaymentNotValidData();

            HttpContent httpContent = new StringContent(JsonSerializer.Serialize(cardPaymentData));
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _client.PostAsync("/api/v2/transaction/process", httpContent);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            Error error = null;

            if (!string.IsNullOrEmpty(responseString))
            {
                error = JsonSerializer.Deserialize<Error>(responseString);
            }

            Assert.True(error != null);
        }

        public void Dispose()
        {
            _testServer.Dispose();
        }
    }
}