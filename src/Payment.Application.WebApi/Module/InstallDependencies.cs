using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Payment.Application.WebApi.Converters;
using Payment.Application.WebApi.Converters.Interfaces;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class InstallDependencies
    {
        public static void AddServicesApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRequisitions(configuration);
            services.AddConvertersViewModel();
        }

        public static void AddConvertersViewModel(this IServiceCollection services)
        {
            services.TryAddScoped<ICardPaymentConverter, CardPaymentConverter>();
            services.TryAddScoped<IConvertersAntecipation, ConvertersAntecipation>();
            services.TryAddScoped<IConvertersTransaction, ConvertersTransaction>();
            services.TryAddScoped<IConvertersModifyStatusAntecipation, ConvertersModifyStatusAntecipation>();
        }
    }
}