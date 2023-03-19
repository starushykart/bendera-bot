using BanderaBot.Host.Common.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BanderaBot.Host.Controllers;

[ApiController]
[Route("")]
public class StatusController : ControllerBase
{
    [HttpGet]
    public IActionResult Get([FromServices] IHostEnvironment environment, [FromServices] IOptions<BotConfiguration> config)
    {
        return Ok(new
        {
            environment.ApplicationName,
            environment.EnvironmentName,
            config.Value.HostAddress
        });
    }
}