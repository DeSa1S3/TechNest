using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using TechNest.Backend.Data;

namespace Backend.Services
{
    public abstract class BaseService
    {
        protected readonly DataContext _context;
        protected readonly ILogger _logger;

        protected BaseService(DataContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        protected async Task<T?> FindEntityById<T>(int id) where T : class
        {
            return await _context.Set<T>().FindAsync(id);
        }

        protected async Task<T?> FindEntityById<T>(Guid id) where T : class
        {
            return await _context.Set<T>().FindAsync(id);
        }

        protected void DeleteImageFile(string imageUrl)
        {
            try
            {
                if (!string.IsNullOrEmpty(imageUrl))
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, $"Failed to delete image file: {imageUrl}");
            }
        }
    }
}