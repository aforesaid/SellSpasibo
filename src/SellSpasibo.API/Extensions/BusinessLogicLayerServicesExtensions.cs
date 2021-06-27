using Microsoft.Extensions.DependencyInjection;
using SellSpasibo.Core.Interfaces;
using SellSpasibo.Core.Services;

namespace SellSpasibo.API.Extensions
{
    public static class BusinessLogicLayerServicesExtensions
    {
        public static void AddBusinessLogicLayerServicesExtensions(this IServiceCollection services)
        {
            services.AddScoped<ITinkoffApiClient,TinkoffApiClient>();
            services.AddScoped<ISberSpasiboApiClient, SberSpasiboApiClient>();
            //TODO: добавить сервис для взаимодействия с бд/юзерской инфой
        }
    }
}
