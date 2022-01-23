using HRM.API.Core.MappingProfiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HRM.Core.Configurations
{
    public static class ConfigurationAutoMapper
    {
        public static void AddAutoMapper(this IServiceCollection services, IConfiguration config)
        {
            // AutoMapper settings
            services.AddAutoMapper(typeof(MappingProfile));
        }
    }
}