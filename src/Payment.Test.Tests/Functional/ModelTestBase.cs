using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Payment.Infra.DataBase.Context;
using Payment.Test.Tests.Base;

namespace Payment.Test.Tests.Functional
{
    public abstract class ModelTestBase : TestBase
    {
        protected override void AddService(IServiceCollection services, IConfiguration configuration)
        {
            AddDataBaseTest<PaymentContext>();
            services.AddServicesPayment(configuration);
            services.AddLogging();

            AddServicesMoks(services, configuration);
        }

        protected override void SetUp()
        {
            SetUpPayment();
        }

        protected abstract void SetUpPayment();

        protected TService GetServiceMoks<TService>()
            where TService : class
        {
            TService service = Substitute.For<TService>();
            return service;
        }

        protected virtual void AddServicesMoks(IServiceCollection services, IConfiguration configuration)
        {
        }
    }
}