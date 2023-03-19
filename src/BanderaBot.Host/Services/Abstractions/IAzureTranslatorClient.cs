using BanderaBot.Host.Services.Models;
using Refit;

namespace BanderaBot.Host.Services.Abstractions;

public interface IAzureTranslatorClient
{
    [Post("/detect?api-version=3.0")]
    Task<IEnumerable<DetectedResponse>> DetectLanguageAsync(IEnumerable<DetectRequest> request, CancellationToken cancellationToken);
}