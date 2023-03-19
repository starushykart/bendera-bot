using System.Diagnostics.CodeAnalysis;

namespace BanderaBot.Host.Resources;

[SuppressMessage("ReSharper", "StringLiteralTypo")]
public static class Blames
{
    public static readonly string[] UnformattedList = {
        "Ти нормальною мовою говорити вмієш?",
        "Не розумію, чи то хтось пернув чи то москальскою заговорив?",
        "Вуха в'януть від твоєї мордорської мови.",
        "Браття, здається серед нас москальський агент.",
        "Russian sucks!",
        "Ой лишенька, зараз блювону від цієї кацапської."
    };
    
    public static readonly string[] FormattedList = {
        "{0}, ну ти ж ніби нормальна людина? То і говори нормально мовою.",
        "{0}, вийди звідси розбійник російськомовний!",
        "Я вже викликав СБУ. Жди гостей {0}! їм своєю кацапською побалакаєш!",
        "{0}, може вакзал, масква, расєя ... з такою говіркою?",
        "Голосування за виганяння з чату {0} за пропагування гівноросійської розпочато!",
    };

    public static readonly IEnumerable<string> List = UnformattedList.Union(FormattedList);
}