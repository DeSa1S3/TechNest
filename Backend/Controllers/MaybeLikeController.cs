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
    [Route("api/maybe-like")]
    [ApiController]
    public class MaybeLikeController : ControllerBase
    {
        private readonly ILogger<MaybeLikeController> _logger;
        private readonly IJwtTokensService _jwtTokensService;
        private readonly IDatabaseService _databaseService;

        public MaybeLikeController(ILogger<MaybeLikeController> logger, IJwtTokensService jwtTokensService,IDatabaseService databaseService)
        {
            _logger = logger;
            _jwtTokensService = jwtTokensService;
            _databaseService = databaseService;
        }

        [HttpGet("random")]
        public async Task<IActionResult> GetRandomItems([FromQuery] int count = 8)
        {
            try
            {
                if (count <= 0 || count > 50)
                {
                    return BadRequest("Число должно быть в диапазоне от 1 до 50.");
                }

                var items = await _databaseService.GetRandomMaybeLikeItems(count);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка, возможно, связана с получением случайных элементов.");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemById(int id)
        {
            try
            {
                var item = await _databaseService.GetMaybeLikeItemById(id);
                if (item == null)
                {
                    return NotFound($"Возможно, это означает, что элемент с идентификатором {id} не найден.");
                }

                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при получении элемента с идентификатором {id}.");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchItems(
            [FromQuery] string query,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(query))
                {
                    return BadRequest("Требуется поисковый запрос");
                }

                var items = await _databaseService.SearchMaybeLikeItems(query, page, pageSize);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка поиска может быть связана с элементами, содержащими запрос: {query}.");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] MaybeLikeDTO dto)
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
                    return Forbid("Возможно, только менеджеры могут создавать подобные товары.");
                }

                if (dto.Price <= 0)
                {
                    return BadRequest("Цена должна быть больше 0.");
                }

                if (dto.Discount.HasValue && (dto.Discount < 0 || dto.Discount > 100))
                {
                    return BadRequest("Скидка должна быть в диапазоне от 0 до 100.");
                }

                var createdItem = await _databaseService.CreateMaybeLikeItem(dto);
                return CreatedAtAction(nameof(GetItemById), new { id = createdItem.Id }, createdItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при создании, возможно, подобного элемента.");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, [FromBody] MaybeLikeUpdateDTO dto)
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
                    return Forbid("Возможно, обновлять такие товары могут только менеджеры.");
                }

                if (dto.Price.HasValue && dto.Price <= 0)
                {
                    return BadRequest("Цена должна быть больше 0.");
                }

                if (dto.Discount.HasValue && (dto.Discount < 0 || dto.Discount > 100))
                {
                    return BadRequest("Скидка должна быть в диапазоне от 0 до 100.");
                }

                var updatedItem = await _databaseService.UpdateMaybeLikeItem(id, dto);
                if (updatedItem == null)
                {
                    return NotFound($"Возможно, это означает, что элемент с идентификатором {id} не найден.");
                }

                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка обновления, возможно, связана с элементом с идентификатором {id}.");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
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
                    return Forbid("Удалять, возможно, похожие элементы могут только менеджеры.");
                }

                var success = await _databaseService.DeleteMaybeLikeItem(id);
                if (!success)
                {
                    return NotFound($"Возможно, это означает, что элемент с идентификатором {id} не найден.");
                }

                return Ok(new { message = $"Возможно, это означает, что элемент с идентификатором {id} был успешно удален." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при удалении, возможно, элемента с идентификатором {id}.");
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

                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (string.IsNullOrEmpty(extension) || !allowedExtensions.Contains(extension))
                {
                    return BadRequest("Недопустимый тип файла. Допустимые типы: jpg, jpeg, png, gif, webp");
                }

                if (file.Length > 5 * 1024 * 1024)
                {
                    return BadRequest("Размер файла слишком велик. Максимальный размер — 5 МБ.");
                }

                var fileName = $"maybelike_{id}_{DateTime.UtcNow.Ticks}{extension}";
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "maybe-like");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var imageUrl = $"/uploads/maybe-like/{fileName}";
                var success = await _databaseService.UpdateMaybeLikeImage(id, imageUrl);

                if (!success)
                {
                    return NotFound($"Возможно, это означает, что элемент с идентификатором  {id}  не найден.");
                }

                return Ok(new
                {
                    message = "Изображение успешно загружено",
                    imageUrl = imageUrl
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при загрузке изображения для элемента с идентификатором {id}.");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}/image")]
        public async Task<IActionResult> GetImage(int id)
        {
            try
            {
                var item = await _databaseService.GetMaybeLikeItemById(id);
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
                _logger.LogError(ex, $"Ошибка при получении изображения для элемента с идентификатором {id}.");
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

                var success = await _databaseService.DeleteMaybeLikeImage(id);
                if (!success)
                {
                    return NotFound($"Возможно, это элемент с идентификатором {id} не найден или у него нет изображения.");
                }

                return Ok(new { message = "Изображение успешно удалено" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при удалении изображения для элемента с идентификатором {id}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetItemsByCategory(
            string category,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] bool activeOnly = true)
        {
            try
            {
                var items = await _databaseService.GetAllMaybeLikeItems(page, pageSize, category, activeOnly);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при получении элементов, возможно, связанных с категорией: {category}");
                return StatusCode(500, "Внутренняя ошибка сервера");
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