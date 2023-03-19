namespace BanderaBot.Host.Services.Models;

public class DetectedResponse
{
    public float Score { get; set; }
    public string Language { get; set; } = null!;
}