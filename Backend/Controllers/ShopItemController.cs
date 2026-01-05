using Backend.Interfaces;
using Backend.Middleware_Components.DTO;
using Backend.Middleware_Components.Interfaces;
using Backend.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/Shop-items")]
    [ApiController]
    public class ShopItemsController : ControllerBase
    {
        private readonly ILogger<ShopItemsController> _logger;
        private readonly IBackendService _backendService;
        private readonly IJwtTokensService _jwtTokensService;
        private readonly IDatabaseService _databaseService;

        public ShopItemsController(ILogger<ShopItemsController> logger,IBackendService backendService,IJwtTokensService jwtTokensService,IDatabaseService databaseService)
        {
            _logger = logger;
            _backendService = backendService;
            _jwtTokensService = jwtTokensService;
            //_databaseService = databaseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllShopItems([FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] string? category = null)
        {
            try
            {
                var authHeader = Request.Headers["Authorization"].ToString();
                if (string.IsNullOrEmpty(authHeader))
                {
                    return Unauthorized("Заголовок Authorization отсутствует");
                }

                var isValid = await _jwtTokensService.IsAccessValid(authHeader);
                if (!isValid)
                {
                    return Unauthorized("Неверный токен");
                }

                var items = await _databaseService.GetAllShopItems(page, pageSize, category);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении всех товаров магазина");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetShopItemById(int id)
        {
            try
            {
                var authHeader = Request.Headers["Authorization"].ToString();
                if (string.IsNullOrEmpty(authHeader))
                {
                    return Unauthorized("Заголовок Authorization отсутствует");
                }

                var isValid = await _jwtTokensService.IsAccessValid(authHeader);
                if (!isValid)
                {
                    return Unauthorized("Неверный токен");
                }

                var item = await _databaseService.GetShopItemById(id);
                if (item == null)
                {
                    return NotFound($"Товар магазина с id {id} не найден");
                }

                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при получении товара магазина с id {id}");
                return StatusCode(500, ex.Message);
            }
        }
        //Только менеджер
        [HttpPost]
        public async Task<IActionResult> CreateShopItem([FromBody] ShopItemsDTO dto)
        {
            try
            {
                var authHeader = Request.Headers["Authorization"].ToString();
                if (string.IsNullOrEmpty(authHeader))
                {
                    return Unauthorized("Заголовок Authorization отсутствует");
                }

                var isManager = await _jwtTokensService.RoleValid(authHeader, "MANAGER");
                if (!isManager)
                {
                    return Forbid("Только менеджеры могут создавать товары магазина");
                }

                var createdItem = await _databaseService.CreateShopItem(dto);
                return CreatedAtAction(nameof(GetShopItemById), new { id = createdItem.Id }, createdItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при создании товара магазина");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShopItem(int id, [FromBody] ShopItemsDTO dto)
        {
            try
            {
                var authHeader = Request.Headers["Authorization"].ToString();
                if (string.IsNullOrEmpty(authHeader))
                {
                    return Unauthorized("Заголовок Authorization отсутствует");
                }

                var isManager = await _jwtTokensService.RoleValid(authHeader, "MANAGER");
                if (!isManager)
                {
                    return Forbid("Только менеджеры могут обновлять товары магазина");
                }

                var updatedItem = await _databaseService.UpdateShopItem(id, dto);
                if (updatedItem == null)
                {
                    return NotFound($"Товар магазина с id {id} не найден");
                }

                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при обновлении товара магазина с id {id}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShopItem(int id)
        {
            try
            {
                var authHeader = Request.Headers["Authorization"].ToString();
                if (string.IsNullOrEmpty(authHeader))
                {
                    return Unauthorized("Заголовок Authorization отсутствует");
                }

                var isManager = await _jwtTokensService.RoleValid(authHeader, "MANAGER");
                if (!isManager)
                {
                    return Forbid("Только менеджеры могут удалять товары магазина");
                }

                var success = await _databaseService.DeleteShopItem(id);
                if (!success)
                {
                    return NotFound($"Товар магазина с id {id} не найден");
                }

                return Ok(new { message = $"Товар магазина с id {id} успешно удален" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при удалении товара магазина с id {id}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/upload-image")]
        public async Task<IActionResult> UploadImage(int id, IFormFile file)
        {
            try
            {
                var authHeader = Request.Headers["Authorization"].ToString();
                if (string.IsNullOrEmpty(authHeader))
                {
                    return Unauthorized("Заголовок Authorization отсутствует");
                }

                var isManager = await _jwtTokensService.RoleValid(authHeader, "MANAGER");
                if (!isManager)
                {
                    return Forbid("Только менеджеры могут загружать изображения");
                }

                if (file == null || file.Length == 0)
                {
                    return BadRequest("Файл не загружен");
                }

                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (string.IsNullOrEmpty(extension) || !allowedExtensions.Contains(extension))
                {
                    return BadRequest("Неподдерживаемый тип файла. Разрешены: jpg, jpeg, png, gif, webp");
                }

                if (file.Length > 5 * 1024 * 1024)
                {
                    return BadRequest("Размер файла слишком большой. Максимальный размер: 5MB");
                }

                var fileName = $"shopitem_{id}_{DateTime.UtcNow.Ticks}{extension}";
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "shop-items");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var imageUrl = $"/uploads/shop-items/{fileName}";
                var success = await _databaseService.UpdateShopItemImage(id, imageUrl);

                if (!success)
                {
                    return NotFound($"Товар магазина с id {id} не найден");
                }

                return Ok(new
                {
                    message = "Изображение успешно загружено",
                    imageUrl = imageUrl
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при загрузке изображения для товара магазина с id {id}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}/image")]
        public async Task<IActionResult> GetImage(int id)
        {
            try
            {
                var authHeader = Request.Headers["Authorization"].ToString();
                if (string.IsNullOrEmpty(authHeader))
                {
                    return Unauthorized("Заголовок Authorization отсутствует");
                }

                var isValid = await _jwtTokensService.IsAccessValid(authHeader);
                if (!isValid)
                {
                    return Unauthorized("Неверный токен");
                }

                var item = await _databaseService.GetShopItemById(id);
                if (item == null || string.IsNullOrEmpty(item.Img))
                {
                    return NotFound("Изображение не найдено");
                }

                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", item.Img.TrimStart('/'));

                if (!System.IO.File.Exists(imagePath))
                {
                    return NotFound("Файл изображения не найден");
                }

                var imageBytes = await System.IO.File.ReadAllBytesAsync(imagePath);
                var contentType = GetContentType(imagePath);

                return File(imageBytes, contentType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при получении изображения для товара магазина с id {id}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchShopItems(
            [FromQuery] string query,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            try
            {
                var authHeader = Request.Headers["Authorization"].ToString();
                if (string.IsNullOrEmpty(authHeader))
                {
                    return Unauthorized("Заголовок Authorization отсутствует");
                }

                var isValid = await _jwtTokensService.IsAccessValid(authHeader);
                if (!isValid)
                {
                    return Unauthorized("Неверный токен");
                }

                if (string.IsNullOrWhiteSpace(query))
                {
                    return BadRequest("Поисковый запрос обязателен");
                }

                var items = await _databaseService.SearchShopItems(query, page, pageSize);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при поиске товаров магазина по запросу: {query}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetShopItemsByCategory(
            string category,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            try
            {
                var authHeader = Request.Headers["Authorization"].ToString();
                if (string.IsNullOrEmpty(authHeader))
                {
                    return Unauthorized("Заголовок Authorization отсутствует");
                }

                var isValid = await _jwtTokensService.IsAccessValid(authHeader);
                if (!isValid)
                {
                    return Unauthorized("Неверный токен");
                }

                var items = await _databaseService.GetShopItemsByCategory(category, page, pageSize);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при получении товаров магазина по категории: {category}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}/image")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            try
            {
                var authHeader = Request.Headers["Authorization"].ToString();
                if (string.IsNullOrEmpty(authHeader))
                {
                    return Unauthorized("Заголовок Authorization отсутствует");
                }

                var isManager = await _jwtTokensService.RoleValid(authHeader, "MANAGER");
                if (!isManager)
                {
                    return Forbid("Только менеджеры могут удалять изображения");
                }

                var success = await _databaseService.DeleteShopItemImage(id);
                if (!success)
                {
                    return NotFound($"Товар магазина с id {id} не найден или не имеет изображения");
                }

                return Ok(new { message = "Изображение успешно удалено" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при удалении изображения для товара магазина с id {id}");
                return StatusCode(500, ex.Message);
            }
        }

        private string GetContentType(string path)
        {
            var extension = Path.GetExtension(path).ToLowerInvariant();
            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".webp" => "image/webp",
                _ => "application/octet-stream",
            };
        }
    }
}