using System.Text;
using HRM.API.Core.MappingProfiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace HRM.Core.Configurations
{
    // public static class ConfigurationJWT
    // {
    //     public static void AddJWT(this IServiceCollection services, IConfiguration config)
    //     {
    //         // //setup JWT
    //         // services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    //         //     .AddJwtBearer(options =>
    //         //     {
    //         //         options.TokenValidationParameters = new TokenValidationParameters
    //         //         {
    //         //             ValidateIssuer = true,
    //         //             ValidateAudience = true,
    //         //             ValidateLifetime = true,
    //         //             ValidateIssuerSigningKey = true,
    //         //             ValidIssuer = Configuration["Jwt:Issuer"],
    //         //             ValidAudience = Configuration["Jwt:Audience"],
    //         //             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
    //         //         };
    //         //     });
    //     }
    // }
}