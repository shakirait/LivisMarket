

namespace Livis.Market.Infrastructure
{
    public interface IQuery
    {
    }

    public interface IQuery<TResult> : IRequest<TResult>, IQuery
    { 
    }
}
