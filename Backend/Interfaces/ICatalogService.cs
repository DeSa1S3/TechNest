using Backend.Middleware_Components.DTO;

namespace Backend.Interfaces
{
    public interface ICatalogService
    {
        Task<List<CatalogGetDTO>> GetAllCatalogCategories();
        Task<CatalogGetDTO?> GetCatalogCategoryById(int id);
        Task<CatalogGetDTO> CreateCatalogCategory(CatalogDTO dto);
        Task<CatalogGetDTO?> UpdateCatalogCategory(int id, CatalogUpdateDTO dto);
        Task<bool> DeleteCatalogCategory(int id);
        Task<bool> UpdateCatalogImage(int id, string imageUrl);
        Task<bool> DeleteCatalogImage(int id);
        Task<List<CatalogGetDTO>> GetRootCategories();
        Task<List<CatalogGetDTO>> GetSubCategories(int parentId);
    }
}
