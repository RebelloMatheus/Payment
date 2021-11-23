using Payment.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Payment.Infra.DataBase.Repository.Base
{
    public interface IRepositoryBase<PrimaryKey>
    {
        IQueryable<EntityType> BuildQueryAsync<EntityType>()
            where EntityType : EntityBase<PrimaryKey>;

        Task<IList<EntityType>> GetAllAsync<EntityType>()
            where EntityType : EntityBase<PrimaryKey>;

        Task<EntityType> GetIdAsync<EntityType>(PrimaryKey id)
            where EntityType : EntityBase<PrimaryKey>;

        Task<IEnumerable<EntityType>> GetAllAsync<EntityType>(Expression<Func<EntityType, bool>> condition)
            where EntityType : EntityBase<PrimaryKey>;

        Task<IList<EntityType>> GetAllListAsync<EntityType>(Expression<Func<EntityType, bool>> condition)
            where EntityType : EntityBase<PrimaryKey>;

        Task<EntityType> FirtOrDefaultAsync<EntityType>(Expression<Func<EntityType, bool>> condition)
            where EntityType : EntityBase<PrimaryKey>;

        Task AddAsync<EntityType>(EntityType entity)
            where EntityType : EntityBase<PrimaryKey>;

        Task UpdateAsync<EntityType>(EntityType entity)
            where EntityType : EntityBase<PrimaryKey>;

        Task RemoveAsync<EntityType>(EntityType entity)
            where EntityType : EntityBase<PrimaryKey>;

        Task RemoveIdAsync<EntityType>(PrimaryKey id)
            where EntityType : EntityBase<PrimaryKey>;

        Task<int> CountAllAsync<EntityType>()
            where EntityType : EntityBase<PrimaryKey>;

        Task<int> TellWhenAsync<EntityType>(Expression<Func<EntityType, bool>> predicate)
            where EntityType : EntityBase<PrimaryKey>;
    }

    public interface IRepositoryBase : IRepositoryBase<int>
    {
    }
}