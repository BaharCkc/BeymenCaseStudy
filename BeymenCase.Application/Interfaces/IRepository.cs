using BeymenCase.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BeymenCase.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetByNameAsync<TName>(TName name) where TName : notnull;
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(params Expression<Func<T, object>>[] including);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task RemoveRangeAsync(List<T> entity);
    }
}
