using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Persistince.Contexts;
using ECommerceAPI.Persistince.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistince
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services)
        {
            //sadece DbContext eklemek yetmez appsetting.json daki ConnectionStrings'e de ulaşmam gerkiyor. Sonucta buraya ulaşmazsam DbContex'e ulaşırım ama aderesi veremem.

            ServiceProvider provider = services.BuildServiceProvider();

            //appsetting.json'a ulaşmak için gerekli configuration ayarlarını istiyorum :
            IConfiguration configuration = provider.GetService<IConfiguration>();

            services.AddDbContext<EcommerceApiDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlConnection")), ServiceLifetime.Singleton);

            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();

            return services;
        }
    }
}
