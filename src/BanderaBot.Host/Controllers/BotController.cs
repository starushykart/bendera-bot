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
    private readonly ILogger<BotController> _logger;

    public BotController(IMediator mediator, IOptions<BotConfiguration> config, ILogger<BotController> logger)
    {
        _config = config;
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost("{secretToken:guid}")]
    public async Task<IActionResult> Post(Guid secretToken, [FromBody] Update update,
        CancellationToken cancellationToken)
    {
        try
        {
            if (secretToken != _config.Value.SecretToken)
                return Forbid();

            var command = update.Type switch
            {
                UpdateType.Message when update.Message != null => new DetectCommand(update.Message),
                _ => null
            };

            if (command != null)
                await _mediator.Send(command, cancellationToken);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Update handling error");
        }
        
        return Ok();
    }
}