using BeymenCase.Application.Repositories;
using BeymenCase.Infrastructure.Persistence;
using BeymenCase.Library.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BeymenCase.Library.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly BeymenDbContext _dbContext;

        public Repository(BeymenDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual IQueryable<T> GetAll()
        {
            return _dbContext.Set<T>().AsNoTracking();
        }

        public virtual IQueryable<T> GetAll(params Expression<Func<T, object>>[] including)
        {
            var query = _dbContext.Set<T>().AsNoTracking();
            if (including != null)
                including.ToList().ForEach(include =>
                {
                    if (include != null)
                        query = query.Include(include);
                });
            return query;
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);

            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(T entity)
        {
            entity.IsActive = false;

            _dbContext.Entry(entity).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task RemoveRangeAsync(List<T> entity)
        {
            foreach (var item in entity)
            {
                item.IsActive = false;

                _dbContext.Entry(item).State = EntityState.Modified;
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T> GetByNameAsync<TName>(TName name) where TName : notnull
        {
            return await _dbContext.Set<T>().FindAsync(name);
        }
    }
}
