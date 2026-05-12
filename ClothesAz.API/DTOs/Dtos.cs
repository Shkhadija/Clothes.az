namespace ClothesAz.API.DTOs;

public record RegisterDto(string FullName, string Email, string Password, string Role, string Phone, string City, string Address, string StoreName = "", string StoreDescription = "", string Instagram = "");
public record LoginDto(string Email, string Password);
public record AuthResponseDto(string Token, int UserId, string FullName, string Email, string Role);
public record ProductDto(int Id, int SellerId, int CategoryId, string Name, string Description, decimal Price, decimal? DiscountPrice, string Size, string Color, string Brand, string Gender, int Stock, string ImageUrl, bool IsTrend, bool IsFeatured, string CategoryName, string StoreName);
public record CreateProductDto(int SellerId, int CategoryId, string Name, string Description, decimal Price, decimal? DiscountPrice, string Size, string Color, string Brand, string Gender, int Stock, string ImageUrl, bool IsTrend, bool IsFeatured);
public record AiRequestDto(int UserId, string Text, string ImageUrl = "");
public record CheckoutDto(int UserId, string DeliveryAddress);
