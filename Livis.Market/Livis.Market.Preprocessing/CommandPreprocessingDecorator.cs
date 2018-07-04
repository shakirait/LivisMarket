using Livis.Market.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Livis.Market.Preprocessing
{
    public class CommandPreprocessingDecorator<TCommand>
       : RequestPreprocessingDecorator<TCommand, VoidReturn>,
           ICommandHandler<TCommand>
       where TCommand : ICommand
    {
        public CommandPreprocessingDecorator(
            IEnumerable<IPreprocessor<TCommand>> preprocessors,
            ICommandHandler<TCommand> inner)
            : base(preprocessors, inner)
        {
        }
    }
}