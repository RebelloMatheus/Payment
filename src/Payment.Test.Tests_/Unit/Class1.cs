using Castle.Core.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Payment.Infra.DataBase.Context;
using Payment.Test.Tests.Base;

namespace Payment.Test.Tests.Unit
{
    public abstract class ModeloTesteBase : TestBase
    {
        protected override void AdicionarServicos(IServiceCollection services, IConfiguration configuration)
        {
            AdicionarBancoDadosTeste<PaymentContext>();
            services.AdicionarServicos(configuration);
            services.AddLogging();

            AdicionarServicosMocados(services, configuration);
        }

        protected override void SetUp()
        {
            SetUpAMcomUbs();
        }

        protected abstract void SetUpAMcomUbs();

        protected TServico ObterServicoMocado<TServico>()
            where TServico : class
        {
            TServico servico = Substitute.For<TServico>();
            return servico;
        }

        protected virtual void AdicionarServicosMocados(IServiceCollection servicos, IConfiguration configuracao)
        {
        }
    }
}