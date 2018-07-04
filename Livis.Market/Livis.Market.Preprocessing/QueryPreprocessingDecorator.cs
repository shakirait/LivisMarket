using Livis.Market.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Livis.Market.Preprocessing
{
    public class QueryPreprocessingDecorator<TQuery, TResult>
         : RequestPreprocessingDecorator<TQuery, TResult>,
             IQueryHandler<TQuery, TResult>
         where TQuery : IQuery<TResult>
    {
        public QueryPreprocessingDecorator(
            IEnumerable<IPreprocessor<TQuery>> preprocessors,
            IQueryHandler<TQuery, TResult> inner)
            : base(preprocessors, inner)
        {
        }
    }
}
