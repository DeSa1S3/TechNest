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
    public class MaybeLikeService : BaseService, IMaybeLikeService
    {
        public MaybeLikeService(DataContext context, ILogger<MaybeLikeService> logger) : base(context, logger) { }

        public async Task<List<MaybeLikeGetDTO>> GetAllMaybeLikeItems(int page, int pageSize, string? category = null, bool? activeOnly = true)
        {
            try
            {
                var query = _context.maybe_like.AsQueryable();

                if (activeOnly.HasValue && activeOnly.Value)
                    query = query.Where(x => x.IsActive);

                if (!string.IsNullOrEmpty(category))
                    query = query.Where(x => x.Category == category);

                var items = await query
                    .OrderBy(x => x.SortOrder)
                    .ThenByDescending(x => x.CreatedAt)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => MapToMaybeLikeGetDTO(x))
                    .ToListAsync();

                return items;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения всех понравишься элементов");
                throw;
            }
        }

        public async Task<MaybeLikeGetDTO?> GetMaybeLikeItemById(int id)
        {
            try
            {
                var item = await _context.maybe_like.FindAsync(id);
                return item != null ? MapToMaybeLikeGetDTO(item) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка получения понравишься элементов по id {id}");
                throw;
            }
        }

        public async Task<MaybeLikeGetDTO> CreateMaybeLikeItem(MaybeLikeDTO dto)
        {
            try
            {
                var item = new MaybeLikeTable
                {
                    Img = dto.Img,
                    Name = dto.Name,
                    Description = dto.Description,
                    Price = dto.Price,
                    Category = dto.Category,
                    Discount = dto.Discount,
                    IsActive = dto.IsActive ?? true,
                    SortOrder = dto.SortOrder ?? 0,
                    CreatedAt = DateTime.UtcNow
                };

                _context.maybe_like.Add(item);
                await _context.SaveChangesAsync();

                return MapToMaybeLikeGetDTO(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка создания понравишься элементов");
                throw;
            }
        }

        public async Task<MaybeLikeGetDTO?> UpdateMaybeLikeItem(int id, MaybeLikeUpdateDTO dto)
        {
            try
            {
                var item = await _context.maybe_like.FindAsync(id);
                if (item == null)
                    return null;

                if (!string.IsNullOrEmpty(dto.Name))
                    item.Name = dto.Name;

                if (dto.Description != null)
                    item.Description = dto.Description;

                if (dto.Price.HasValue)
                    item.Price = dto.Price.Value;

                if (dto.Category != null)
                    item.Category = dto.Category;

                if (dto.Discount.HasValue)
                    item.Discount = dto.Discount.Value;

                if (dto.IsActive.HasValue)
                    item.IsActive = dto.IsActive.Value;

                if (dto.SortOrder.HasValue)
                    item.SortOrder = dto.SortOrder.Value;

                if (dto.Img != null)
                    item.Img = dto.Img;

                item.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                return MapToMaybeLikeGetDTO(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка обновления понравишься элементов по id {id}");
                throw;
            }
        }

        public async Task<bool> DeleteMaybeLikeItem(int id)
        {
            try
            {
                var item = await _context.maybe_like.FindAsync(id);
                if (item == null)
                    return false;

                if (!string.IsNullOrEmpty(item.Img))
                    DeleteImageFile(item.Img);

                _context.maybe_like.Remove(item);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка удаления понравишься элементов по id {id}");
                throw;
            }
        }

        public async Task<bool> UpdateMaybeLikeImage(int id, string imageUrl)
        {
            try
            {
                var item = await _context.maybe_like.FindAsync(id);
                if (item == null)
                    return false;

                if (!string.IsNullOrEmpty(item.Img))
                    DeleteImageFile(item.Img);

                item.Img = imageUrl;
                item.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка обновления фотографий понравишься элементов по id {id}");
                throw;
            }
        }

        public async Task<bool> DeleteMaybeLikeImage(int id)
        {
            try
            {
                var item = await _context.maybe_like.FindAsync(id);
                if (item == null || string.IsNullOrEmpty(item.Img))
                    return false;

                DeleteImageFile(item.Img);
                item.Img = null;
                item.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка удаления фотографий понравишься элементов по id {id}");
                throw;
            }
        }

        //public async Task<List<MaybeLikeGetDTO>> GetRandomMaybeLikeItems(int count)
        //{
        //    try
        //    {
        //        var allItems = await _context.maybe_like
        //            .Where(x => x.IsActive)
        //            .Select(x => MapToMaybeLikeGetDTO(x))
        //            .ToListAsync();

        //        var random = new Random();
        //        var shuffledItems = allItems.OrderBy(x => random.Next()).Take(count).ToList();

        //        return shuffledItems;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error getting random maybe like items");
        //        throw;
        //    }
        //}

        //public async Task<List<MaybeLikeGetDTO>> SearchMaybeLikeItems(string query, int page, int pageSize)
        //{
        //    try
        //    {
        //        var items = await _context.maybe_like
        //            .Where(x => x.IsActive &&
        //                (x.Name.Contains(query) ||
        //                 x.Description.Contains(query) ||
        //                 x.Category.Contains(query)))
        //            .OrderBy(x => x.SortOrder)
        //            .ThenByDescending(x => x.CreatedAt)
        //            .Skip((page - 1) * pageSize)
        //            .Take(pageSize)
        //            .Select(x => MapToMaybeLikeGetDTO(x))
        //            .ToListAsync();

        //        return items;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, $"Error searching maybe like items with query: {query}");
        //        throw;
        //    }
        //}

        private MaybeLikeGetDTO MapToMaybeLikeGetDTO(MaybeLikeTable item)
        {
            return new MaybeLikeGetDTO
            {
                Id = item.Id,
                Img = item.Img,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                Discount = item.Discount,
                Category = item.Category,
                IsActive = item.IsActive,
                SortOrder = item.SortOrder,
                CreatedAt = item.CreatedAt,
                UpdatedAt = item.UpdatedAt
            };
        }
    }
}