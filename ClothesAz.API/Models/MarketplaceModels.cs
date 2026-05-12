namespace ClothesAz.API.Models;

public class User
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = "Customer";
    public string Phone { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class SellerProfile
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string StoreName { get; set; } = string.Empty;
    public string StoreDescription { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Instagram { get; set; } = string.Empty;
    public string DeliveryInfo { get; set; } = "Bütün rayonlara çatdırılma";
    public bool IsVerified { get; set; }
}

public class Category { public int Id { get; set; } public string Name { get; set; } = string.Empty; public string Icon { get; set; } = "🛍️"; }

public class Product
{
    public int Id { get; set; }
    public int SellerId { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal? DiscountPrice { get; set; }
    public string Size { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string Gender { get; set; } = "Unisex";
    public int Stock { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsTrend { get; set; }
    public bool IsFeatured { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class Favorite { public int Id { get; set; } public int UserId { get; set; } public int ProductId { get; set; } }
public class CartItem { public int Id { get; set; } public int UserId { get; set; } public int ProductId { get; set; } public int Quantity { get; set; } = 1; }
public class ViewedProduct { public int Id { get; set; } public int UserId { get; set; } public int ProductId { get; set; } public DateTime ViewedAt { get; set; } = DateTime.UtcNow; }
public class Review { public int Id { get; set; } public int UserId { get; set; } public int ProductId { get; set; } public int Rating { get; set; } public string Comment { get; set; } = string.Empty; public DateTime CreatedAt { get; set; } = DateTime.UtcNow; }
public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "Hazırlanır";
    public string DeliveryAddress { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public List<OrderItem> Items { get; set; } = new();
}
public class OrderItem { public int ProductId { get; set; } public string ProductName { get; set; } = string.Empty; public int Quantity { get; set; } public decimal UnitPrice { get; set; } }
public class AiMessage { public int Id { get; set; } public int UserId { get; set; } public string Question { get; set; } = string.Empty; public string Answer { get; set; } = string.Empty; public DateTime CreatedAt { get; set; } = DateTime.UtcNow; }

public class JsonDatabase
{
    public List<User> Users { get; set; } = new();
    public List<SellerProfile> SellerProfiles { get; set; } = new();
    public List<Category> Categories { get; set; } = new();
    public List<Product> Products { get; set; } = new();
    public List<CartItem> CartItems { get; set; } = new();
    public List<Favorite> Favorites { get; set; } = new();
    public List<ViewedProduct> ViewedProducts { get; set; } = new();
    public List<Order> Orders { get; set; } = new();
    public List<Review> Reviews { get; set; } = new();
    public List<AiMessage> AiMessages { get; set; } = new();
}
