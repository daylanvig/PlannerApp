using Microsoft.EntityFrameworkCore;
using PlannerApp.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PlannerApp.Server.Data
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly PlannerContext context;

        public Repository(PlannerContext context)
        {
            this.context = context;
        }
        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> whereCondition)
        {
            return await context.Set<T>().Where(whereCondition).ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<T> FirstAsync(Expression<Func<T, bool>> whereCondition)
        {
            return await context.Set<T>().FirstAsync(whereCondition);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> whereCondition)
        {
            return await context.Set<T>().FirstOrDefaultAsync(whereCondition);
        }

    }
}
