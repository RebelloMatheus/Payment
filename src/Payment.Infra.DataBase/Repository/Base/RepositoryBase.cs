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
    public class RepositoryBase<TContextType, TPrimaryKey> : IRepositoryBase<TPrimaryKey>, IDisposable
        where TContextType : ContextBase
    {
        protected readonly TContextType _context;

        public RepositoryBase(TContextType context)
        {
            _context = context;
        }

        public async Task AddAsync<TEntityType>(TEntityType entity) where TEntityType : EntityBase<TPrimaryKey>
        {
            await _context.Set<TEntityType>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<TEntityType> BuildQueryAsync<TEntityType>() where TEntityType : EntityBase<TPrimaryKey>
            => _context.Set<TEntityType>();

        public async Task<int> CountAllAsync<TEntityType>() where TEntityType : EntityBase<TPrimaryKey>
            => await _context.Set<TEntityType>().CountAsync();

        public async Task<TEntityType> FirtOrDefaultAsync<TEntityType>(Expression<Func<TEntityType, bool>> condition) where TEntityType : EntityBase<TPrimaryKey>
            => await _context.Set<TEntityType>().FirstOrDefaultAsync(condition);

        public async Task<IList<TEntityType>> GetAllAsync<TEntityType>() where TEntityType : EntityBase<TPrimaryKey>
            => await _context.Set<TEntityType>().ToListAsync();

        public async Task<IEnumerable<TEntityType>> GetAllAsync<TEntityType>(Expression<Func<TEntityType, bool>> condition) where TEntityType : EntityBase<TPrimaryKey>
            => await _context.Set<TEntityType>().Where(condition).ToListAsync();

        public async Task<IList<TEntityType>> GetAllListAsync<TEntityType>(Expression<Func<TEntityType, bool>> condition) where TEntityType : EntityBase<TPrimaryKey>
            => await _context.Set<TEntityType>().Where(condition).ToListAsync();

        public async Task<TEntityType> GetIdAsync<TEntityType>(TPrimaryKey id)
            where TEntityType : EntityBase<TPrimaryKey>
            => await _context.Set<TEntityType>().FindAsync(id);

        public async Task RemoveAsync<TEntityType>(TEntityType entity) where TEntityType : EntityBase<TPrimaryKey>
        {
            _context.Set<TEntityType>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveIdAsync<TEntityType>(TPrimaryKey id) where TEntityType : EntityBase<TPrimaryKey>
        {
            var entity = await GetIdAsync<TEntityType>(id).ConfigureAwait(false);
            if (entity != null)
            {
                _context.Set<TEntityType>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> TellWhenAsync<TEntityType>(Expression<Func<TEntityType, bool>> predicate) where TEntityType : EntityBase<TPrimaryKey>
            => await _context.Set<TEntityType>().CountAsync(predicate);

        public async Task UpdateAsync<TEntityType>(TEntityType entity) where TEntityType : EntityBase<TPrimaryKey>
        {
            _context.Set<TEntityType>().Update(entity);
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