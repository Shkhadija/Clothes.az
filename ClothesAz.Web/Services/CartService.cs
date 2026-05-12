using System;
using System.Collections.Generic;
using System.Linq;

namespace ClothesAz.Web.Services;

public class CartService
{
    // Səbətdəki bütün məhsullar
    public List<CartItem> Items { get; private set; } = new();

    // Ümumi məhsul sayı (badge üçün)
    public int TotalItems => Items.Sum(x => x.Quantity);

    // Səbət dəyişəndə UI-ni yeniləmək üçün event
    public event Action? OnChange;

    // Məhsulu səbətə əlavə et
    public void AddToCart(int productId, string color, string size)
    {
        // Eyni məhsul + eyni rəng + eyni ölçü varsa sayını artır
        var existing = Items.FirstOrDefault(x =>
            x.ProductId == productId &&
            x.Color == color &&
            x.Size == size);

        if (existing != null)
        {
            existing.Quantity++;
        }
        else
        {
            Items.Add(new CartItem
            {
                ProductId = productId,
                Color = color,
                Size = size,
                Quantity = 1
            });
        }

        NotifyStateChanged();
    }

    // Məhsul sayını 1 azalt
    public void DecreaseQuantity(int productId, string color, string size)
    {
        var item = Items.FirstOrDefault(x =>
            x.ProductId == productId &&
            x.Color == color &&
            x.Size == size);

        if (item == null)
            return;

        item.Quantity--;

        if (item.Quantity <= 0)
        {
            Items.Remove(item);
        }

        NotifyStateChanged();
    }

    // Məhsulu tam sil
    public void RemoveItem(int productId, string color, string size)
    {
        var item = Items.FirstOrDefault(x =>
            x.ProductId == productId &&
            x.Color == color &&
            x.Size == size);

        if (item == null)
            return;

        Items.Remove(item);

        NotifyStateChanged();
    }

    // Səbəti tam təmizlə
    public void ClearCart()
    {
        Items.Clear();
        NotifyStateChanged();
    }

    // Müəyyən məhsulun sayını qaytar
    public int GetQuantity(int productId, string color, string size)
    {
        var item = Items.FirstOrDefault(x =>
            x.ProductId == productId &&
            x.Color == color &&
            x.Size == size);

        return item?.Quantity ?? 0;
    }

    // Event-i işə sal
    private void NotifyStateChanged()
    {
        OnChange?.Invoke();
    }
}

public class CartItem
{
    public int ProductId { get; set; }

    public string Color { get; set; } = "";

    public string Size { get; set; } = "";

    public int Quantity { get; set; }
}