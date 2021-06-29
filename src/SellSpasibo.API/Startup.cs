using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SellSpasibo.Core.Options;
using SellSpasibo.API.Extensions;
using SellSpasibo.Infrastructure;

namespace SellSpasibo.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SellSpasiboDbContext>(options =>
            {
                options.UseNpgsql("User ID=postgres;Password=root;Server=localhost;Port=5432;Database=sell_spasibo;Integrated Security=true;");
            });
            
            //TODO: продумать роли
            //TODO: продумать админскую часть
            services.Configure<SberOptions>(Configuration.GetSection(SberOptions.Sber));
            services.Configure<TinkoffOptions>(Configuration.GetSection(TinkoffOptions.Tinkoff));
            services.Configure<StringCryptOptions>(Configuration.GetSection(StringCryptOptions.SectionName));
            
            services.AddBusinessLogicLayerServicesExtensions();
            services.AddControllers();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //TODO: добавить RateLimit
            //TODO: на дев добавить сваггер
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
