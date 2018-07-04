using Livis.Market.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Livis.Market.Validation
{
    public class CommandValidationDecorator<TCommand>
         : RequestValidationDecorator<TCommand, VoidReturn>,
             ICommandHandler<TCommand>
         where TCommand : ICommand
    {
        public CommandValidationDecorator(
            IEnumerable<IValidator<TCommand>> validators,
            ICommandHandler<TCommand> inner, IServiceProvider serviceProvider)
            : base(validators, inner, serviceProvider)
        {
        }
    }
}