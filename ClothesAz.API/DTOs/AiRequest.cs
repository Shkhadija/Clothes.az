namespace ClothesAz.API.DTOs;

public class AiRequest
{
    public int UserId { get; set; }
    public string Text { get; set; } = "";
    public string ImageUrl { get; set; } = "";
}