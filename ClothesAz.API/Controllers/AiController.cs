using ClothesAz.API.Data;
using ClothesAz.API.DTOs;
using ClothesAz.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
namespace ClothesAz.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AiController:ControllerBase
{private readonly JsonDatabaseService _db; public AiController(JsonDatabaseService db)=>_db=db;
    [HttpPost("assistant")]
    public IActionResult Assistant([FromBody] AiRequest request)
    {
        var text = (request.Text ?? "").ToLower().Trim();

        if (string.IsNullOrWhiteSpace(text))
        {
            return Ok(new
            {
                Answer = "Zəhmət olmasa axtardığınız geyimi yazın.",
                Products = new List<Product>()
            });
        }

        var products = _db.Products
            .Where(p =>
                p.Name.ToLower().Contains(text) ||
                p.Description.ToLower().Contains(text) ||
                p.Color.ToLower().Contains(text) ||
                p.Brand.ToLower().Contains(text))
            .Take(6)
            .ToList();

        string answer;

        if (products.Any())
        {
            answer = $"Sorğunuza uyğun {products.Count} məhsul seçdim. Ümid edirəm bəyənəcəksiniz.";
        }
        else
        {
            answer = "Sorğunuza uyğun məhsul tapılmadı. Fərqli rəng, stil və ya kateqoriya yazın.";
        }

        return Ok(new
        {
            Answer = answer,
            Products = products
        });
    }
    [HttpPost("seller-copy")] public IActionResult SellerCopy([FromBody]CreateProductDto dto){return Ok(new{title=dto.Name, description=$"{dto.Name} — {dto.Color} rəngdə, {dto.Gender} üçün uyğun, rahat və trend seçim. Ölçülər: {dto.Size}. Məhdud stok: {dto.Stock} ədəd.", hashtags=new[]{"#clothesaz","#geyim","#trend","#azerbaijan",$"#{dto.Color}"}});} }
