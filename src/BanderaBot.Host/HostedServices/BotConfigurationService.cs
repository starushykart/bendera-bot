using BanderaBot.Host.Common.Configuration;
using BanderaBot.Host.Common.Utils;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace BanderaBot.Host.HostedServices;

public class BotConfigurationService : IHostedService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<BotConfigurationService> _logger;

    public BotConfigurationService(IServiceScopeFactory serviceScopeFactory, ILogger<BotConfigurationService> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = _serviceScopeFactory.CreateAsyncScope();
        var client = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();
        var config = scope.ServiceProvider.GetRequiredService<IOptions<BotConfiguration>>();

        var webhookAddress = $"{config.Value.HostAddress}{config.Value.SecretRoute}";
        _logger.LogInformation("Setting webhook: {WebhookAddress}", webhookAddress);

        await client.SetMyCommandsAsync(BotCommands.List, cancellationToken: cancellationToken);
        await client.SetWebhookAsync(
            url: webhookAddress,
            allowedUpdates: new[] { UpdateType.Message },
            dropPendingUpdates: true,
            cancellationToken: cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await using var scope = _serviceScopeFactory.CreateAsyncScope();
        var client = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();

        _logger.LogInformation("Removing webhook");
        await client.DeleteWebhookAsync(cancellationToken: cancellationToken);
    }
}