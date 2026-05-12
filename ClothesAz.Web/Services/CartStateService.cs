namespace ClothesAz.Web.Services;

public class CartStateService
{
    public int ItemCount { get; private set; }
    public event Action? OnChange;

    public void SetCount(int count)
    {
        ItemCount = count;
        OnChange?.Invoke();
    }
}