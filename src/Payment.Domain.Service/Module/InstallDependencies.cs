using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Payment.Domain.Interfaces.Converters;
using Payment.Domain.Interfaces.Services;
using Payment.Domain.Service.Converters;
using Payment.Domain.Service.Services;
using Payment.Infra.DataBase.Context;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class InstallDependencies
    {
        public static void AddServicesPayment(this IServiceCollection services, IConfiguration configuration)
        {
            // Services.
            services.TryAddScoped<ITransactionService, TransactionService>();
            services.TryAddScoped<IAntecipationService, AntecipationService>();

            // Contracters
            services.TryAddScoped<IConvertersTransaction, ConvertersTransaction>();
            services.TryAddScoped<IConvertersInstallments, ConvertersInstallments>();
            services.TryAddScoped<IConvertersAntecipation, ConvertersAntecipation>();

            // Service dependencies.
            services.AddDataBase<PaymentContext>(configuration);

            // Configurations
            AddConfiguration(services, configuration);
        }

        private static void AddConfiguration(IServiceCollection services, IConfiguration configuration)
        {
        }
    }
}