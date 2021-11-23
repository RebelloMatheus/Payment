using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payment.Domain.Models;
using Payment.Infra.DataBase.EntityModel.Base;

namespace Payment.Infra.DataBase.EntityModel
{
    internal class TransactionsMap : MappingBase<Transactions>
    {
        private readonly string TableName = "Transactions";

        public override void Configure(EntityTypeBuilder<Transactions> builder)
        {
            builder.ToTable(TableName);

            builder.Property(i => i.Id).ValueGeneratedOnAdd();
            builder.Property(i => i.Amount).HasColumnType("decimal(8,2)");
            builder.Property(i => i.NetAmount).HasColumnType("decimal(8,2)");
            builder.Property(i => i.FlatRate).HasColumnType("decimal(5,2)");
            builder.Property(i => i.AntecipationStatus).HasColumnType("int");
            builder.Property(i => i.AntecipationId).IsRequired(false);

            builder.HasOne(p => p.Antecipation)
                             .WithMany(a => a.RequestedTransactions)
                             .HasForeignKey(p => p.AntecipationId)
                             .OnDelete(DeleteBehavior.Restrict)
                             .IsRequired();

            builder.HasMany(i => i.Installments)
                             .WithOne(p => p.Payment);
            base.Configure(builder);
        }
    }
}