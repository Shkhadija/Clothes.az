using ClothesAz.API.Data;
using ClothesAz.API.DTOs;
using ClothesAz.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClothesAz.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MarketplaceController : ControllerBase
{
    private readonly JsonDatabaseService _db;
    public MarketplaceController(JsonDatabaseService db)=>_db=db;
    [HttpGet("categories")] public IActionResult Categories()=>Ok(_db.Load().Categories);
    [HttpGet("home/{userId}")] public IActionResult Home(int userId)
    {
        var d=_db.Load();
        var viewedIds=d.ViewedProducts.Where(v=>v.UserId==userId).OrderByDescending(v=>v.ViewedAt).Select(v=>v.ProductId).Take(8).ToHashSet();
        return Ok(new { banners=new[]{"Yeni sezon geyimlərdə 40%-dək endirim","AI ilə şəklə görə oxşar geyimi tap","Satıcı ol və mağazanı 5 dəqiqəyə aç"}, categories=d.Categories, trend=d.Products.Where(p=>p.IsTrend).Take(12), viewed=d.Products.Where(p=>viewedIds.Contains(p.Id)), featured=d.Products.Where(p=>p.IsFeatured).Take(12), stores=d.SellerProfiles.Take(8) });
    }
    [HttpGet("seller-dashboard/{sellerId}")] public IActionResult Dashboard(int sellerId)
    {
        var d=_db.Load(); var products=d.Products.Where(p=>p.SellerId==sellerId).ToList(); var ids=products.Select(p=>p.Id).ToHashSet();
        var orders=d.Orders.Where(o=>o.Items.Any(i=>ids.Contains(i.ProductId))).ToList();
        return Ok(new { store=d.SellerProfiles.FirstOrDefault(s=>s.UserId==sellerId), productCount=products.Count, activeStock=products.Sum(p=>p.Stock), orderCount=orders.Count, revenue=orders.Sum(o=>o.Items.Where(i=>ids.Contains(i.ProductId)).Sum(i=>i.UnitPrice*i.Quantity)), lowStock=products.Where(p=>p.Stock<=3) });
    }
}
