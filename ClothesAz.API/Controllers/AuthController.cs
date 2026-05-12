using ClothesAz.API.Data;
using ClothesAz.API.DTOs;
using ClothesAz.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClothesAz.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly JsonDatabaseService _db;
    public AuthController(JsonDatabaseService db) => _db = db;

    [HttpPost("register")]
    public IActionResult Register(RegisterDto dto)
    {
        var data = _db.Load();
        if (data.Users.Any(u => u.Email.Equals(dto.Email, StringComparison.OrdinalIgnoreCase)))
            return BadRequest("Bu email artıq qeydiyyatdan keçib.");
        var user = new User { Id = _db.NextId(data.Users), FullName=dto.FullName, Email=dto.Email, Password=dto.Password, Role=string.IsNullOrWhiteSpace(dto.Role)?"Customer":dto.Role, Phone=dto.Phone, City=dto.City, Address=dto.Address };
        data.Users.Add(user);
        if (user.Role.Equals("Seller", StringComparison.OrdinalIgnoreCase))
        {
            data.SellerProfiles.Add(new SellerProfile { Id=_db.NextId(data.SellerProfiles), UserId=user.Id, StoreName=string.IsNullOrWhiteSpace(dto.StoreName)?$"{dto.FullName} mağazası":dto.StoreName, StoreDescription=dto.StoreDescription, Phone=dto.Phone, City=dto.City, Address=dto.Address, Instagram=dto.Instagram });
        }
        _db.Save(data);
        return Ok(new AuthResponseDto(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), user.Id, user.FullName, user.Email, user.Role));
    }

    [HttpPost("login")]
    public IActionResult Login(LoginDto dto)
    {
        var user = _db.Load().Users.FirstOrDefault(u => u.Email.Equals(dto.Email, StringComparison.OrdinalIgnoreCase) && u.Password == dto.Password);
        return user is null ? Unauthorized("Email və ya şifrə yanlışdır.") : Ok(new AuthResponseDto(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), user.Id, user.FullName, user.Email, user.Role));
    }
}
