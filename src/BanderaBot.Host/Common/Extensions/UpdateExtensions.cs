using BanderaBot.Host.Common.Utils;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace BanderaBot.Host.Common.Extensions;

public static class UpdateExtensions
{
    public static bool IsCommand(this Update update)
    {
        if (update.Message?.Entities is null)
            return false;

        var commandEntity = update.Message.Entities.FirstOrDefault(x => x.Type == MessageEntityType.BotCommand);

        return commandEntity is not null && BotCommands.List.Any(x => x.Command == update.Message.Text);
    }
}