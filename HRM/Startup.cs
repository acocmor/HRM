
using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;
using HRM.App_Start;
using HRM.Core.Configurations;
using HRM.Core.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Microsoft.OpenApi.Models;

namespace HRM
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<JwtConfig>(Configuration.GetSection("Jwt"));

            services.AddSharedServices(Configuration);
            services.AddDbContext(Configuration);
            services.AddAutoMapper(Configuration);
            services.AddAuthentication(Configuration);  
            
            services.AddCors(); 
            
            services.AddControllers()
                .AddNewtonsoftJson(ops =>
                {
                    //ops.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                    ops.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                })
                .AddFluentValidation( fv => {
                    // fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                    fv.RegisterValidatorsFromAssemblyContaining<Startup>(); }); 
            

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "HRM", Version = "v1"}); });
        }

 
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HRM v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            //error middleware
            app.UseErrorHandlingMiddleware();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}