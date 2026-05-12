using System.Net.Http.Json;
namespace ClothesAz.Web.Services;
public class ApiService
{ private readonly HttpClient _http; public ApiService(HttpClient http)=>_http=http;
public void SetToken(string token)=>_http.DefaultRequestHeaders.Authorization=new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",token);
public async Task<T?> GetAsync<T>(string url)=>await _http.GetFromJsonAsync<T>(url);
public async Task<HttpResponseMessage> PostAsync<T>(string url,T data)=>await _http.PostAsJsonAsync(url,data);
public async Task<HttpResponseMessage> DeleteAsync(string url)=>await _http.DeleteAsync(url);
}
