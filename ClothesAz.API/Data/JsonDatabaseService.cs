using System.Text.Json;
using ClothesAz.API.Models;

namespace ClothesAz.API.Data;

public class JsonDatabaseService
{
    private readonly string _path;

    private readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };

    private readonly object _lock = new();

    public JsonDatabaseService(IWebHostEnvironment env)
    {
        var dataDir = Path.Combine(env.ContentRootPath, "Data");
        Directory.CreateDirectory(dataDir);

        _path = Path.Combine(dataDir, "database.json");

        if (!File.Exists(_path))
        {
            Save(CreateSeed());
        }
    }

    // JSON faylını oxuyur
    public JsonDatabase Load()
    {
        lock (_lock)
        {
            var json = File.ReadAllText(_path);

            return JsonSerializer.Deserialize<JsonDatabase>(json, _options)
                   ?? CreateSeed();
        }
    }

    // JSON faylına yazır
    public void Save(JsonDatabase db)
    {
        lock (_lock)
        {
            File.WriteAllText(
                _path,
                JsonSerializer.Serialize(db, _options));
        }
    }

    // Qısa property-lər
    public List<Product> Products => Load().Products;
    public List<User> Users => Load().Users;
    public List<Category> Categories => Load().Categories;
    public List<Favorite> Favorites => Load().Favorites;
    public List<CartItem> CartItems => Load().CartItems;
    public List<Order> Orders => Load().Orders;
    public List<SellerProfile> SellerProfiles => Load().SellerProfiles;
    public List<ViewedProduct> ViewedProducts => Load().ViewedProducts;
    public List<Review> Reviews => Load().Reviews;
    public List<AiMessage> AiMessages => Load().AiMessages;

    // Növbəti Id tapır
    public int NextId<T>(IEnumerable<T> list) where T : class
    {
        var prop = typeof(T).GetProperty("Id");

        var max = list
            .Select(x => (int)(prop?.GetValue(x) ?? 0))
            .DefaultIfEmpty(0)
            .Max();

        return max + 1;
    }

    // İlk demo data
    private static JsonDatabase CreateSeed() => new()
    {
        Users = new()
        {
            new User
            {
                Id = 1,
                FullName = "Demo Müştəri",
                Email = "customer@clothes.az",
                Password = "123456",
                Role = "Customer",
                Phone = "0500000000",
                City = "Bakı"
            },

            new User
            {
                Id = 2,
                FullName = "Demo Satıcı",
                Email = "seller@clothes.az",
                Password = "123456",
                Role = "Seller",
                Phone = "0551112233",
                City = "Bakı"
            }
        },

        SellerProfiles = new()
        {
            new SellerProfile
            {
                Id = 1,
                UserId = 2,
                StoreName = "TrendWear Baku",
                StoreDescription = "Gənclər üçün trend geyimlər",
                Phone = "0551112233",
                City = "Bakı",
                Address = "Nizami küç.",
                Instagram = "@trendwear.az",
                DeliveryInfo = "Bütün rayonlara çatdırılma",
                IsVerified = true
            }
        },

        Categories = new()
        {
            new Category { Id = 1, Name = "Hoodie", Icon = "🧥" },
            new Category { Id = 2, Name = "Ayaqqabı", Icon = "👟" },
            new Category { Id = 3, Name = "Şalvar", Icon = "👖" },
            new Category { Id = 4, Name = "Köynək", Icon = "👕" },
            new Category { Id = 5, Name = "Aksesuar", Icon = "🧢" }
        },

        Products = new()
        {
            new Product
            {
                Id = 1,
                SellerId = 2,
                CategoryId = 1,
                Name = "Qara Oversize Hoodie",
                Description = "Rahat, qalın parça, gündəlik streetwear stil.",
                Price = 59,
                DiscountPrice = 49,
                Size = "S,M,L,XL",
                Color = "Qara",
                Brand = "ClothesAz",
                Gender = "Unisex",
                Stock = 20,
                ImageUrl = "https://images.unsplash.com/photo-1556821840-3a63f95609a7?w=700",
                IsTrend = true,
                IsFeatured = true
            },

            new Product
            {
                Id = 2,
                SellerId = 2,
                CategoryId = 2,
                Name = "Ağ Sneaker",
                Description = "Minimal dizayn, gündəlik istifadə üçün rahat sneaker.",
                Price = 89,
                DiscountPrice = 69,
                Size = "40,41,42,43",
                Color = "Ağ",
                Brand = "Urban",
                Gender = "Unisex",
                Stock = 12,
                ImageUrl = "https://images.unsplash.com/photo-1549298916-b41d501d3772?w=700",
                IsTrend = true
            },

            new Product
            {
                Id = 3,
                SellerId = 2,
                CategoryId = 3,
                Name = "Baggy Jeans",
                Description = "Yeni sezon baggy kəsim cins şalvar.",
                Price = 74,
                Size = "28,30,32,34",
                Color = "Mavi",
                Brand = "DenimPro",
                Gender = "Kişi",
                Stock = 18,
                ImageUrl = "https://images.unsplash.com/photo-1541099649105-f69ad21f3246?w=700",
                IsFeatured = true
            },

            new Product
            {
                Id = 4,
                SellerId = 2,
                CategoryId = 4,
                Name = "Basic White T-Shirt",
                Description = "100% pambıq basic ağ köynək.",
                Price = 24,
                DiscountPrice = 19,
                Size = "S,M,L",
                Color = "Ağ",
                Brand = "BasicLine",
                Gender = "Unisex",
                Stock = 40,
                ImageUrl = "https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?w=700"
            }
        },

        Favorites = new(),
        CartItems = new(),
        ViewedProducts = new(),
        Orders = new(),
        Reviews = new(),
        AiMessages = new()
    };
}