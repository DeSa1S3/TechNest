using Backend.Interfaces;
using Microsoft.Extensions.Logging;
using TechNest.Backend.Data;

namespace Backend.Services
{
    public class DatabaseService : IDatabaseService
    {
        public IUserService UserService { get; }
        public IRuleService RuleService { get; }
        public IShopItemService ShopItemService { get; }
        public INewsService NewsService { get; }
        public ICatalogService CatalogService { get; }
        public IMaybeLikeService MaybeLikeService { get; }

        public DatabaseService(DataContext context, ILoggerFactory loggerFactory)
        {
            UserService = new UserService(context, loggerFactory.CreateLogger<UserService>());
            RuleService = new RuleService(context, loggerFactory.CreateLogger<RuleService>());
            ShopItemService = new ShopItemService(context, loggerFactory.CreateLogger<ShopItemService>());
            NewsService = new NewsService(context, loggerFactory.CreateLogger<NewsService>());
            CatalogService = new CatalogService(context, loggerFactory.CreateLogger<CatalogService>());
            MaybeLikeService = new MaybeLikeService(context, loggerFactory.CreateLogger<MaybeLikeService>());
        }
    }
}