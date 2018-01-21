using Microsoft.EntityFrameworkCore;
using RH.Domain.Core.Entities;
using RH.Domain.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RH.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbContext dbContext;
        private readonly DbSet<TEntity> dbSet;

        public Repository(PrincipalDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.dbSet = this.dbContext.Set<TEntity>();
        }

        public Task CommitAsync()
        {
            return this.dbContext.SaveChangesAsync();
        }

        public Task<long> CountAsync()
        {
            return this.Query().LongCountAsync();
        }

        public Task<long> CountAsync(Expression<Func<TEntity, bool>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            return this.Query().LongCountAsync(expression);
        }

        public Task DeleteAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return Task.Run(() => this.dbSet.Remove(entity));
        }

        public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            return this.Query().AnyAsync(expression);
        }

        public Task<TEntity> GetAsync(params object[] keys)
        {
            if (keys == null)
                throw new ArgumentNullException(nameof(keys));

            return this.dbSet.FindAsync(keys);
        }

        public Task InsertAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return this.dbSet.AddAsync(entity);
        }

        public Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            return this.Query().Where(expression).ToListAsync();
        }

        public Task UpdateAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return Task.Run(() => this.dbSet.Update(entity));
        }

        protected IQueryable<TEntity> QueryAsTracking()
        {
            return this.dbSet;
        }

        protected IQueryable<TEntity> Query()
        {
            return this.dbSet.AsNoTracking();
        }

        public Task<List<TEntity>> GetAllAsync()
        {
            return this.Query().ToListAsync();
        }
    }
}
