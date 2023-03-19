using BanderaBot.Host.Common.Configuration;
using BanderaBot.Host.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace BanderaBot.Host.Controllers;

[ApiController]
[Route("bot")]
public class BotController : ControllerBase
{
    private readonly IOptions<BotConfiguration> _config;
    private readonly IMediator _mediator;

    public BotController(IMediator mediator, IOptions<BotConfiguration> config)
    {
        _config = config;
        _mediator = mediator;
    }

    [HttpPost("{secretToken:guid}")]
    public async Task<IActionResult> Post(Guid secretToken, [FromBody] Update update,
        CancellationToken cancellationToken)
    {
        if (secretToken != _config.Value.SecretToken)
            return Forbid();

        var command = update.Type switch
        {
            UpdateType.Message when update.Message?.Chat.Id != null => new DetectCommand(update.Message.Text, update.Message.MessageId, update.Message.Chat.Id),
            _ => null
        };

        if (command != null)
            await _mediator.Send(command, cancellationToken);

        return Ok();
    }
}