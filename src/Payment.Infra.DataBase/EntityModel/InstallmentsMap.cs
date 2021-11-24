using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payment.Domain.Models;
using Payment.Infra.DataBase.EntityModel.Base;

namespace Payment.Infra.DataBase.EntityModel
{
    internal class InstallmentsMap : MappingBase<Installments>
    {
        private readonly string TableName = "Installments";

        public override void Configure(EntityTypeBuilder<Installments> builder)
        {
            builder.ToTable(nameof(Installments));

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id).ValueGeneratedOnAdd();
            builder.Property(i => i.Amount).HasColumnType("decimal(8,2)");
            builder.Property(i => i.NetAmount).HasColumnType("decimal(8,2)");
            builder.Property(i => i.AntecipationValue).HasColumnType("decimal(8,2)");

            builder.HasOne(i => i.Payment)
                             .WithMany(p => p.Installments)
                             .HasForeignKey(i => i.PaymentId)
                             .OnDelete(DeleteBehavior.Restrict)
                             .IsRequired();
            base.Configure(builder);
        }
    }
}