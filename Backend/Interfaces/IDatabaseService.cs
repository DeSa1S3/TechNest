using Backend.Middleware_Components.DTO;

namespace Backend.Interfaces { 
    public interface IDatabaseService
    {
        Task<bool> CheckUserAuth(string email, string password);
        Task<UserGetDTO> AddUser(UserAddDTO dtoObj);
        Task ChangeUser(Guid id, UserChangeDTO dtoObj);
        Task DeleteUser(Guid idUser);
        Task<List<UserGetDTO>> GetAllUsers(int from, int count);
        Task<UserGetDTO?> GetUser(Guid idUser);
        Task<UserGetDTO?> GetUserByEmail(string email);
        Task<UserGetDTO?> GetUserById(int id);
        Task<List<Guid>> CollectAllIdUsers();
        Task RuleFillUp(TimedRuleDTO rule);
        Task<GetRuleDTO?> GetRuleFromDB(Guid ruleId);
        Task<List<GetRuleDTO>> GetAllRulesFromDB();
        Task<MeDTO> GetMeInfo(int userId);
        Task<List<ShopItems_Get>> GetAllShopItems(int page, int pageSize, string? category = null);
        Task<ShopItems_Get?> GetShopItemById(int id);
        Task<ShopItems_Get> CreateShopItem(ShopItemsDTO dto);
        Task<ShopItems_Get?> UpdateShopItem(int id, ShopItemsDTO dto);
        Task<bool> DeleteShopItem(int id);
        Task<bool> UpdateShopItemImage(int id, string imageUrl);
        Task<bool> DeleteShopItemImage(int id);
        Task<List<ShopItems_Get>> SearchShopItems(string query, int page, int pageSize);
        Task<List<ShopItems_Get>> GetShopItemsByCategory(string category, int page, int pageSize);
        Task<List<NewsGetDTO>> GetAllNews(int page, int pageSize);
        Task<NewsGetDTO?> GetNewsById(int id);
        Task<NewsGetDTO> CreateNews(NewsDTO dto, int createdByUserId);
        Task<NewsGetDTO?> UpdateNews(int id, NewsUpdateDTO dto);
        Task<bool> DeleteNews(int id);
        Task<bool> UpdateNewsImage(int id, string imageUrl);
        Task<bool> DeleteNewsImage(int id);
        Task<List<CatalogGetDTO>> GetAllCatalogCategories();
        Task<CatalogGetDTO?> GetCatalogCategoryById(int id);
        Task<CatalogGetDTO> CreateCatalogCategory(CatalogDTO dto);
        Task<CatalogGetDTO?> UpdateCatalogCategory(int id, CatalogUpdateDTO dto);
        Task<bool> DeleteCatalogCategory(int id);
        Task<bool> UpdateCatalogImage(int id, string imageUrl);
        Task<bool> DeleteCatalogImage(int id);
        Task<List<CatalogGetDTO>> GetRootCategories();
        Task<List<CatalogGetDTO>> GetSubCategories(int parentId);
        Task<List<MaybeLikeGetDTO>> GetAllMaybeLikeItems(int page, int pageSize, string? category = null, bool? activeOnly = true);
        Task<MaybeLikeGetDTO?> GetMaybeLikeItemById(int id);
        Task<MaybeLikeGetDTO> CreateMaybeLikeItem(MaybeLikeDTO dto);
        Task<MaybeLikeGetDTO?> UpdateMaybeLikeItem(int id, MaybeLikeUpdateDTO dto);
        Task<bool> DeleteMaybeLikeItem(int id);
        Task<bool> UpdateMaybeLikeImage(int id, string imageUrl);
        Task<bool> DeleteMaybeLikeImage(int id);
        Task<List<MaybeLikeGetDTO>> GetRandomMaybeLikeItems(int count);
        Task<List<MaybeLikeGetDTO>> SearchMaybeLikeItems(string query, int page, int pageSize);
    }
}