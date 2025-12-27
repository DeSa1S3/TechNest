using Backend.Interfaces;
using Backend.Middleware_Components.DTO;
using Backend.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechNest.Backend.Data;

namespace Backend.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly ILogger<DatabaseService> _logger;
        private readonly DataContext _context;

        public DatabaseService(DataContext context, ILogger<DatabaseService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> CheckUserAuth(string email, string password)
        {

            await Task.Delay(100);
            return email == "admin@example.com" && password == "admin123";
        }

        public async Task<UserGetDTO> AddUser(UserAddDTO dtoObj)
        {
            _logger.LogInformation($"Adding new user: {dtoObj.firstName} {dtoObj.lastName}, {dtoObj.Email}");

            await Task.Delay(100);

            return new UserGetDTO
            {
                Id = dtoObj.Id,
                firstName = dtoObj.firstName,
                lastName = dtoObj.lastName,
                RegistrationDate = dtoObj.RegistrationDate,
                Status = "Active",
                Email = dtoObj.Email,
                Password = dtoObj.Password,
                Roles = dtoObj.Roles,
                Img = dtoObj.Img,
                created_at = DateTime.UtcNow
            };
        }

        public async Task ChangeUser(Guid id, UserChangeDTO dtoObj)
        {
            _logger.LogInformation($"Изменение пользователя {id}: {dtoObj.firstName} {dtoObj.lastName}, {dtoObj.Email}");
            await Task.Delay(100);
        }

        public async Task DeleteUser(Guid idUser)
        {
            _logger.LogInformation($"Удаление пользователя {idUser}");
            await Task.Delay(100);
        }

        public async Task<List<UserGetDTO>> GetAllUsers(int from, int count)
        {
            _logger.LogInformation($"Получение пользователей из {from}, количество {count}");
            await Task.Delay(100);

            return new List<UserGetDTO>
                {
                    new UserGetDTO
                    {
                        Id = 1,
                        firstName = "Иван",
                        lastName = "Розанов",
                        RegistrationDate = DateTime.UtcNow.AddDays(-10).ToString("yyyy-MM-dd"),
                        Status = "Active",
                        Email = "rozanovivan13.doe@example.com",
                        Password = "12345",
                        Roles = new[] { "USER" },
                        Img = null,
                        created_at = DateTime.UtcNow.AddDays(-10)
                    },
                    new UserGetDTO
                    {
                        Id = 2,
                        firstName = "Антон",
                        lastName = "Сыченко",
                        RegistrationDate = DateTime.UtcNow.AddDays(-5).ToString("yyyy-MM-dd"),
                        Status = "Active",
                        Email = "study123@example.com",
                        Password = "hashed_password",
                        Roles = new[] { "USER", "MANAGER" },
                        Img = null,
                        created_at = DateTime.UtcNow.AddDays(-5)
                    }
                };
        }

        public async Task<UserGetDTO?> GetUser(Guid idUser)
        {
            _logger.LogInformation($"Получение пользователя {idUser}");
            await Task.Delay(100);

            return new UserGetDTO
            {
                Id = 1,
                firstName = "Test",
                lastName = "User",
                RegistrationDate = DateTime.UtcNow.AddDays(-7).ToString("yyyy-MM-dd"),
                Status = "Active",
                Email = "test@example.com",
                Password = "hashed_password",
                Roles = new[] { "USER" },
                Img = null,
                created_at = DateTime.UtcNow.AddDays(-7)
            };
        }

        public async Task<UserGetDTO?> GetUserByEmail(string email)
        {
            _logger.LogInformation($"Получение адреса электронной почты пользователя: {email}");
            await Task.Delay(100);

            return new UserGetDTO
            {
                Id = 1,
                firstName = "Test",
                lastName = "User",
                RegistrationDate = DateTime.UtcNow.AddDays(-7).ToString("yyyy-MM-dd"),
                Status = "Active",
                Email = email,
                Password = "hashed_password",
                Roles = new[] { "USER" },
                Img = null,
                created_at = DateTime.UtcNow.AddDays(-7)
            };
        }

        public async Task<UserGetDTO?> GetUserById(int id)
        {
            _logger.LogInformation($"Получение пользователя по идентификатору: {id}");
            await Task.Delay(100);

            return new UserGetDTO
            {
                Id = id,
                firstName = "Test",
                lastName = "User",
                RegistrationDate = DateTime.UtcNow.AddDays(-7).ToString("yyyy-MM-dd"),
                Status = "Active",
                Email = "test@example.com",
                Password = "hashed_password",
                Roles = new[] { "USER" },
                Img = null,
                created_at = DateTime.UtcNow.AddDays(-7)
            };
        }

        public async Task<List<Guid>> CollectAllIdUsers()
        {
            await Task.Delay(100);
            return new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        }

        public async Task RuleFillUp(TimedRuleDTO rule)
        {
            _logger.LogInformation($"Adding rule: {rule.name}");
            await Task.Delay(100);
        }

        public async Task<GetRuleDTO?> GetRuleFromDB(Guid ruleId)
        {
            await Task.Delay(100);
            return new GetRuleDTO
            {
                id = ruleId,
                name = "Test Rule",
                description = "Test Description",
                logic = "test > 0",
                status = "Active",
                created_at = DateTime.UtcNow
            };
        }

        public async Task<List<GetRuleDTO>> GetAllRulesFromDB()
        {
            await Task.Delay(100);
            return new List<GetRuleDTO>
                {
                    new GetRuleDTO
                    {
                        id = Guid.NewGuid(),
                        name = "Rule 1",
                        description = "First rule",
                        logic = "value > 10",
                        status = "Active",
                        created_at = DateTime.UtcNow.AddDays(-5)
                    }
                };
        }

        public async Task<MeDTO> GetMeInfo(int userId)
        {
            await Task.Delay(100);
            return new MeDTO
            {
                Id = userId,
                firstName = "Current",
                lastName = "User",
                RegistrationDate = DateTime.UtcNow.AddDays(-30).ToString("yyyy-MM-dd"),
                Email = "current.user@example.com",
                Password = "hashed_password",
                Img = null
            };
        }
        public async Task<List<ShopItems_Get>> GetAllShopItems(int page, int pageSize, string? category = null)
        {
            try
            {
                var query = _context.shop_items.AsQueryable();

                if (!string.IsNullOrEmpty(category))
                {
                    query = query.Where(x => x.Category == category);
                }

                var items = await query
                    .OrderBy(x => x.Id)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => new ShopItems_Get
                    {
                        Id = x.Id,
                        Img = x.Img,
                        Name = x.Name,
                        Price = x.Price,
                        Quantity = x.Quantity,
                        Category = x.Category
                    })
                    .ToListAsync();

                return items;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all shop items");
                throw;
            }
        }

        public async Task<ShopItems_Get?> GetShopItemById(int id)
        {
            try
            {
                var item = await _context.shop_items
                    .Where(x => x.Id == id)
                    .Select(x => new ShopItems_Get
                    {
                        Id = x.Id,
                        Img = x.Img,
                        Name = x.Name,
                        Price = x.Price,
                        Quantity = x.Quantity,
                        Category = x.Category
                    })
                    .FirstOrDefaultAsync();

                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting shop item with id {id}");
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

                return new ShopItems_Get
                {
                    Id = shopItem.Id,
                    Img = shopItem.Img,
                    Name = shopItem.Name,
                    Price = shopItem.Price,
                    Quantity = shopItem.Quantity,
                    Category = shopItem.Category
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating shop item");
                throw;
            }
        }

        public async Task<ShopItems_Get?> UpdateShopItem(int id, ShopItemsDTO dto)
        {
            try
            {
                var shopItem = await _context.shop_items.FindAsync(id);
                if (shopItem == null)
                {
                    return null;
                }

                shopItem.Name = dto.Name;
                shopItem.Price = dto.Price;
                shopItem.Quantity = dto.Quantity;
                shopItem.Category = dto.Category;

                if (!string.IsNullOrEmpty(dto.Img))
                {
                    shopItem.Img = dto.Img;
                }

                await _context.SaveChangesAsync();

                return new ShopItems_Get
                {
                    Id = shopItem.Id,
                    Img = shopItem.Img,
                    Name = shopItem.Name,
                    Price = shopItem.Price,
                    Quantity = shopItem.Quantity,
                    Category = shopItem.Category
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating shop item with id {id}");
                throw;
            }
        }

        public async Task<bool> DeleteShopItem(int id)
        {
            try
            {
                var shopItem = await _context.shop_items.FindAsync(id);
                if (shopItem == null)
                {
                    return false;
                }

                _context.shop_items.Remove(shopItem);
                await _context.SaveChangesAsync();

                if (!string.IsNullOrEmpty(shopItem.Img))
                {
                    try
                    {
                        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", shopItem.Img.TrimStart('/'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, $"Failed to delete image file for shop item {id}");
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting shop item with id {id}");
                throw;
            }
        }

        public async Task<bool> UpdateShopItemImage(int id, string imageUrl)
        {
            try
            {
                var shopItem = await _context.shop_items.FindAsync(id);
                if (shopItem == null)
                {
                    return false;
                }

                if (!string.IsNullOrEmpty(shopItem.Img))
                {
                    try
                    {
                        var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", shopItem.Img.TrimStart('/'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, $"Failed to delete old image file for shop item {id}");
                    }
                }

                shopItem.Img = imageUrl;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating image for shop item with id {id}");
                throw;
            }
        }

        public async Task<bool> DeleteShopItemImage(int id)
        {
            try
            {
                var shopItem = await _context.shop_items.FindAsync(id);
                if (shopItem == null || string.IsNullOrEmpty(shopItem.Img))
                {
                    return false;
                }

                try
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", shopItem.Img.TrimStart('/'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, $"Failed to delete image file for shop item {id}");
                }

                shopItem.Img = null;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting image for shop item with id {id}");
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
                    .Select(x => new ShopItems_Get
                    {
                        Id = x.Id,
                        Img = x.Img,
                        Name = x.Name,
                        Price = x.Price,
                        Quantity = x.Quantity,
                        Category = x.Category
                    })
                    .ToListAsync();

                return items;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error searching shop items with query: {query}");
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
                    .Select(x => new ShopItems_Get
                    {
                        Id = x.Id,
                        Img = x.Img,
                        Name = x.Name,
                        Price = x.Price,
                        Quantity = x.Quantity,
                        Category = x.Category
                    })
                    .ToListAsync();

                return items;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting shop items by category: {category}");
                throw;
            }
        }
        public async Task<List<NewsGetDTO>> GetAllNews(int page, int pageSize)
        {
            try
            {
                var news = await _context.news_tables
                    .OrderByDescending(x => x.CreatedAt)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => new NewsGetDTO
                    {
                        Id = x.Id,
                        Img = x.Img,
                        Title = x.Title,
                        Text = x.Text,
                        CreatedAt = x.CreatedAt,
                        UpdatedAt = x.UpdatedAt
                    })
                    .ToListAsync();

                return news;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all news");
                throw;
            }
        }
        public async Task<NewsGetDTO?> GetNewsById(int id)
        {
            try
            {
                var news = await _context.news_tables
                    .Where(x => x.Id == id)
                    .Select(x => new NewsGetDTO
                    {
                        Id = x.Id,
                        Img = x.Img,
                        Title = x.Title,
                        Text = x.Text,
                        CreatedAt = x.CreatedAt,
                        UpdatedAt = x.UpdatedAt
                    })
                    .FirstOrDefaultAsync();

                return news;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting news with id {id}");
                throw;
            }
        }

        public async Task<NewsGetDTO> CreateNews(NewsDTO dto, int createdByUserId)
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating news");
                throw;
            }
        }

        public async Task<NewsGetDTO?> UpdateNews(int id, NewsUpdateDTO dto)
        {
            try
            {
                var news = await _context.news_tables.FindAsync(id);
                if (news == null)
                {
                    return null;
                }

                if (!string.IsNullOrEmpty(dto.Title))
                {
                    news.Title = dto.Title;
                }

                if (!string.IsNullOrEmpty(dto.Text))
                {
                    news.Text = dto.Text;
                }

                if (dto.Img != null)
                {
                    news.Img = dto.Img;
                }

                news.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();

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
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating news with id {id}");
                throw;
            }
        }

        public async Task<bool> DeleteNews(int id)
        {
            try
            {
                var news = await _context.news_tables.FindAsync(id);
                if (news == null)
                {
                    return false;
                }

                if (!string.IsNullOrEmpty(news.Img))
                {
                    try
                    {
                        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", news.Img.TrimStart('/'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, $"Failed to delete image file for news {id}");
                    }
                }

                _context.news_tables.Remove(news);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting news with id {id}");
                throw;
            }
        }

        public async Task<bool> UpdateNewsImage(int id, string imageUrl)
        {
            try
            {
                var news = await _context.news_tables.FindAsync(id);
                if (news == null)
                {
                    return false;
                }

                if (!string.IsNullOrEmpty(news.Img))
                {
                    try
                    {
                        var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", news.Img.TrimStart('/'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, $"Failed to delete old image file for news {id}");
                    }
                }

                news.Img = imageUrl;
                news.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating image for news with id {id}");
                throw;
            }
        }

        public async Task<bool> DeleteNewsImage(int id)
        {
            try
            {
                var news = await _context.news_tables.FindAsync(id);
                if (news == null || string.IsNullOrEmpty(news.Img))
                {
                    return false;
                }

                try
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", news.Img.TrimStart('/'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, $"Failed to delete image file for news {id}");
                }

                news.Img = null;
                news.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting image for news with id {id}");
                throw;
            }

        }
        public async Task<List<CatalogGetDTO>> GetAllCatalogCategories()
        {
            try
            {
                var categories = await _context.catalog_tables
                    .Include(c => c.ParentCategory)
                    .Include(c => c.SubCategories)
                    .OrderBy(c => c.Name)
                    .Select(c => new CatalogGetDTO
                    {
                        Id = c.Id,
                        Img = c.Img,
                        Name = c.Name,
                        Description = c.Description,
                        CreatedAt = c.CreatedAt,
                        UpdatedAt = c.UpdatedAt,
                        ParentCategoryId = c.ParentCategoryId,
                        ParentCategoryName = c.ParentCategory != null ? c.ParentCategory.Name : null,
                        SubCategories = c.SubCategories.Select(sc => new CatalogGetDTO
                        {
                            Id = sc.Id,
                            Name = sc.Name
                        }).ToList()
                    })
                    .ToListAsync();

                return categories;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all catalog categories");
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
                    .Where(c => c.Id == id)
                    .Select(c => new CatalogGetDTO
                    {
                        Id = c.Id,
                        Img = c.Img,
                        Name = c.Name,
                        Description = c.Description,
                        CreatedAt = c.CreatedAt,
                        UpdatedAt = c.UpdatedAt,
                        ParentCategoryId = c.ParentCategoryId,
                        ParentCategoryName = c.ParentCategory != null ? c.ParentCategory.Name : null,
                        SubCategories = c.SubCategories.Select(sc => new CatalogGetDTO
                        {
                            Id = sc.Id,
                            Img = sc.Img,
                            Name = sc.Name,
                            Description = sc.Description
                        }).ToList()
                    })
                    .FirstOrDefaultAsync();

                return category;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting catalog category with id {id}");
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
                    {
                        throw new Exception($"Parent category with id {dto.ParentCategoryId} not found");
                    }
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
                    UpdatedAt = category.UpdatedAt,
                    ParentCategoryId = category.ParentCategoryId
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating catalog category");
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
                {
                    return null;
                }

                if (dto.ParentCategoryId.HasValue && dto.ParentCategoryId.Value == id)
                {
                    throw new Exception("Category cannot be its own parent");
                }

                if (dto.ParentCategoryId.HasValue)
                {
                    var parentCategory = await _context.catalog_tables.FindAsync(dto.ParentCategoryId.Value);
                    if (parentCategory == null)
                    {
                        throw new Exception($"Parent category with id {dto.ParentCategoryId} not found");
                    }
                }

                if (!string.IsNullOrEmpty(dto.Name))
                {
                    category.Name = dto.Name;
                }

                if (dto.Description != null)
                {
                    category.Description = dto.Description;
                }

                if (dto.ParentCategoryId.HasValue)
                {
                    category.ParentCategoryId = dto.ParentCategoryId.Value;
                }
                else if (dto.ParentCategoryId == null)
                {
                    category.ParentCategoryId = null;
                }

                if (dto.Img != null)
                {
                    category.Img = dto.Img;
                }

                category.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                return new CatalogGetDTO
                {
                    Id = category.Id,
                    Img = category.Img,
                    Name = category.Name,
                    Description = category.Description,
                    CreatedAt = category.CreatedAt,
                    UpdatedAt = category.UpdatedAt,
                    ParentCategoryId = category.ParentCategoryId,
                    ParentCategoryName = category.ParentCategory?.Name
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating catalog category with id {id}");
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
                {
                    return false;
                }

                if (category.SubCategories.Any())
                {
                    throw new Exception("Cannot delete category that has subcategories. Delete or move subcategories first.");
                }

                if (!string.IsNullOrEmpty(category.Img))
                {
                    try
                    {
                        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", category.Img.TrimStart('/'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, $"Failed to delete image file for catalog category {id}");
                    }
                }

                _context.catalog_tables.Remove(category);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting catalog category with id {id}");
                throw;
            }
        }

        public async Task<bool> UpdateCatalogImage(int id, string imageUrl)
        {
            try
            {
                var category = await _context.catalog_tables.FindAsync(id);
                if (category == null)
                {
                    return false;
                }

                if (!string.IsNullOrEmpty(category.Img))
                {
                    try
                    {
                        var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", category.Img.TrimStart('/'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, $"Failed to delete old image file for catalog category {id}");
                    }
                }

                category.Img = imageUrl;
                category.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating image for catalog category with id {id}");
                throw;
            }
        }

        public async Task<bool> DeleteCatalogImage(int id)
        {
            try
            {
                var category = await _context.catalog_tables.FindAsync(id);
                if (category == null || string.IsNullOrEmpty(category.Img))
                {
                    return false;
                }

                try
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", category.Img.TrimStart('/'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, $"Failed to delete image file for catalog category {id}");
                }

                category.Img = null;
                category.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting image for catalog category with id {id}");
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
                    .Select(c => new CatalogGetDTO
                    {
                        Id = c.Id,
                        Img = c.Img,
                        Name = c.Name,
                        Description = c.Description,
                        CreatedAt = c.CreatedAt,
                        UpdatedAt = c.UpdatedAt,
                        SubCategories = c.SubCategories.Select(sc => new CatalogGetDTO
                        {
                            Id = sc.Id,
                            Name = sc.Name
                        }).ToList()
                    })
                    .ToListAsync();

                return categories;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting root categories");
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
                    .Select(c => new CatalogGetDTO
                    {
                        Id = c.Id,
                        Img = c.Img,
                        Name = c.Name,
                        Description = c.Description,
                        CreatedAt = c.CreatedAt,
                        UpdatedAt = c.UpdatedAt,
                        SubCategories = c.SubCategories.Select(sc => new CatalogGetDTO
                        {
                            Id = sc.Id,
                            Name = sc.Name
                        }).ToList()
                    })
                    .ToListAsync();

                return categories;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting subcategories for parent id {parentId}");
                throw;
            }
        }
        public async Task<List<MaybeLikeGetDTO>> GetAllMaybeLikeItems(int page, int pageSize, string? category = null, bool? activeOnly = true)
        {
            try
            {
                var query = _context.maybe_like.AsQueryable();

                if (activeOnly.HasValue && activeOnly.Value)
                {
                    query = query.Where(x => x.IsActive);
                }

                if (!string.IsNullOrEmpty(category))
                {
                    query = query.Where(x => x.Category == category);
                }

                var items = await query
                    .OrderBy(x => x.SortOrder)
                    .ThenByDescending(x => x.CreatedAt)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => new MaybeLikeGetDTO
                    {
                        Id = x.Id,
                        Img = x.Img,
                        Name = x.Name,
                        Description = x.Description,
                        Price = x.Price,
                        Discount = x.Discount,
                        Category = x.Category,
                        IsActive = x.IsActive,
                        SortOrder = x.SortOrder,
                        CreatedAt = x.CreatedAt,
                        UpdatedAt = x.UpdatedAt
                    })
                    .ToListAsync();

                return items;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all maybe like items");
                throw;
            }
        }

        public async Task<MaybeLikeGetDTO?> GetMaybeLikeItemById(int id)
        {
            try
            {
                var item = await _context.maybe_like
                    .Where(x => x.Id == id)
                    .Select(x => new MaybeLikeGetDTO
                    {
                        Id = x.Id,
                        Img = x.Img,
                        Name = x.Name,
                        Description = x.Description,
                        Price = x.Price,
                        Discount = x.Discount,
                        Category = x.Category,
                        IsActive = x.IsActive,
                        SortOrder = x.SortOrder,
                        CreatedAt = x.CreatedAt,
                        UpdatedAt = x.UpdatedAt
                    })
                    .FirstOrDefaultAsync();

                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting maybe like item with id {id}");
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating maybe like item");
                throw;
            }
        }

        public async Task<MaybeLikeGetDTO?> UpdateMaybeLikeItem(int id, MaybeLikeUpdateDTO dto)
        {
            try
            {
                var item = await _context.maybe_like.FindAsync(id);
                if (item == null)
                {
                    return null;
                }

                if (!string.IsNullOrEmpty(dto.Name))
                {
                    item.Name = dto.Name;
                }

                if (dto.Description != null)
                {
                    item.Description = dto.Description;
                }

                if (dto.Price.HasValue)
                {
                    item.Price = dto.Price.Value;
                }

                if (dto.Category != null)
                {
                    item.Category = dto.Category;
                }

                if (dto.Discount.HasValue)
                {
                    item.Discount = dto.Discount.Value;
                }

                if (dto.IsActive.HasValue)
                {
                    item.IsActive = dto.IsActive.Value;
                }

                if (dto.SortOrder.HasValue)
                {
                    item.SortOrder = dto.SortOrder.Value;
                }

                if (dto.Img != null)
                {
                    item.Img = dto.Img;
                }

                item.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();

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
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating maybe like item with id {id}");
                throw;
            }
        }

        public async Task<bool> DeleteMaybeLikeItem(int id)
        {
            try
            {
                var item = await _context.maybe_like.FindAsync(id);
                if (item == null)
                {
                    return false;
                }

                if (!string.IsNullOrEmpty(item.Img))
                {
                    try
                    {
                        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", item.Img.TrimStart('/'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, $"Не удалось удалить файл изображения, возможно, для элемента {id}.");
                    }
                }

                _context.maybe_like.Remove(item);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при удалении, возможно, элемента с идентификатором {id}.");
                throw;
            }
        }

        public async Task<bool> UpdateMaybeLikeImage(int id, string imageUrl)
        {
            try
            {
                var item = await _context.maybe_like.FindAsync(id);
                if (item == null)
                {
                    return false;
                }


                if (!string.IsNullOrEmpty(item.Img))
                {
                    try
                    {
                        var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", item.Img.TrimStart('/'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, $"Failed to delete old image file for maybe like item {id}");
                    }
                }

                item.Img = imageUrl;
                item.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating image for maybe like item with id {id}");
                throw;
            }
        }

        public async Task<bool> DeleteMaybeLikeImage(int id)
        {
            try
            {
                var item = await _context.maybe_like.FindAsync(id);
                if (item == null || string.IsNullOrEmpty(item.Img))
                {
                    return false;
                }

                try
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", item.Img.TrimStart('/'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, $"Не удалось удалить файл изображения, возможно, для элемента {id}.");
                }

                item.Img = null;
                item.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при удалении изображения для элемента с идентификатором {id}");
                throw;
            }
        }

        public async Task<List<MaybeLikeGetDTO>> GetRandomMaybeLikeItems(int count)
        {
            try
            {
                var allItems = await _context.maybe_like
                    .Where(x => x.IsActive)
                    .Select(x => new MaybeLikeGetDTO
                    {
                        Id = x.Id,
                        Img = x.Img,
                        Name = x.Name,
                        Description = x.Description,
                        Price = x.Price,
                        Discount = x.Discount,
                        Category = x.Category,
                        IsActive = x.IsActive,
                        SortOrder = x.SortOrder,
                        CreatedAt = x.CreatedAt,
                        UpdatedAt = x.UpdatedAt
                    })
                    .ToListAsync();

                var random = new Random();
                var shuffledItems = allItems.OrderBy(x => random.Next()).Take(count).ToList();

                return shuffledItems;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка, возможно, связана с получением случайных предметов.");
                throw;
            }
        }

        public async Task<List<MaybeLikeGetDTO>> SearchMaybeLikeItems(string query, int page, int pageSize)
        {
            try
            {
                var items = await _context.maybe_like
                    .Where(x => x.IsActive &&
                        (x.Name.Contains(query) ||
                         x.Description.Contains(query) ||
                         x.Category.Contains(query)))
                    .OrderBy(x => x.SortOrder)
                    .ThenByDescending(x => x.CreatedAt)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => new MaybeLikeGetDTO
                    {
                        Id = x.Id,
                        Img = x.Img,
                        Name = x.Name,
                        Description = x.Description,
                        Price = x.Price,
                        Discount = x.Discount,
                        Category = x.Category,
                        IsActive = x.IsActive,
                        SortOrder = x.SortOrder,
                        CreatedAt = x.CreatedAt,
                        UpdatedAt = x.UpdatedAt
                    })
                    .ToListAsync();

                return items;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка поиска может быть связана с элементами, содержащими запрос: {query}.");
                throw;
            }
        }
    }

}