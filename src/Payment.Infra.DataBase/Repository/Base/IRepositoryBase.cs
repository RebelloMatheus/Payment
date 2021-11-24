using Payment.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Payment.Infra.DataBase.Repository.Base
{
    public interface IRepositoryBase<TPrimaryKey>
    {
        IQueryable<EntityType> BuildQueryAsync<EntityType>()
            where EntityType : EntityBase<TPrimaryKey>;

        Task<IList<EntityType>> GetAllAsync<EntityType>()
            where EntityType : EntityBase<TPrimaryKey>;

        Task<EntityType> GetIdAsync<EntityType>(TPrimaryKey id)
            where EntityType : EntityBase<TPrimaryKey>;

        Task<IEnumerable<TEntityType>> GetAllAsync<TEntityType>(Expression<Func<TEntityType, bool>> condition)
            where TEntityType : EntityBase<TPrimaryKey>;

        Task<IList<TEntityType>> GetAllListAsync<TEntityType>(Expression<Func<TEntityType, bool>> condition)
            where TEntityType : EntityBase<TPrimaryKey>;

        Task<TEntityType> FirtOrDefaultAsync<TEntityType>(Expression<Func<TEntityType, bool>> condition)
            where TEntityType : EntityBase<TPrimaryKey>;

        Task AddAsync<TEntityType>(TEntityType entity)
            where TEntityType : EntityBase<TPrimaryKey>;

        Task UpdateAsync<TEntityType>(TEntityType entity)
            where TEntityType : EntityBase<TPrimaryKey>;

        Task RemoveAsync<TEntityType>(TEntityType entity)
            where TEntityType : EntityBase<TPrimaryKey>;

        Task RemoveIdAsync<TEntityType>(TPrimaryKey id)
            where TEntityType : EntityBase<TPrimaryKey>;

        Task<int> CountAllAsync<TEntityType>()
            where TEntityType : EntityBase<TPrimaryKey>;

        Task<int> TellWhenAsync<TEntityType>(Expression<Func<TEntityType, bool>> predicate)
            where TEntityType : EntityBase<TPrimaryKey>;
    }

    public interface IRepositoryBase : IRepositoryBase<int>
    {
    }
}