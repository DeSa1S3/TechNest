using Backend.Middleware_Components.DTO;

namespace Backend.Interfaces { 
    public interface IDatabaseService
    {
        IUserService UserService { get; }
        IRuleService RuleService { get; }
        IShopItemService ShopItemService { get; }
        INewsService NewsService { get; }
        ICatalogService CatalogService { get; }
        IMaybeLikeService MaybeLikeService { get; }
    }
}