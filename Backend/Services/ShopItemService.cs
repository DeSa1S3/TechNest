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
    public class ShopItemService : BaseService, IShopItemService
    {
        public ShopItemService(DataContext context, ILogger<ShopItemService> logger)
            : base(context, logger) { }

        public async Task<List<ShopItems_Get>> GetAllShopItems(int page, int pageSize, string? category = null)
        {
            try
            {
                var query = _context.shop_items.AsQueryable();

                if (!string.IsNullOrEmpty(category))
                    query = query.Where(x => x.Category == category);

                var items = await query
                    .OrderBy(x => x.Id)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => MapToShopItemsGet(x))
                    .ToListAsync();

                return items;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения элементы");
                throw;
            }
        }

        public async Task<ShopItems_Get?> GetShopItemById(int id)
        {
            try
            {
                var item = await _context.shop_items.FindAsync(id);
                return item != null ? MapToShopItemsGet(item) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошбика поулчения элементов покупок с id  {id}");
                throw;
            }
        }

        public async Task<ShopItems_Get> CreateShopItem(ShopItemsDTO dto)
        {
            try
            {
                var shopItem = new ShopItemsTable
                {
                    Img = dto.Img,
                    Name = dto.Name,
                    Price = dto.Price,
                    Quantity = dto.Quantity,
                    Category = dto.Category
                };

                _context.shop_items.Add(shopItem);
                await _context.SaveChangesAsync();

                return MapToShopItemsGet(shopItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка создания элементов покупок");
                throw;
            }
        }

        public async Task<ShopItems_Get?> UpdateShopItem(int id, ShopItemsDTO dto)
        {
            try
            {
                var shopItem = await _context.shop_items.FindAsync(id);
                if (shopItem == null)
                    return null;

                shopItem.Name = dto.Name;
                shopItem.Price = dto.Price;
                shopItem.Quantity = dto.Quantity;
                shopItem.Category = dto.Category;

                if (!string.IsNullOrEmpty(dto.Img))
                    shopItem.Img = dto.Img;

                await _context.SaveChangesAsync();
                return MapToShopItemsGet(shopItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошбика обновления элементов покупок с id {id}");
                throw;
            }
        }

        public async Task<bool> DeleteShopItem(int id)
        {
            try
            {
                var shopItem = await _context.shop_items.FindAsync(id);
                if (shopItem == null)
                    return false;

                if (!string.IsNullOrEmpty(shopItem.Img))
                    DeleteImageFile(shopItem.Img);

                _context.shop_items.Remove(shopItem);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка удаления элементов покупок с id {id}");
                throw;
            }
        }

        public async Task<bool> UpdateShopItemImage(int id, string imageUrl)
        {
            try
            {
                var shopItem = await _context.shop_items.FindAsync(id);
                if (shopItem == null)
                    return false;

                if (!string.IsNullOrEmpty(shopItem.Img))
                    DeleteImageFile(shopItem.Img);

                shopItem.Img = imageUrl;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка обновления фотографии элемента покупок с id {id}");
                throw;
            }
        }

        public async Task<bool> DeleteShopItemImage(int id)
        {
            try
            {
                var shopItem = await _context.shop_items.FindAsync(id);
                if (shopItem == null || string.IsNullOrEmpty(shopItem.Img))
                    return false;

                DeleteImageFile(shopItem.Img);
                shopItem.Img = null;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка удаления фотографии элемента покупок с id  {id}");
                throw;
            }
        }

        public async Task<List<ShopItems_Get>> SearchShopItems(string query, int page, int pageSize)
        {
            try
            {
                var items = await _context.shop_items
                    .Where(x => x.Name.Contains(query) || x.Category.Contains(query))
                    .OrderBy(x => x.Id)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => MapToShopItemsGet(x))
                    .ToListAsync();

                return items;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка поиска товаров в магазине по запросу.: {query}");
                throw;
            }
        }

        public async Task<List<ShopItems_Get>> GetShopItemsByCategory(string category, int page, int pageSize)
        {
            try
            {
                var items = await _context.shop_items
                    .Where(x => x.Category == category)
                    .OrderBy(x => x.Id)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => MapToShopItemsGet(x))
                    .ToListAsync();

                return items;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка получения товаров из магазина по категориям.: {category}");
                throw;
            }
        }

        private ShopItems_Get MapToShopItemsGet(ShopItemsTable item)
        {
            return new ShopItems_Get
            {
                Id = item.Id,
                Img = item.Img,
                Name = item.Name,
                Price = item.Price,
                Quantity = item.Quantity,
                Category = item.Category
            };
        }
    }
} 