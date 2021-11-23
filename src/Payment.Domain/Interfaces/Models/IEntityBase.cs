namespace Payment.Domain.Interfaces.Models
{
    public interface IEntityBase<TPrimaryKey>
    {
        TPrimaryKey Id { get; }
    }

    public interface IEntityBase : IEntityBase<int>
    {
    }
}