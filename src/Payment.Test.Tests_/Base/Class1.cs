using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Payment.Infra.DataBase.Context.Base;
using Payment.Infra.DataBase.Repository.Base;
using System;
using System.Collections.Generic;

namespace Payment.Test.Tests.Base
{
    public abstract class TestBase
    {
        private readonly IServiceCollection _serviceCollection;
        private IServiceScopeFactory _serviceScopeFactory;
        private Type contextEFTest;
        private DbContextOptions<ContextBase> dbContextOptions;

        protected IConfiguration config => new ConfigurationBuilder()
            .AddJsonFile("appsettings.test.json")
            .Build();

        protected IServiceScopeFactory ServiceScope
        {
            get
            {
                var context = Activator.CreateInstance(contextEFTest, dbContextOptions);
                _serviceCollection.AddScoped(_ => context);
                return _serviceScopeFactory;
            }
            private set => value = _serviceScopeFactory;
        }

        protected IRepositoryBase Repository { get; set; }
        protected IServiceProvider Service;

        protected TestBase()
        {
            _serviceCollection = new ServiceCollection();
            _serviceCollection.AddScoped(_ => config);
        }

        [SetUp]
        public void SetUpBase()
        {
            dbContextOptions = new DbContextOptionsBuilder<ContextBase>()
                .UseInMemoryDatabase(databaseName: nameof(contextEFTest))
                .Options;

            AdicionarServicos(_serviceCollection, config);

            IniciarServiceProviderEContexto();

            SetUp();
        }

        private void IniciarServiceProviderEContexto()
        {
            Service = _serviceCollection.BuildServiceProvider();

            if (contextEFTest != null)
            {
                var contexto = (ContextoBase)Service.GetService(contextEFTest);
                contexto.Database.EnsureDeleted();
                contexto.Database.EnsureCreated();

                Repository = ObterServico<IRepositorioBase>();
            }

            _serviceScopeFactory = Service.GetService<IServiceScopeFactory>();
        }

        protected abstract void AdicionarServicos(IServiceCollection servicos, IConfiguration configuracao);

        protected abstract void SetUp();

        protected void AdicionarBancoDadosTeste<TContexto>() where TContexto : ContextoBase
        {
            AdicionarBancoDadosTeste<TContexto, Guid>();
        }

        protected void AdicionarBancoDadosTeste<TContexto, TChavePrimaria>() where TContexto : ContextoBase
        {
            contextEFTest = typeof(TContexto);
            var contexto = (TContexto)Activator.CreateInstance(contextEFTest, dbContextOptions);
            _serviceCollection.AddScoped(_ => contexto);
            _serviceCollection.AddSingleton<IRepositorioBase<TChavePrimaria>, RepositorioBase<TContexto, TChavePrimaria>>();
            _serviceCollection.AddSingleton<IRepositorioBase, RepositorioBase<TContexto>>();
        }

        protected T ObterServico<T>()
        {
            return Service.GetService<T>();
        }

        protected IEnumerable<T> ObterServicos<T>()
        {
            return Service.GetServices<T>();
        }

        protected T ObterServico<T>(Type type)
        {
            return (T)Service.GetService(type);
        }

        protected void SubstituirServico<T>() where T : class
        {
            SubstituirServico<T>(Substitute.For<T>());
        }

        protected void SubstituirServico<T>(T instance)
        {
            var descriptorARemover = _serviceCollection.FirstOrDefault(x => x.ServiceType == typeof(T));
            if (descriptorARemover == null)
                return;

            _serviceCollection.Remove(descriptorARemover);

            var descriptorAAdicionar = new ServiceDescriptor(typeof(T), instance);

            _serviceCollection.Add(descriptorAAdicionar);

            IniciarServiceProviderEContexto();
        }

        [TearDown]
        public void TearDownAttribute()
        {
            TearDown();
        }

        public virtual void TearDown()
        {
        }
    }
}