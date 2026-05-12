using ClothesAz.API.Data;
using Microsoft.AspNetCore.Mvc;
namespace ClothesAz.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class OrdersController:ControllerBase{private readonly JsonDatabaseService _db; public OrdersController(JsonDatabaseService db)=>_db=db; [HttpGet("user/{userId}")] public IActionResult UserOrders(int userId)=>Ok(_db.Load().Orders.Where(o=>o.UserId==userId).OrderByDescending(o=>o.OrderDate)); [HttpGet("seller/{sellerId}")] public IActionResult SellerOrders(int sellerId){var d=_db.Load(); var ids=d.Products.Where(p=>p.SellerId==sellerId).Select(p=>p.Id).ToHashSet(); return Ok(d.Orders.Where(o=>o.Items.Any(i=>ids.Contains(i.ProductId))).OrderByDescending(o=>o.OrderDate));}}
