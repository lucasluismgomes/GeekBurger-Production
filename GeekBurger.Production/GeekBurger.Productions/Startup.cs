using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekBurger.Production.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using GeekBurger.Production.Extension;

namespace GeekBurger.Production
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

            services.AddDbContext<ProductionsContext>(o => o.UseInMemoryDatabase("geekburger-production"));
            services.AddTransient<IProductionRepository, ProductionRepository>();
            services.AddScoped<ProductionsContext, ProductionsContext>();
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
