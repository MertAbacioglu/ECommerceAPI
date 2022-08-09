using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Domain.Entities.Common;
using ECommerceAPI.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace ECommerceAPI.Persistince.Contexts
{
    public class EcommerceApiDbContext : DbContext
    {
        public EcommerceApiDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            IEnumerable<EntityEntry<BaseEntity>> datas = ChangeTracker.Entries<BaseEntity>();

            foreach (EntityEntry<BaseEntity> data in datas)
            {
                switch (data.State)
                {
                    case EntityState.Added:
                        {
                            data.Entity.CreatedDate = DateTime.UtcNow;
                            data.Entity.Status = DataStatus.Inserted;
                            break;
                        }
                    case EntityState.Modified:
                        {
                            if (data.Entity.Status == DataStatus.Deleted)
                            {

                                data.Entity.DeletedDate = DateTime.UtcNow;
                                break;
                            }
                            data.Entity.ModifiedDate = DateTime.Now;
                            data.Entity.Status = DataStatus.Updated;
                            break;
                        }
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

    }



}
