using System.Threading.Tasks;

namespace Livis.Market.Infrastructure
{
    public abstract class QueryHandler<TQuery, TResult>
        : RequestHandler<TQuery, TResult>,
            IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        public override async Task<TResult> HandleAsync(TQuery query)
        {
            return await HandleQueryAsync(query);
        }

        protected abstract Task<TResult> HandleQueryAsync(TQuery query);
    }
}
