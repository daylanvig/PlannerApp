using Application.Interfaces;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Persistence.Common
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly IPlannerContext context;
        protected readonly string tenantID;
        protected readonly DbSet<T> items;
        public Repository(IPlannerContext context, ITenantService tenantService)
        {
            this.context = context;
            tenantID = tenantService.GetTenantID();
            items = context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return items;
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await items.FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await items.AsQueryable().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> whereCondition)
        {
            return await items.Where(whereCondition).ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await items.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            items.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            items.Remove(entity);
            await context.SaveChangesAsync();
        }
        // todo remove
        public async Task<T> FirstAsync(Expression<Func<T, bool>> whereCondition)
        {
            return await items.FirstAsync(whereCondition);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> whereCondition)
        {
            return await items.FirstOrDefaultAsync(whereCondition);
        }

    }
}
