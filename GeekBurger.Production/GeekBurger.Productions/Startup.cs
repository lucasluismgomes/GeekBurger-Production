using AutoMapper;
using GeekBurger.Productions.Extension;
using GeekBurger.Productions.Repository;
using GeekBurger.Productions.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace GeekBurger.Productions
{
    public class Startup
    {
        public static IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var mvcCoreBuilder = services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Production", Version = "v1" });
            });

            services.AddAutoMapper();

            services.AddDbContext<ProductionsContext>(o => o.UseInMemoryDatabase("geekburger-production"));
            services.AddScoped<IProductionAreaRepository, ProductionAreaRepository>();
            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddSingleton<IProductionAreaChangedService, ProductionAreaChangedService>();
            services.AddSingleton<ILogService, LogService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ProductionsContext productionsContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "GeekBurger Production API V1");
            });

            productionsContext.Seed();
        }
    }
}
