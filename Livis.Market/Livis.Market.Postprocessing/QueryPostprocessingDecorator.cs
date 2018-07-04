using Livis.Market.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Livis.Market.Postprocessing
{
    public class QueryPostprocessingDecorator<TQuery, TResult>
       : RequestPostprocessingDecorator<TQuery, TResult>,
           IQueryHandler<TQuery, TResult>
       where TQuery : IQuery<TResult>
    {
        public QueryPostprocessingDecorator(
            IQueryHandler<TQuery, TResult> inner,
            IEnumerable<IPostprocessor<TQuery, TResult>> postprocessors)
            : base(inner, postprocessors)
        {
        }
    }
}
