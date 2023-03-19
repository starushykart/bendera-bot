namespace BanderaBot.Host.Common.Utils;

public static class Rand
{
    [ThreadStatic] 
    private static Random? _defaultRand;

    public static Random Default => _defaultRand ??= new Random();
}