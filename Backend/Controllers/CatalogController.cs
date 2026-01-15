using Backend.Interfaces;
using Backend.Middleware_Components.DTO;
using Backend.Middleware_Components.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    //Админ и редактор
    [Route("api/Catalog")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ILogger<CatalogController> _logger;
        private readonly IJwtTokensService _jwtTokensService;
        private readonly ICatalogService _catalogService;

        public CatalogController(ILogger<CatalogController> logger,IJwtTokensService jwtTokensService, ICatalogService catalogService)
        {
            _logger = logger;
            _jwtTokensService = jwtTokensService;
            _catalogService = catalogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await _catalogService.GetAllCatalogCategories();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении всех категорий каталога.");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        [HttpGet("roots")]
        public async Task<IActionResult> GetRootCategories()
        {
            try
            {
                var categories = await _catalogService.GetRootCategories();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении корневых категорий");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        [HttpGet("parent/{parentId}/subcategories")]
        public async Task<IActionResult> GetSubCategories(int parentId)
        {
            try
            {
                var categories = await _catalogService.GetSubCategories(parentId);
                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при получении подкатегорий для родительского идентификатора {parentId}");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                var category = await _catalogService.GetCatalogCategoryById(id);
                if (category == null)
                {
                    return NotFound($"Категория с идентификатором  {id}  не найдена");
                }

                return Ok(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при получении категории каталога с идентификатором {id}");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CatalogDTO dto)
        {
            try
            {
                var authHeader = Request.Headers["Authorization"].ToString();
                if (string.IsNullOrEmpty(authHeader))
                {
                    return Unauthorized("Authorization header is missing");
                }

                var isManager = await _jwtTokensService.RoleValid(authHeader, "MANAGER");
                if (!isManager)
                {
                    return Forbid("Создавать категории каталога могут только менеджеры.");
                }

                var createdCategory = await _catalogService.CreateCatalogCategory(dto);
                return CreatedAtAction(nameof(GetCategoryById), new { id = createdCategory.Id }, createdCategory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при создании категории каталога.");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CatalogUpdateDTO dto)
        {
            try
            {
                var authHeader = Request.Headers["Authorization"].ToString();
                if (string.IsNullOrEmpty(authHeader))
                {
                    return Unauthorized("Authorization header is missing");
                }

                var isManager = await _jwtTokensService.RoleValid(authHeader, "MANAGER");
                if (!isManager)
                {
                    return Forbid("Обновлять категории каталога могут только менеджеры.");
                }

                var updatedCategory = await _catalogService.UpdateCatalogCategory(id, dto);
                if (updatedCategory == null)
                {
                    return NotFound($"Категория с идентификатором {id} не найдена");
                }

                return Ok(updatedCategory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка обновления категории каталога с идентификатором {id}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var authHeader = Request.Headers["Authorization"].ToString();
                if (string.IsNullOrEmpty(authHeader))
                {
                    return Unauthorized("Authorization header is missing");
                }

                var isManager = await _jwtTokensService.RoleValid(authHeader, "MANAGER");
                if (!isManager)
                {
                    return Forbid("Удалять категории каталога могут только менеджеры.");
                }

                var success = await _catalogService.DeleteCatalogCategory(id);
                if (!success)
                {
                    return NotFound($"Категория с идентификатором {id} не найдена");
                }

                return Ok(new { message = $"Категория с идентификатором {id} успешно удалена." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при удалении категории каталога с идентификатором {id}");
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
                    return Unauthorized("Authorization header is missing");
                }

                var isManager = await _jwtTokensService.RoleValid(authHeader, "MANAGER");
                if (!isManager)
                {
                    return Forbid("Загружать изображения могут только менеджеры.");
                }

                if (file == null || file.Length == 0)
                {
                    return BadRequest("Ни один файл не загружен");
                }

                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp", ".svg" };
                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (string.IsNullOrEmpty(extension) || !allowedExtensions.Contains(extension))
                {
                    return BadRequest("Недопустимый тип файла. Допустимые типы: jpg, jpeg, png, gif, webp, svg");
                }

                if (file.Length > 5 * 1024 * 1024)
                {
                    return BadRequest("Размер файла слишком велик. Максимальный размер — 5 МБ.");
                }

                var fileName = $"catalog_{id}_{DateTime.UtcNow.Ticks}{extension}";
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "catalog");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var imageUrl = $"/uploads/catalog/{fileName}";
                var success = await _catalogService.UpdateCatalogImage(id, imageUrl);

                if (!success)
                {
                    return NotFound($"Категория с идентификатором {id} не найдена");
                }

                return Ok(new
                {
                    message = "Изображение успешно загружено",
                    imageUrl = imageUrl
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при загрузке изображения для категории каталога с идентификатором {id}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}/image")]
        public async Task<IActionResult> GetImage(int id)
        {
            try
            {
                var category = await _catalogService.GetCatalogCategoryById(id);
                if (category == null || string.IsNullOrEmpty(category.Img))
                {
                    return NotFound("Изображение не найдено");
                }

                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", category.Img.TrimStart('/'));

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
                _logger.LogError(ex, $"Ошибка при получении изображения для категории каталога с идентификатором {id}");
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
                    return Unauthorized("Authorization header is missing");
                }

                var isManager = await _jwtTokensService.RoleValid(authHeader, "MANAGER");
                if (!isManager)
                {
                    return Forbid("Удалять изображения могут только менеджеры.");
                }

                var success = await _catalogService.DeleteCatalogImage(id);
                if (!success)
                {
                    return NotFound($"Категория с идентификатором {id} не найдена или не имеет изображения.");
                }

                return Ok(new { message = "Изображение успешно удалено" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при удалении изображения для категории каталога с идентификатором {id}");
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
                ".svg" => "image/svg+xml",
                _ => "application/octet-stream",
            };
        }
    }
}