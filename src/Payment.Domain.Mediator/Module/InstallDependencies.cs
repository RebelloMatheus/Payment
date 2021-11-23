using MediatR;
using Microsoft.Extensions.Configuration;
using Payment.Domain.Mediator.Mediators.Handlers;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class InstallDependencies
    {
        public static void AdicionarRequisicoes(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(typeof(TransactionGetIdHandler).Assembly);
            services.AddServicesPayment(configuration);
        }
    }
}