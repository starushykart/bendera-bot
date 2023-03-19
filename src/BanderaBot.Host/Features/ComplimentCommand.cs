using MediatR;

namespace BanderaBot.Host.Features;

public class ComplimentCommand : IBotCommand
{
    public string Command => "/compliment";
    
    public class Handler : IRequestHandler<ComplimentCommand>
    {
        public Task Handle(ComplimentCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}