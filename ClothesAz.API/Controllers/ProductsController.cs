using ClothesAz.API.Data;
using ClothesAz.API.DTOs;
using ClothesAz.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClothesAz.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly JsonDatabaseService _db;
    public ProductsController(JsonDatabaseService db) => _db = db;
    private ProductDto Map(Product p, JsonDatabase d) => new(p.Id,p.SellerId,p.CategoryId,p.Name,p.Description,p.Price,p.DiscountPrice,p.Size,p.Color,p.Brand,p.Gender,p.Stock,p.ImageUrl,p.IsTrend,p.IsFeatured,d.Categories.FirstOrDefault(c=>c.Id==p.CategoryId)?.Name??"",d.SellerProfiles.FirstOrDefault(s=>s.UserId==p.SellerId)?.StoreName??"Satıcı");

    [HttpGet] public IActionResult Get([FromQuery]string? q,[FromQuery]int? categoryId,[FromQuery]bool? trend)
    {
        var d=_db.Load(); IEnumerable<Product> ps=d.Products;
        if(!string.IsNullOrWhiteSpace(q)) ps=ps.Where(p => (p.Name+" "+p.Description+" "+p.Brand+" "+p.Color+" "+p.Gender).Contains(q, StringComparison.OrdinalIgnoreCase));
        if(categoryId.HasValue) ps=ps.Where(p=>p.CategoryId==categoryId);
        if(trend==true) ps=ps.Where(p=>p.IsTrend);
        return Ok(ps.OrderByDescending(p=>p.IsFeatured).ThenByDescending(p=>p.CreatedAt).Select(p=>Map(p,d)));
    }

    [HttpGet("{id}")] public IActionResult GetById(int id,[FromQuery]int? userId)
    {
        var d=_db.Load(); var p=d.Products.FirstOrDefault(x=>x.Id==id); if(p==null) return NotFound();
        if(userId.HasValue && !d.ViewedProducts.Any(v=>v.UserId==userId && v.ProductId==id)) d.ViewedProducts.Add(new ViewedProduct{Id=_db.NextId(d.ViewedProducts),UserId=userId.Value,ProductId=id});
        _db.Save(d); return Ok(Map(p,d));
    }

    [HttpGet("seller/{sellerId}")] public IActionResult SellerProducts(int sellerId) { var d=_db.Load(); return Ok(d.Products.Where(p=>p.SellerId==sellerId).Select(p=>Map(p,d))); }

    [HttpPost] public IActionResult Create(CreateProductDto dto)
    {
        var d=_db.Load(); var p=new Product{Id=_db.NextId(d.Products), SellerId=dto.SellerId, CategoryId=dto.CategoryId, Name=dto.Name, Description=dto.Description, Price=dto.Price, DiscountPrice=dto.DiscountPrice, Size=dto.Size, Color=dto.Color, Brand=dto.Brand, Gender=dto.Gender, Stock=dto.Stock, ImageUrl=dto.ImageUrl, IsTrend=dto.IsTrend, IsFeatured=dto.IsFeatured};
        d.Products.Add(p); _db.Save(d); return Ok(Map(p,d));
    }

    [HttpDelete("{id}")] public IActionResult Delete(int id){var d=_db.Load(); var p=d.Products.FirstOrDefault(x=>x.Id==id); if(p==null)return NotFound(); d.Products.Remove(p); _db.Save(d); return Ok();}
}
