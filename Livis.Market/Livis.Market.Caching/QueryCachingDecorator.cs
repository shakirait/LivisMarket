using Livis.Market.Infrastructure;
using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Text;

namespace Livis.Market.Caching
{
    public class QueryCachingDecorator<TQuery, TResult>
      : RequestCachingDecorator<TQuery, TResult>,
          IQueryHandler<TQuery, TResult>
      where TQuery : IQuery<TResult>
    {
        public QueryCachingDecorator(MemoryCache memoryCache, IQueryHandler<TQuery, TResult> inner)
            : base(memoryCache, inner)
        {
        }
    }
}
