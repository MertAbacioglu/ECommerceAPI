using ECommerceAPI.Application.UnitOfWork;
using ECommerceAPI.Persistince.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistince.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EcommerceApiDbContext _ecommerceApiDbContext;

        public UnitOfWork(EcommerceApiDbContext ecommerceApiDbContext)
        {
            _ecommerceApiDbContext = ecommerceApiDbContext;
        }

        public void Commit()
        {
            _ecommerceApiDbContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _ecommerceApiDbContext.SaveChangesAsync();
        }
    }
}
