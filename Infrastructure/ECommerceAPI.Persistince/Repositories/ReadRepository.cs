using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Common;
using ECommerceAPI.Persistince.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistince.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly EcommerceApiDbContext _context;

        public ReadRepository(EcommerceApiDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll(bool tracking = true)
        {
            IQueryable<T> query = Table.AsQueryable();
            if (!tracking == true)
                query=query.AsNoTracking();
            return query;


        }

        public async Task<T> GetByIdAsync(string id, bool tracking = true)
        {
            IQueryable<T> query = Table.AsQueryable();
            if (!tracking == true)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(x=>x.Id==Guid.Parse(id));

        }
        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, bool tracking = true)
        {
            IQueryable<T> query = Table.AsQueryable();
            if (!tracking == true)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(expression);
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> expression, bool tracking = true)
        {
            IQueryable<T> query =Table.Where(expression);
            if (!tracking == true)
                query = query.AsQueryable();
            return query;
        }
    }
}
