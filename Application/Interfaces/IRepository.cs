using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        IQueryable<T> GetAll();
        Task<T> AddAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T> FirstAsync(Expression<Func<T, bool>> whereCondition);
        System.Threading.Tasks.Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> whereCondition);
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> whereCondition);
        Task UpdateAsync(T entity);
    }
}
