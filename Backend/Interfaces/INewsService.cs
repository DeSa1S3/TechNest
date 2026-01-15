using Backend.Middleware_Components.DTO;

namespace Backend.Interfaces
{
    public interface INewsService
    {
        Task<List<NewsGetDTO>> GetAllNews(int page, int pageSize);
        Task<NewsGetDTO?> GetNewsById(int id);
        Task<NewsGetDTO> CreateNews(NewsDTO dto, Guid createdByUserId);
        Task<NewsGetDTO?> UpdateNews(int id, NewsUpdateDTO dto);
        Task<bool> DeleteNews(int id);
        Task<bool> UpdateNewsImage(int id, string imageUrl);
        Task<bool> DeleteNewsImage(int id);
    }
}
