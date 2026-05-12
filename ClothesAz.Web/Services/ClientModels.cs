using System;
using System.Collections.Generic;

namespace ClothesAz.Web.Services;

public class AuthResponse
{
    public string Token { get; set; } = "";
    public int UserId { get; set; }
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Role { get; set; } = "Customer";
}

public class RegisterRequest
{
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
    public string Role { get; set; } = "Customer";
    public string Phone { get; set; } = "";
    public string City { get; set; } = "";
    public string Address { get; set; } = "";
    public string StoreName { get; set; } = "";
    public string StoreDescription { get; set; } = "";
    public string Instagram { get; set; } = "";
}

public class LoginRequest
{
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
}

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Icon { get; set; } = "🛍️";
}

public class Product
{
    public int Id { get; set; }
    public int SellerId { get; set; }
    public int CategoryId { get; set; }

    public string Name { get; set; } = "";
    public string Description { get; set; } = "";

    public decimal Price { get; set; }
    public decimal? DiscountPrice { get; set; }

    public string Size { get; set; } = "";
    public string Color { get; set; } = "";
    public string Brand { get; set; } = "";
    public string Gender { get; set; } = "";

    public int Stock { get; set; }
    public string ImageUrl { get; set; } = "";

    public bool IsTrend { get; set; }
    public bool IsFeatured { get; set; }

    public bool IsFavorite { get; set; }

    public string CategoryName { get; set; } = "";
    public string StoreName { get; set; } = "";

    public decimal CurrentPrice => DiscountPrice ?? Price;
}

public class CreateProduct
{
    public int SellerId { get; set; }
    public int CategoryId { get; set; } = 1;
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public decimal Price { get; set; }
    public decimal? DiscountPrice { get; set; }
    public string Size { get; set; } = "S,M,L";
    public string Color { get; set; } = "";
    public string Brand { get; set; } = "";
    public string Gender { get; set; } = "Unisex";
    public int Stock { get; set; } = 1;
    public string ImageUrl { get; set; } = "";
    public bool IsTrend { get; set; }
    public bool IsFeatured { get; set; }
}

public class CartLine
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public Product? Product { get; set; }
    public decimal LineTotal { get; set; }
}

public class AiResponse
{
    public string Answer { get; set; } = "";
    public List<Product> Products { get; set; } = new();
}

public class HomeResponse
{
    public string[] Banners { get; set; } = Array.Empty<string>();
    public List<Category> Categories { get; set; } = new();
    public List<Product> Trend { get; set; } = new();
    public List<Product> Viewed { get; set; } = new();
    public List<Product> Featured { get; set; } = new();
}
public class SellerProfile
{
    public int Id { get; set; }
    public int UserId { get; set; }

    public string StoreName { get; set; } = "";
    public string StoreDescription { get; set; } = "";
    public string Phone { get; set; } = "";
    public string City { get; set; } = "";
    public string Address { get; set; } = "";
    public string Instagram { get; set; } = "";
    public string DeliveryInfo { get; set; } = "";
    public bool IsVerified { get; set; }
}

