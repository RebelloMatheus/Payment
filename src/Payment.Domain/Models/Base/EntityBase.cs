using Payment.Domain.Interfaces.Models;

namespace Payment.Domain.Models.Base
{
    public abstract class EntityBase<PrimaryKey> : IEntityBase<PrimaryKey>
    {
        public PrimaryKey Id { get; protected set; }

        //public override string ToString()
        //{
        //    return $"{this.GetType().Name}#{Id}";
        //}
    }

    public abstract class EntityBase : EntityBase<int>, IEntityBase
    {
    }
}