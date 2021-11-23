using Microsoft.EntityFrameworkCore;
using Payment.Domain.Models.Base;
using Payment.Infra.DataBase.Context.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Payment.Infra.DataBase.Repository.Base
{
    public class RepositoryBase<ContextType, PrimaryKey> : IRepositoryBase<PrimaryKey>, IDisposable
        where ContextType : ContextBase
    {
        protected readonly ContextType _context;

        public RepositoryBase(ContextType context)
        {
            _context = context;
        }

        public async Task AddAsync<EntityType>(EntityType entity) where EntityType : EntityBase<PrimaryKey>
        {
            await _context.Set<EntityType>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<EntityType> BuildQueryAsync<EntityType>() where EntityType : EntityBase<PrimaryKey>
            => _context.Set<EntityType>();

        public async Task<int> CountAllAsync<EntityType>() where EntityType : EntityBase<PrimaryKey>
            => await _context.Set<EntityType>().CountAsync();

        public async Task<EntityType> FirtOrDefaultAsync<EntityType>(Expression<Func<EntityType, bool>> condition) where EntityType : EntityBase<PrimaryKey>
            => await _context.Set<EntityType>().FirstOrDefaultAsync(condition);

        public async Task<IList<EntityType>> GetAllAsync<EntityType>() where EntityType : EntityBase<PrimaryKey>
            => await _context.Set<EntityType>().ToListAsync();

        public async Task<IEnumerable<EntityType>> GetAllAsync<EntityType>(Expression<Func<EntityType, bool>> condition) where EntityType : EntityBase<PrimaryKey>
            => await _context.Set<EntityType>().Where(condition).ToListAsync();

        public async Task<IList<EntityType>> GetAllListAsync<EntityType>(Expression<Func<EntityType, bool>> condition) where EntityType : EntityBase<PrimaryKey>
            => await _context.Set<EntityType>().Where(condition).ToListAsync();

        public async Task<EntityType> GetIdAsync<EntityType>(PrimaryKey id)
            where EntityType : EntityBase<PrimaryKey>
            => await _context.Set<EntityType>().FindAsync(id);

        public async Task RemoveAsync<EntityType>(EntityType entity) where EntityType : EntityBase<PrimaryKey>
        {
            _context.Set<EntityType>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveIdAsync<EntityType>(PrimaryKey id) where EntityType : EntityBase<PrimaryKey>
        {
            var entity = await GetIdAsync<EntityType>(id).ConfigureAwait(false);
            if (entity != null)
            {
                _context.Set<EntityType>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> TellWhenAsync<EntityType>(Expression<Func<EntityType, bool>> predicate) where EntityType : EntityBase<PrimaryKey>
            => await _context.Set<EntityType>().CountAsync(predicate);

        public async Task UpdateAsync<EntityType>(EntityType entity) where EntityType : EntityBase<PrimaryKey>
        {
            _context.Set<EntityType>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
        }
    }

    public class RepositoryBase<ContextType> : RepositoryBase<ContextType, int>, IRepositoryBase
        where ContextType : ContextBase
    {
        public RepositoryBase(ContextType context) : base(context)
        {
        }
    }
}