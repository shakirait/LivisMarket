

namespace Livis.Market.Infrastructure
{
    public interface ICommandHandler
    {
    }

    public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, VoidReturn>, 
        ICommandHandler where TCommand : ICommand
    {

    }
}
