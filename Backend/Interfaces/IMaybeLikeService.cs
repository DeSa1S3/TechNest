using Backend.Middleware_Components.DTO;

namespace Backend.Interfaces
{
    public interface IMaybeLikeService
    {
        Task<List<MaybeLikeGetDTO>> GetAllMaybeLikeItems(int page, int pageSize, string? category = null, bool? activeOnly = true);
        Task<MaybeLikeGetDTO?> GetMaybeLikeItemById(int id);
        Task<MaybeLikeGetDTO> CreateMaybeLikeItem(MaybeLikeDTO dto);
        Task<MaybeLikeGetDTO?> UpdateMaybeLikeItem(int id, MaybeLikeUpdateDTO dto);
        Task<bool> DeleteMaybeLikeItem(int id);
        Task<bool> UpdateMaybeLikeImage(int id, string imageUrl);
        Task<bool> DeleteMaybeLikeImage(int id);
        //Task<List<MaybeLikeGetDTO>> GetRandomMaybeLikeItems(int count);
        //Task<List<MaybeLikeGetDTO>> SearchMaybeLikeItems(string query, int page, int pageSize);
    }
}
