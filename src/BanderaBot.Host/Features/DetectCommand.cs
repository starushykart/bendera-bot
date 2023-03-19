using BanderaBot.Host.Common.Utils;
using BanderaBot.Host.Resources;
using BanderaBot.Host.Services.Abstractions;
using BanderaBot.Host.Services.Models;
using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BanderaBot.Host.Features;

public record DetectCommand(Message Message) : IRequest
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
            if (string.IsNullOrEmpty(command.Message.Text))
                return;

            var username = command.Message.From?.FirstName
                           ?? command.Message.From?.Username;

            var detectionResult =
                await _client.DetectLanguageAsync(new[] { new DetectRequest(command.Message.Text) }, cancellationToken);

            if (detectionResult.Any(x => x.Language == "ru" && x.Score > 0.7))
            {
                var blames = string.IsNullOrEmpty(username)
                    ? Blames.UnformattedList
                    : Blames.List.ToArray();
                
                var randomBlameIndex = Rand.Default.Next(0, blames.Length - 1);
                
                await _botClient.SendTextMessageAsync(command.Message.Chat.Id,
                    string.Format(blames[randomBlameIndex], username),
                    replyToMessageId: command.Message.MessageId,
                    cancellationToken: cancellationToken);
            }
        }
    }
}