using Livis.Market.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using LivinMarket.Contact.Model.Queries.Business;
using Microsoft.EntityFrameworkCore;

namespace LivinMarket.Contact.QueryHandlers.Business
{
    public class ContactQueryHandler : QueryHandler<ContactQuery, ContactResponse>
    {
        protected override async Task<ContactResponse> HandleQueryAsync(ContactQuery query)
        {
            var contact = _context.Contacts.Include(x => x.BillingAddress).Include(x => x.ShippingAddress).Where(c => c.OwnerId.Equals(query.OwnerId)).OrderBy(c => c.Created).First();

            return new ContactResponse()
            {
                Contact = contact
            };
        }
    }
}
