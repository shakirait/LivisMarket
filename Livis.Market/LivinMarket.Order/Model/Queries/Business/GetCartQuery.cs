using Livis.Market.Caching;
using Livis.Market.Infrastructure;
using System;

namespace LivinMarket.Order.Model.Queries.Business
{
    [CacheableResponse]
    public class GetCartQuery : IQuery<GetCartResponse>
    {
        public Guid CustomerId { get; set; }
        public string MartketName { get; set; }
        public string ShopName { get; set; }
    }
}
