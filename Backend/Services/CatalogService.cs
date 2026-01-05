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
    public class CatalogService : BaseService, ICatalogService
    {
        public CatalogService(DataContext context, ILogger<CatalogService> logger)
            : base(context, logger) { }

        public async Task<List<CatalogGetDTO>> GetAllCatalogCategories()
        {
            try
            {
                var categories = await _context.catalog_tables
                    .Include(c => c.ParentCategory)
                    .Include(c => c.SubCategories)
                    .OrderBy(c => c.Name)
                    .Select(c => MapToCatalogGetDTO(c))
                    .ToListAsync();

                return categories;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения всех товаров");
                throw;
            }
        }

        public async Task<CatalogGetDTO?> GetCatalogCategoryById(int id)
        {
            try
            {
                var category = await _context.catalog_tables
                    .Include(c => c.ParentCategory)
                    .Include(c => c.SubCategories)
                    .FirstOrDefaultAsync(c => c.Id == id);

                return category != null ? MapToCatalogGetDTO(category) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка поулчения товаров по id {id}");
                throw;
            }
        }

        public async Task<CatalogGetDTO> CreateCatalogCategory(CatalogDTO dto)
        {
            try
            {
                if (dto.ParentCategoryId.HasValue)
                {
                    var parentExists = await _context.catalog_tables.AnyAsync(c => c.Id == dto.ParentCategoryId.Value);
                    if (!parentExists)
                        throw new Exception($"Главная категория по id {dto.ParentCategoryId}  не обноружено");
                }

                var category = new CatalogTable
                {
                    Img = dto.Img,
                    Name = dto.Name,
                    Description = dto.Description,
                    ParentCategoryId = dto.ParentCategoryId,
                    CreatedAt = DateTime.UtcNow
                };

                _context.catalog_tables.Add(category);
                await _context.SaveChangesAsync();

                return new CatalogGetDTO
                {
                    Id = category.Id,
                    Img = category.Img,
                    Name = category.Name,
                    Description = category.Description,
                    CreatedAt = category.CreatedAt,
                    ParentCategoryId = category.ParentCategoryId
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка создания категории для каталога");
                throw;
            }
        }

        public async Task<CatalogGetDTO?> UpdateCatalogCategory(int id, CatalogUpdateDTO dto)
        {
            try
            {
                var category = await _context.catalog_tables
                    .Include(c => c.ParentCategory)
                    .Include(c => c.SubCategories)
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (category == null)
                    return null;

                if (dto.ParentCategoryId.HasValue && dto.ParentCategoryId.Value == id)
                    throw new Exception("Категория не может быть своим собственным родителем.");

                if (dto.ParentCategoryId.HasValue)
                {
                    var parentExists = await _context.catalog_tables.AnyAsync(c => c.Id == dto.ParentCategoryId.Value);
                    if (!parentExists)
                        throw new Exception($"Родительская категория по id {dto.ParentCategoryId} не найдена.");
                }

                if (!string.IsNullOrEmpty(dto.Name))
                    category.Name = dto.Name;

                if (dto.Description != null)
                    category.Description = dto.Description;

                if (dto.ParentCategoryId.HasValue)
                    category.ParentCategoryId = dto.ParentCategoryId.Value;
                else if (dto.ParentCategoryId == null)
                    category.ParentCategoryId = null;

                if (dto.Img != null)
                    category.Img = dto.Img;

                category.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                return MapToCatalogGetDTO(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка обновления категории по id {id}");
                throw;
            }
        }

        public async Task<bool> DeleteCatalogCategory(int id)
        {
            try
            {
                var category = await _context.catalog_tables
                    .Include(c => c.SubCategories)
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (category == null)
                    return false;

                if (category.SubCategories.Any())
                    throw new Exception("Невозможно удалить категорию, содержащую подкатегории.");

                if (!string.IsNullOrEmpty(category.Img))
                    DeleteImageFile(category.Img);

                _context.catalog_tables.Remove(category);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при удалении категории каталога по id {id}");
                throw;
            }
        }

        public async Task<bool> UpdateCatalogImage(int id, string imageUrl)
        {
            try
            {
                var category = await _context.catalog_tables.FindAsync(id);
                if (category == null)
                    return false;

                if (!string.IsNullOrEmpty(category.Img))
                    DeleteImageFile(category.Img);

                category.Img = imageUrl;
                category.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка обновления изображения для категории каталога по id. {id}");
                throw;
            }
        }

        public async Task<bool> DeleteCatalogImage(int id)
        {
            try
            {
                var category = await _context.catalog_tables.FindAsync(id);
                if (category == null || string.IsNullOrEmpty(category.Img))
                    return false;

                DeleteImageFile(category.Img);
                category.Img = null;
                category.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при удалении изображения для категории каталога по id. {id}");
                throw;
            }
        }

        public async Task<List<CatalogGetDTO>> GetRootCategories()
        {
            try
            {
                var categories = await _context.catalog_tables
                    .Include(c => c.SubCategories)
                    .Where(c => c.ParentCategoryId == null)
                    .OrderBy(c => c.Name)
                    .Select(c => MapToCatalogGetDTO(c))
                    .ToListAsync();

                return categories;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении главной категории");
                throw;
            }
        }

        public async Task<List<CatalogGetDTO>> GetSubCategories(int parentId)
        {
            try
            {
                var categories = await _context.catalog_tables
                    .Include(c => c.SubCategories)
                    .Where(c => c.ParentCategoryId == parentId)
                    .OrderBy(c => c.Name)
                    .Select(c => MapToCatalogGetDTO(c))
                    .ToListAsync();

                return categories;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при получении подкатегорий по id. {parentId}");
                throw;
            }
        }

        private CatalogGetDTO MapToCatalogGetDTO(CatalogTable category)
        {
            return new CatalogGetDTO
            {
                Id = category.Id,
                Img = category.Img,
                Name = category.Name,
                Description = category.Description,
                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt,
                ParentCategoryId = category.ParentCategoryId,
                ParentCategoryName = category.ParentCategory?.Name,
                SubCategories = category.SubCategories?.Select(sc => new CatalogGetDTO
                {
                    Id = sc.Id,
                    Name = sc.Name,
                    Img = sc.Img,
                    Description = sc.Description
                }).ToList() ?? new List<CatalogGetDTO>()
            };
        }
    }
}