using Backend.Middleware_Components.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using TechNest.Backend.Data;

namespace Backend_AA.Controllers
{

    [Route("api/ShopItems/")]
    [ApiController]
    public class ShopItemsController : ControllerBase
    {
        private readonly IConfiguration _configuration;


        public ShopItemsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //[HttpPost("Men")]
        //public async Task<IActionResult> AddItemM([FromBody] ShopItemsDTO shopItems_DTO)
        //{
        //    using (DataContext DB = new DataContext())
        //    {
        //        var dbCheck = DB.shop_items.Where(c => c.Name == shopItems_DTO.Name).FirstOrDefault();

        //        if (dbCheck != null)
        //            return BadRequest();

        //        ShopItemsDTO shopItems = new ShopItemsDTO()
        //        {
        //            Name = shopItems_DTO.Name,
        //            Color = shopItems_DTO.Color,
        //            Price = shopItems_DTO.Price,
        //            category = new string[] { "Men", shopItems_DTO.Category }
        //        };

        //        DB.shop_items.Add(shopItems);
        //        await DB.SaveChangesAsync();

        //        return Ok("Успех");

        //    }
        //}

        //[HttpPost("Woman")]
        //public async Task<IActionResult> AddItemW([FromBody] ShopItemsDTO shopItems_DTO)
        //{
        //    using (DataContext DB = new DataContext())
        //    {
        //        var dbCheck = DB.shop_items.Where(c => c.Name == shopItems_DTO.Name).FirstOrDefault();

        //        if (dbCheck != null)
        //            return BadRequest();
        //        ShopItems shopItems = new ShopItems()
        //        {
        //            Name = shopItems_DTO.Name,
        //            Color = shopItems_DTO.Color,
        //            Price = shopItems_DTO.Price,
        //            category = new string[] { "Woman", shopItems_DTO.Category }
        //        };

        //        DB.shop_items.Add(shopItems);
        //        await DB.SaveChangesAsync();

        //        return Ok("Успех");

        //    }
        //}



        //[HttpDelete("DeleteShopItems/{id}")]
        //public async Task<IActionResult> DeleteShopItems(int id)
        //{
        //    using (DataContext DB = new DataContext())
        //    {
        //        var deleteShopItems = await DB.shop_items.FindAsync(id);
        //        if (deleteShopItems == null)
        //        {
        //            return NotFound("Товар не найден.");
        //        }

        //        DB.shop_items.Remove(deleteShopItems);
        //        await DB.SaveChangesAsync();



        //        return Ok("Товар успешно удалён.");
        //    }
        //}

        //[HttpPost("ImgUpload/{itemName}")]
        //public async Task<IActionResult> ImgUpload(string itemName, IFormFile img)
        //{
        //    try
        //    {
        //        if (img == null)
        //        {
        //            return BadRequest("Нету изображения");
        //        }
        //        var filePath = Path.GetFullPath("Img/" + img.FileName);

        //        if (!Directory.Exists(Path.GetFullPath("Img/")))
        //            Directory.CreateDirectory(Path.GetFullPath("Img/"));

        //        using (var stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await img.CopyToAsync(stream);
        //        }
        //        using (DataContext DB = new DataContext())
        //        {
        //            var item = DB.shop_items.Where(item => item.Name == itemName).FirstOrDefault();
        //            if (item != null)
        //            {
        //                item.Img = filePath;
        //                await DB.SaveChangesAsync();
        //                return Ok("Изображение загружено");
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //    return BadRequest();
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetItems()
        //{
        //    using (DataContext DB = new DataContext())
        //    {
        //        List<ShopItems_Get> shopItems_Get = new List<ShopItems_Get>();
        //        var Items = DB.shop_items;
        //        foreach (var item in Items)
        //        {
        //            ShopItems_Get shopItems = new ShopItems_Get()
        //            {
        //                Id = item.Id,
        //                Name = item.Name,
        //                Color = item.Color,
        //                Price = item.Price,
        //                Img = item.Img
        //            };
        //            shopItems_Get.Add(shopItems);
        //        }
        //        return Ok(shopItems_Get);
        //    }
        //}

        //[HttpGet("Men/{category}")]
        //public async Task<IActionResult> GetItemsM(string category)
        //{
        //    using (DataContext DB = new DataContext())
        //    {
        //        List<ShopItems_Get> shopItems_Get = new List<ShopItems_Get>();
        //        var Items = DB.shop_items.Where(c => c.category[0] == "Men" && c.category[1] == category);
        //        foreach (var item in Items)
        //        {
        //            ShopItems_Get shopItems = new ShopItems_Get()
        //            {
        //                Id = item.Id,
        //                Name = item.Name,
        //                Color = item.Color,
        //                Price = item.Price,
        //                Img = item.Img
        //            };
        //            shopItems_Get.Add(shopItems);
        //        }
        //        return Ok(shopItems_Get);
        //    }
        //}

        //[HttpGet("Woman/{category}")]
        //public async Task<IActionResult> GetItemsW(string category)
        //{
        //    using (DataContext DB = new DataContext())
        //    {
        //        List<ShopItems_Get> shopItems_Get = new List<ShopItems_Get>();
        //        var Items = DB.shop_items.Where(c => c.category[0] == "Woman" && c.category[1] == category);
        //        foreach (var item in Items)
        //        {
        //            ShopItems_Get shopItems = new ShopItems_Get()
        //            {
        //                Id = item.Id,
        //                Name = item.Name,
        //                Color = item.Color,
        //                Price = item.Price,
        //                Img = item.Img
        //            };
        //            shopItems_Get.Add(shopItems);
        //        }
        //        return Ok(shopItems_Get);
        //    }
        //}

        //[HttpGet("GetImage")]
        //public async Task<IActionResult> GetImage([FromHeader] string filePath)
        //{
        //    try
        //    {
        //        var file = new FileStream(filePath, FileMode.Open, FileAccess.Read);

        //        return File(file, "image/jpeg");
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}
    }
}