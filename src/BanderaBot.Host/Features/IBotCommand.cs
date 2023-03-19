using MediatR;

namespace BanderaBot.Host.Features;

public interface IBotCommand : IRequest
{
    string Command { get; }
}