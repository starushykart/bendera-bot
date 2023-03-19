using System.ComponentModel.DataAnnotations;

namespace BanderaBot.Host.Common.Configuration;

public class BotConfiguration
{
    public const string ClientName = "Telegram";
    public const string ConfigSection = "BotConfiguration";

    [Required]
    public string BotToken { get; init; } = default!;
    
    [Required]
    public string HostAddress { get; init; } = default!;
    
    [Required]
    public string Route { get; init; } = default!;
    
    [Required]
    public Guid SecretToken { get; init; } = Guid.NewGuid();
    
    public string SecretRoute => $"{Route}/{SecretToken}";
}