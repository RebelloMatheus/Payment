using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Payment.Infra.DataBase.Context.Base;
using Payment.Infra.DataBase.Repository.Base;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class InstallDependencies
    {
        public const string DatabaseConnectionString = "DatabaseConnectionString";

        public static void AddDataBase<ContextType, PrimaryKey>(this IServiceCollection servicos, IConfiguration configuration)
            where ContextType : ContextBase
        {
            servicos.AddDbContext<ContextType>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            servicos.AddScoped<IRepositoryBase<PrimaryKey>, RepositoryBase<ContextType, PrimaryKey>>();
        }

        public static void AddDataBase<ContextType>(this IServiceCollection servicos, IConfiguration configuration)
            where ContextType : ContextBase
        {
            servicos.AddDbContext<ContextType>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            servicos.AddScoped<IRepositoryBase, RepositoryBase<ContextType>>();
        }
    }
}