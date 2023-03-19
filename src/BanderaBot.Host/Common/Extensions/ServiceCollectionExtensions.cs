using BanderaBot.Host.Common.Configuration;
using BanderaBot.Host.Features;
using BanderaBot.Host.HostedServices;
using BanderaBot.Host.Services.Abstractions;
using Microsoft.Extensions.Options;
using Refit;
using Telegram.Bot;

namespace BanderaBot.Host.Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBotServices(this IServiceCollection services)
    {
        return services
            .AddBotClient()
            .AddTranslatorClient()
            .AddMediatR(x => x.RegisterServicesFromAssemblyContaining<IBotCommand>())
            .AddHostedService<BotConfigurationService>();
    }

    private static IServiceCollection AddBotClient(this IServiceCollection services)
    {
        services
            .AddOptions<BotConfiguration>()
            .BindConfiguration(BotConfiguration.ConfigSection)
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        services
            .AddHttpClient(BotConfiguration.ClientName)
            .AddTypedClient<ITelegramBotClient>((client, provider) =>
            {
                var botConfig = provider.GetRequiredService<IOptions<BotConfiguration>>();
                var options = new TelegramBotClientOptions(botConfig.Value.BotToken);
                return new TelegramBotClient(options, client);
            });

        return services;
    }
    
    public static IServiceCollection AddTranslatorClient(this IServiceCollection services)
    {
        services
            .AddOptions<TranslatorConfiguration>()
            .BindConfiguration(TranslatorConfiguration.ConfigSection)
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        services
            .AddRefitClient<IAzureTranslatorClient>()
            .ConfigureHttpClient((provider, client ) =>
            {
                var config = provider.GetRequiredService<IOptions<TranslatorConfiguration>>();
                client.BaseAddress = new Uri(config.Value.BaseAddress);
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", config.Value.Key);
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Region", config.Value.Region);
            });

        return services;
    }
}