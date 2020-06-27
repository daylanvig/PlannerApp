using PlannerApp.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PlannerApp.Server.Data
{
    public interface IRepository<T> where T : Entity
    {
        Task<T> AddAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T> FirstAsync(Expression<Func<T, bool>> whereCondition);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> whereCondition);
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> whereCondition);
        Task UpdateAsync(T entity);
    }
}