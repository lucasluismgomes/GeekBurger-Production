using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekBurger.Productions.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using GeekBurger.Productions.Extension;
using GeekBurger.Productions.Service;
using AutoMapper;

namespace GeekBurger.Productions
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var mvcCoreBuilder = services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Production", Version = "v1" });
            });

            services.AddAutoMapper();

            services.AddDbContext<ProductionsContext>(o => o.UseInMemoryDatabase("geekburger-production"));
            services.AddTransient<IProductionAreaRepository, ProductionAreaRepository>();
            services.AddTransient<IStoreRepository, StoreRepository>();
            services.AddSingleton<IProductionAreaChangedService, ProductionAreaChangedService>();
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
