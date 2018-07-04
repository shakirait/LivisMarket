using Livis.Market.Caching;
using Livis.Market.Data;
using Livis.Market.Infrastructure;

namespace LivinMarket.Order.Model.Queries.Business
{
    [CacheableResponse]
    public class GetCartResponse
    {
        public SerializableCart Cart { get; set; }
    }
}
