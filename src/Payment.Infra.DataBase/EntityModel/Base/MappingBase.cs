using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payment.Domain.Models.Base;

namespace Payment.Infra.DataBase.EntityModel.Base
{
    public class MappingBase<TypeEntity, Tipo>
        : IEntityTypeConfiguration<TypeEntity> where TypeEntity : EntityBase<Tipo>
    {
        private string TableName => typeof(TypeEntity).Name;

        public virtual void Configure(EntityTypeBuilder<TypeEntity> builder)
        {
            builder.ToTable(TableName);

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Id)
                .HasName($"IX_{TableName}_Id");
        }
    }

    public class MappingBase<TypeEntity> : IEntityTypeConfiguration<TypeEntity> where TypeEntity : EntityBase
    {
        private string TableName => typeof(TypeEntity).Name;

        public virtual void Configure(EntityTypeBuilder<TypeEntity> builder)
        {
            builder.ToTable(TableName);

            builder.HasKey(x => x.Id)
                .HasName($"IX_{TableName}_Id");
        }
    }
}