using Livis.Market.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Livis.Market.Validation
{
    public class QueryValidationDecorator<TQuery, TResult>
         : RequestValidationDecorator<TQuery, TResult>,
             IQueryHandler<TQuery, TResult>
         where TQuery : IQuery<TResult>
    {
        public QueryValidationDecorator(
            IEnumerable<IValidator<TQuery>> validators,
            IQueryHandler<TQuery, TResult> inner, IServiceProvider serviceProvider)
            : base(validators, inner, serviceProvider)
        {
        }
    }
}
