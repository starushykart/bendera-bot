using System.ComponentModel.DataAnnotations;

namespace BanderaBot.Host.Common.Configuration;

public class TranslatorConfiguration
{
    public const string ClientName = "Translator";
    public const string ConfigSection = "TranslatorConfiguration";
    
    [Required]
    public string BaseAddress { get; init; } = default!;
    
    [Required]
    public string Key { get; init; } = default!;

    [Required]
    public string Region { get; set; } = default!;
}