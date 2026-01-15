using Backend.Interfaces;
using Backend.Middleware_Components.DTO;
using Backend.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechNest.Backend.Data;

namespace Backend.Services
{
    public class NewsService : BaseService, INewsService
    {
        public NewsService(DataContext context, ILogger<NewsService> logger) : base(context, logger) { }

        public async Task<List<NewsGetDTO>> GetAllNews(int page, int pageSize)
        {
            try
            {
                var news = await _context.news_tables
                    .OrderByDescending(x => x.CreatedAt)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => MapToNewsGetDTO(x))
                    .ToListAsync();

                return news;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения всех новостей");
                throw;
            }
        }

        public async Task<NewsGetDTO?> GetNewsById(int id)
        {
            try
            {
                var news = await _context.news_tables.FindAsync(id);
                return news != null ? MapToNewsGetDTO(news) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка получения новостей по id {id}");
                throw;
            }
        }

        public async Task<NewsGetDTO> CreateNews(NewsDTO dto, Guid createdByUserId)
        {
            try
            {
                var news = new NewsTable
                {
                    Img = dto.Img,
                    Title = dto.Title,
                    Text = dto.Text,
                    CreatedAt = DateTime.UtcNow
                };

                _context.news_tables.Add(news);
                await _context.SaveChangesAsync();

                return MapToNewsGetDTO(news);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка создания новостей");
                throw;
            }
        }

        public async Task<NewsGetDTO?> UpdateNews(int id, NewsUpdateDTO dto)
        {
            try
            {
                var news = await _context.news_tables.FindAsync(id);
                if (news == null)
                    return null;

                if (!string.IsNullOrEmpty(dto.Title))
                    news.Title = dto.Title;

                if (!string.IsNullOrEmpty(dto.Text))
                    news.Text = dto.Text;

                if (dto.Img != null)
                    news.Img = dto.Img;

                news.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                return MapToNewsGetDTO(news);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка обновления новостей по id {id}");
                throw;
            }
        }

        public async Task<bool> DeleteNews(int id)
        {
            try
            {
                var news = await _context.news_tables.FindAsync(id);
                if (news == null)
                    return false;

                if (!string.IsNullOrEmpty(news.Img))
                    DeleteImageFile(news.Img);

                _context.news_tables.Remove(news);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка удаления новостей по id {id}");
                throw;
            }
        }

        public async Task<bool> UpdateNewsImage(int id, string imageUrl)
        {
            try
            {
                var news = await _context.news_tables.FindAsync(id);
                if (news == null)
                    return false;

                if (!string.IsNullOrEmpty(news.Img))
                    DeleteImageFile(news.Img);

                news.Img = imageUrl;
                news.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка обновления фотографии по id {id}");
                throw;
            }
        }

        public async Task<bool> DeleteNewsImage(int id)
        {
            try
            {
                var news = await _context.news_tables.FindAsync(id);
                if (news == null || string.IsNullOrEmpty(news.Img))
                    return false;

                DeleteImageFile(news.Img);
                news.Img = null;
                news.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка удаления фотографий по id {id}");
                throw;
            }
        }

        private NewsGetDTO MapToNewsGetDTO(NewsTable news)
        {
            return new NewsGetDTO
            {
                Id = news.Id,
                Img = news.Img,
                Title = news.Title,
                Text = news.Text,
                CreatedAt = news.CreatedAt,
                UpdatedAt = news.UpdatedAt
            };
        }
    }
}