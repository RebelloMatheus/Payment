using Microsoft.EntityFrameworkCore;
using Payment.Infra.DataBase.Context.Base;

namespace Payment.Infra.DataBase.Context
{
    public class PaymentContext : ContextBase
    {
        public override string ContextName => nameof(PaymentContext);

        public PaymentContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLazyLoadingProxies()
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging();

                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PaymentContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}