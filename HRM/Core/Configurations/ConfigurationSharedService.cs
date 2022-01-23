using HRM.Core.Interfaces;
using HRM.Core.AppServices;
using HRM.Entity.Constracts;
using HRM.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HRM.Core.Configurations
{
    public static class ConfigurationSharedService
    {
        public static void AddSharedServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IUserService, UserManager>();
            services.AddTransient<IEmployeeService, EmployeeManager>();
            services.AddTransient<IAddressService, AddressManager>();
            services.AddTransient<IGenderService, GenderManager>();
            services.AddTransient<IPositionService, PositionManager>();
            services.AddTransient<IDepartmentService, DepartmentManager>();
            
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IAddressRepository, AddressRepository>();
            services.AddTransient<IGenderRepository, GenderRepository>();
            services.AddTransient<IPositionRepository, PositionRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
        }
    }
}