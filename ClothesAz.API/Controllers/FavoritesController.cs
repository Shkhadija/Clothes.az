using ClothesAz.API.Data;
using ClothesAz.API.Models;
using Microsoft.AspNetCore.Mvc;
namespace ClothesAz.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class FavoritesController:ControllerBase
{ private readonly JsonDatabaseService _db; public FavoritesController(JsonDatabaseService db)=>_db=db;
[HttpGet("{userId}")] public IActionResult Get(int userId){var d=_db.Load(); var ids=d.Favorites.Where(f=>f.UserId==userId).Select(f=>f.ProductId).ToHashSet(); return Ok(d.Products.Where(p=>ids.Contains(p.Id)));}
[HttpPost("{userId}/{productId}")] public IActionResult Toggle(int userId,int productId){var d=_db.Load(); var f=d.Favorites.FirstOrDefault(x=>x.UserId==userId&&x.ProductId==productId); if(f==null)d.Favorites.Add(new Favorite{Id=_db.NextId(d.Favorites),UserId=userId,ProductId=productId}); else d.Favorites.Remove(f); _db.Save(d); return Ok();}}
