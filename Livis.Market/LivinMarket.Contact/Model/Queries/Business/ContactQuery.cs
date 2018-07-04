using Livis.Market.Caching;
using Livis.Market.Infrastructure;

namespace LivinMarket.Contact.Model.Queries.Business
{
    [CacheableResponse]
    public class ContactQuery : IQuery<ContactResponse>
    {
        public string OwnerId { get; set; }
    }
}
