using Backend.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Services
{
    public static class ServiceManager
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IDatabaseService, DatabaseService>();

            services.AddScoped<IUserService>(sp => sp.GetRequiredService<IDatabaseService>().UserService);
            services.AddScoped<IRuleService>(sp => sp.GetRequiredService<IDatabaseService>().RuleService);
            services.AddScoped<IShopItemService>(sp => sp.GetRequiredService<IDatabaseService>().ShopItemService);
            services.AddScoped<INewsService>(sp => sp.GetRequiredService<IDatabaseService>().NewsService);
            services.AddScoped<ICatalogService>(sp => sp.GetRequiredService<IDatabaseService>().CatalogService);
            services.AddScoped<IMaybeLikeService>(sp => sp.GetRequiredService<IDatabaseService>().MaybeLikeService);

            return services;
        }
    }
}