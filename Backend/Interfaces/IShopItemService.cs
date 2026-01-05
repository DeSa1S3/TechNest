using Backend.Middleware_Components.DTO;

namespace Backend.Interfaces
{
    public interface IShopItemService
    {
        Task<List<ShopItems_Get>> GetAllShopItems(int page, int pageSize, string? category = null);
        Task<ShopItems_Get?> GetShopItemById(int id);
        Task<ShopItems_Get> CreateShopItem(ShopItemsDTO dto);
        Task<ShopItems_Get?> UpdateShopItem(int id, ShopItemsDTO dto);
        Task<bool> DeleteShopItem(int id);
        Task<bool> UpdateShopItemImage(int id, string imageUrl);
        Task<bool> DeleteShopItemImage(int id);
        Task<List<ShopItems_Get>> SearchShopItems(string query, int page, int pageSize);
        Task<List<ShopItems_Get>> GetShopItemsByCategory(string category, int page, int pageSize);
    }
}
