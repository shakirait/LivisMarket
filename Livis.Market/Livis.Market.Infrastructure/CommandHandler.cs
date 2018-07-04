using System.Threading.Tasks;

namespace Livis.Market.Infrastructure
{
    public abstract class CommandHandler<TCommand>
       : RequestHandler<TCommand, VoidReturn>,
           ICommandHandler<TCommand>
       where TCommand : ICommand
    {
        public override async Task<VoidReturn> HandleAsync(TCommand command)
        {
            await HandleCommandAsync(command);

            return VoidReturn.Instance;
        }

        protected abstract Task HandleCommandAsync(TCommand command);
    }
}
