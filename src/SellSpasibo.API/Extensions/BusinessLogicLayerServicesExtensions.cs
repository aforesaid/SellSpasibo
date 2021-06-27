using Microsoft.Extensions.DependencyInjection;
using SellSpasibo.Core.Interfaces;
using SellSpasibo.Core.Services;

namespace SellSpasibo.API.Extensions
{
    public static class BusinessLogicLayerServicesExtensions
    {
        public static void AddBusinessLogicLayerServicesExtensions(this IServiceCollection services)
        {
            services.AddScoped<ITinkoff,TinkoffApiClient>();
            services.AddScoped<ISberSpasibo, SberSpasiboApiClient>();
            //TODO: добавить сервис для взаимодействия с бд/юзерской инфой
        }
    }
}
