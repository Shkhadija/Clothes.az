using Microsoft.JSInterop;

namespace ClothesAz.Web.Services;

public class AuthService
{
    private readonly IJSRuntime _js;
    private readonly ApiService _api;

    public string? Token { get; private set; }
    public int UserId { get; private set; }
    public string? UserName { get; private set; }
    public string? UserRole { get; private set; }

    public bool IsLoggedIn => !string.IsNullOrWhiteSpace(Token);

    public event Action? OnChange;

    public AuthService(IJSRuntime js, ApiService api)
    {
        _js = js;
        _api = api;
    }

    public async Task InitAsync()
    {
        try
        {
            Token = await _js.InvokeAsync<string?>("localStorage.getItem", "token");
            UserName = await _js.InvokeAsync<string?>("localStorage.getItem", "userName");
            UserRole = await _js.InvokeAsync<string?>("localStorage.getItem", "userRole");

            var id = await _js.InvokeAsync<string?>("localStorage.getItem", "userId");
            int.TryParse(id, out var uid);
            UserId = uid;

            // Əgər token boşdursa, istifadəçi login sayılmır
            if (string.IsNullOrWhiteSpace(Token))
            {
                Token = null;
                UserId = 0;
                UserName = null;
                UserRole = null;
                return;
            }

            // Token varsa API-yə göndər
            _api.SetToken(Token);
        }
        catch
        {
            // Hər hansı xəta olsa, istifadəçini guest et
            Token = null;
            UserId = 0;
            UserName = null;
            UserRole = null;
        }

        OnChange?.Invoke();
    }

    public async Task LoginAsync(AuthResponse res)
    {
        Token = res.Token;
        UserId = res.UserId;
        UserName = res.FullName;
        UserRole = res.Role;

        _api.SetToken(res.Token);

        await _js.InvokeVoidAsync("localStorage.setItem", "token", res.Token);
        await _js.InvokeVoidAsync("localStorage.setItem", "userId", res.UserId.ToString());
        await _js.InvokeVoidAsync("localStorage.setItem", "userName", res.FullName);
        await _js.InvokeVoidAsync("localStorage.setItem", "userRole", res.Role);

        OnChange?.Invoke();
    }

    public async Task LogoutAsync()
    {
        Token = null;
        UserId = 0;
        UserName = null;
        UserRole = null;

        // Təkcə auth məlumatlarını sil
        await _js.InvokeVoidAsync("localStorage.removeItem", "token");
        await _js.InvokeVoidAsync("localStorage.removeItem", "userId");
        await _js.InvokeVoidAsync("localStorage.removeItem", "userName");
        await _js.InvokeVoidAsync("localStorage.removeItem", "userRole");

        _api.SetToken(null);

        OnChange?.Invoke();
    }
}