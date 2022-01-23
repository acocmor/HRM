using HRM.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HRM.Core.Configurations
{
    public static class ConfigurationDbContext
    {
        public static void AddDbContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("HRM"), b => b.MigrationsAssembly("HRM"));
            });
        }
    }
}