using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using Payment.Infra.DataBase.Context.Base;
using Payment.Infra.DataBase.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;

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

            AddService(_serviceCollection, config);

            StartServiceProviderContext();

            SetUp();
        }

        private void StartServiceProviderContext()
        {
            Service = _serviceCollection.BuildServiceProvider();

            if (contextEFTest != null)
            {
                var context = (ContextBase)Service.GetService(contextEFTest);
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository = GetService<IRepositoryBase>();
            }

            _serviceScopeFactory = Service.GetService<IServiceScopeFactory>();
        }

        protected abstract void AddService(IServiceCollection services, IConfiguration configuration);

        protected abstract void SetUp();

        protected void AddDataBaseTest<TContext>() where TContext : ContextBase
        {
            AddDataBaseTest<TContext, Guid>();
        }

        protected void AddDataBaseTest<TContext, TPrimaryKey>() where TContext : ContextBase
        {
            contextEFTest = typeof(TContext);
            var context = (TContext)Activator.CreateInstance(contextEFTest, dbContextOptions);
            _serviceCollection.AddScoped(_ => context);
            _serviceCollection.AddSingleton<IRepositoryBase<TPrimaryKey>, RepositoryBase<TContext, TPrimaryKey>>();
            _serviceCollection.AddSingleton<IRepositoryBase, RepositoryBase<TContext>>();
        }

        protected T GetService<T>()
        {
            return Service.GetService<T>();
        }

        protected IEnumerable<T> GetServices<T>()
        {
            return Service.GetServices<T>();
        }

        protected T GetService<T>(Type type)
        {
            return (T)Service.GetService(type);
        }

        protected void ReplaceService<T>() where T : class
        {
            ReplaceService<T>(Substitute.For<T>());
        }

        protected void ReplaceService<T>(T instance)
        {
            var descriptorRemove = _serviceCollection.FirstOrDefault(x => x.ServiceType == typeof(T));
            if (descriptorRemove == null)
                return;

            _serviceCollection.Remove(descriptorRemove);

            var descriptorAdd = new ServiceDescriptor(typeof(T), instance);

            _serviceCollection.Add(descriptorAdd);

            StartServiceProviderContext();
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