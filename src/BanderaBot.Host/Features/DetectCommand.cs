using BanderaBot.Host.Common.Utils;
using BanderaBot.Host.Resources;
using BanderaBot.Host.Services.Abstractions;
using BanderaBot.Host.Services.Models;
using MediatR;
using Telegram.Bot;

namespace BanderaBot.Host.Features;

public record DetectCommand(string? Text, int MessageId, long ChatId) : IRequest
{
    public class DetectCommandHandler : IRequestHandler<DetectCommand>
    {
        private readonly IAzureTranslatorClient _client;
        private readonly ITelegramBotClient _botClient;

        public DetectCommandHandler(IAzureTranslatorClient client, ITelegramBotClient botClient)
        {
            _client = client;
            _botClient = botClient;
        }
        
        public async Task Handle(DetectCommand command, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(command.Text))
                return;

            var detectionResult =
                await _client.DetectLanguageAsync(new[] { new DetectRequest(command.Text) }, cancellationToken);

            if (detectionResult.Any(x => x.Language == "ru" && x.Score > 0.7))
            {
                var randomBlameIndex = Rand.Default.Next(0, Blames.List.Length - 1);
                
                await _botClient.SendTextMessageAsync(command.ChatId,
                    Blames.List[randomBlameIndex],
                    replyToMessageId: command.MessageId,
                    cancellationToken: cancellationToken);
            }
        }
    }
}