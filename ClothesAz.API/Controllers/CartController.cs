using ClothesAz.API.Data;
using ClothesAz.API.DTOs;
using ClothesAz.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClothesAz.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CartController:ControllerBase
{
    private readonly JsonDatabaseService _db; public CartController(JsonDatabaseService db)=>_db=db;
    [HttpGet("{userId}")] public IActionResult Get(int userId){var d=_db.Load(); var items=d.CartItems.Where(c=>c.UserId==userId).Select(c=>new{c.Id,c.ProductId,c.Quantity,Product=d.Products.FirstOrDefault(p=>p.Id==c.ProductId),LineTotal=(d.Products.FirstOrDefault(p=>p.Id==c.ProductId)?.DiscountPrice??d.Products.FirstOrDefault(p=>p.Id==c.ProductId)?.Price??0)*c.Quantity}); return Ok(items);}
    [HttpPost("{userId}/{productId}")] public IActionResult Add(int userId,int productId){var d=_db.Load(); var item=d.CartItems.FirstOrDefault(c=>c.UserId==userId&&c.ProductId==productId); if(item==null)d.CartItems.Add(new CartItem{Id=_db.NextId(d.CartItems),UserId=userId,ProductId=productId,Quantity=1}); else item.Quantity++; _db.Save(d); return Ok();}
    [HttpDelete("{id}")] public IActionResult Delete(int id){var d=_db.Load(); var item=d.CartItems.FirstOrDefault(x=>x.Id==id); if(item==null)return NotFound(); d.CartItems.Remove(item); _db.Save(d); return Ok();}
    [HttpPost("checkout")] public IActionResult Checkout(CheckoutDto dto){var d=_db.Load(); var cart=d.CartItems.Where(c=>c.UserId==dto.UserId).ToList(); if(!cart.Any()) return BadRequest("Səbət boşdur."); var order=new Order{Id=_db.NextId(d.Orders),UserId=dto.UserId,DeliveryAddress=dto.DeliveryAddress}; foreach(var c in cart){var p=d.Products.First(x=>x.Id==c.ProductId); var price=p.DiscountPrice??p.Price; order.Items.Add(new OrderItem{ProductId=p.Id,ProductName=p.Name,Quantity=c.Quantity,UnitPrice=price}); order.TotalAmount+=price*c.Quantity; p.Stock=Math.Max(0,p.Stock-c.Quantity);} d.Orders.Add(order); d.CartItems.RemoveAll(c=>c.UserId==dto.UserId); _db.Save(d); return Ok(order);}
}
