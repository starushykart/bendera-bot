using Telegram.Bot.Types;

namespace BanderaBot.Host.Common.Utils;

public static class BotCommands
{
    private static BotCommand BotCommand = new() { Command = "/test", Description = "test" };

    public static IEnumerable<BotCommand> List => new[]
    {
        BotCommand
    };
}