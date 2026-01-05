using Backend.Interfaces;
using Backend.Middleware_Components.DTO;
using Backend.Middleware_Components.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/News")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly ILogger<NewsController> _logger;
        private readonly IJwtTokensService _jwtTokensService;
        private readonly IDatabaseService _databaseService;

        public NewsController(ILogger<NewsController> logger, IJwtTokensService jwtTokensService, IDatabaseService databaseService)
        {
            _logger = logger;
            _jwtTokensService = jwtTokensService;
            _databaseService = databaseService;
        }
        
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetNewsById(int id)
        {
            try
            {
                var news = await _databaseService.GetNewsById(id);
                if (news == null)
                {
                    return NotFound($"Новость с id {id} не найдена");
                }

                return Ok(news);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при получении новости с id {id}");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }
        // Только менеджер
        [HttpPost]
        public async Task<IActionResult> CreateNews([FromBody] NewsDTO dto)
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
                    return Forbid("Только менеджеры могут создавать новости");
                }

                var userId = await _jwtTokensService.GetTokenUserId(authHeader);

                var createdNews = await _databaseService.CreateNews(dto, userId);
                return CreatedAtAction(nameof(GetNewsById), new { id = createdNews.Id }, createdNews);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при создании новости");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNews(int id, [FromBody] NewsUpdateDTO dto)
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
                    return Forbid("Только менеджеры могут обновлять новости");
                }

                var updatedNews = await _databaseService.UpdateNews(id, dto);
                if (updatedNews == null)
                {
                    return NotFound($"Новость с id {id} не найдена");
                }

                return Ok(updatedNews);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при обновлении новости с id {id}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNews(int id)
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
                    return Forbid("Только менеджеры могут удалять новости");
                }

                var success = await _databaseService.DeleteNews(id);
                if (!success)
                {
                    return NotFound($"Новость с id {id} не найдена");
                }

                return Ok(new { message = $"Новость с id {id} успешно удалена" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при удалении новости с id {id}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/Upload-image")]
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

                var fileName = $"news_{id}_{DateTime.UtcNow.Ticks}{extension}";
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "news");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var imageUrl = $"/uploads/news/{fileName}";
                var success = await _databaseService.UpdateNewsImage(id, imageUrl);

                if (!success)
                {
                    return NotFound($"Новость с id {id} не найдена");
                }

                return Ok(new
                {
                    message = "Изображение успешно загружено",
                    imageUrl = imageUrl
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при загрузке изображения для новости с id {id}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}/Image")]
        [AllowAnonymous]
        public async Task<IActionResult> GetImage(int id)
        {
            try
            {
                var news = await _databaseService.GetNewsById(id);
                if (news == null || string.IsNullOrEmpty(news.Img))
                {
                    return NotFound("Изображение не найдено");
                }

                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", news.Img.TrimStart('/'));

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
                _logger.LogError(ex, $"Ошибка при получении изображения для новости с id {id}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}/Image")]
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

                var success = await _databaseService.DeleteNewsImage(id);
                if (!success)
                {
                    return NotFound($"Новость с id {id} не найдена или не имеет изображения");
                }

                return Ok(new { message = "Изображение успешно удалено" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при удалении изображения для новости с id {id}");
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