using Microsoft.Extensions.DependencyInjection;
using SellSpasibo.BLL.Interfaces;
using SellSpasibo.BLL.Services;
namespace SellSpasibo.Extensions
{
    public static class BusinessLogicLayerServicesExtensions
    {
        public static void AddBusinessLogicLayerServicesExtensions(this IServiceCollection services)
        {
            services.AddScoped<ITinkoff,Tinkoff>();
            services.AddScoped<ISberSpasibo, SberSpasiboApiClient>();
            //TODO: добавить сервис для взаимодействия с бд/юзерской инфой
        }
    }
}
