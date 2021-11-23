using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payment.Domain.Models;
using Payment.Infra.DataBase.EntityModel.Base;

namespace Payment.Infra.DataBase.EntityModel
{
    internal class AntecipationsMap : MappingBase<Antecipations>
    {
        private readonly string TableName = "Antecipations";

        public override void Configure(EntityTypeBuilder<Antecipations> builder)
        {
            builder.ToTable(nameof(Antecipations));

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id).ValueGeneratedOnAdd();
            builder.Property(i => i.RequestedAmount).HasColumnType("decimal(8,2)");
            builder.Property(i => i.GrantedAmount).HasColumnType("decimal(8,2)");
            builder.Property(i => i.AnalysisResult).HasColumnType("int");

            builder.HasMany(a => a.RequestedTransactions)
                             .WithOne(p => p.Antecipation);
            base.Configure(builder);
        }
    }
}