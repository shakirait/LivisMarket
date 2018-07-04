using Livis.Market.Caching;
using Livis.Market.Infrastructure;

namespace LivinMarket.Order.Model.Queries.Business
{
    [CacheableResponse]
    public class OrderQuery : IQuery<OrderResponse>
    {
    }
}
