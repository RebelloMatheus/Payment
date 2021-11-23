using Payment.Domain.Interfaces.Services;

namespace Payment.Test.Tests.Functional.Services
{
    internal class AntecipationSeriveTest : ModelTestBase
    {
        private IAntecipationService _service;

        protected override void SetUpPayment()
        {
            _service = GetService<IAntecipationService>();
        }
    }
}