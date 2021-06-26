using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SellSpasibo.BLL.Services;
using SellSpasibo.DAL;
using SellSpasibo.Extensions;
using SellSpasibo.Options;

namespace SellSpasibo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            var sberOptions = Configuration.GetSection(SberOptions.Sber).Get<SberOptions>();
            SberSpasibo.SetTokens(sberOptions.AuthToken, sberOptions.RefreshToken);

            var tinkoffOptions = Configuration.GetSection(TinkoffOptions.Tinkoff).Get<TinkoffOptions>();
            Tinkoff.SetValue(tinkoffOptions.SessionId, tinkoffOptions.WuId, tinkoffOptions.Account);
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SellSpasiboDbContext>(options =>
            {
                options.UseInMemoryDatabase("db");
            });
            services.AddBusinessLogicLayerServicesExtensions();
            services.AddControllersWithViews();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
