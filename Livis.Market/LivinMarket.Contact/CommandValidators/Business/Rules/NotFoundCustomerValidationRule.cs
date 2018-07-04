
using LivinMarket.Contact.Exceptions.Business;
using LivinMarket.Contact.Model.Commands.Business;
using Livis.Market.Data;
using Livis.Market.Validation;
using System.Linq;
using System.Threading.Tasks;

namespace LivinMarket.Contact.CommandValidators.Business.Rules
{
    public class NotFoundCustomerValidationRule : IValidationRule<CustomerConfirmationCommand>
    {
        private const string _USER_Not_EXIST = "Not found customer.";
        private readonly LivisMarketContext _context;

        public NotFoundCustomerValidationRule(LivisMarketContext context)
        {
            _context = context;
        }
        public async Task TestAsync(CustomerConfirmationCommand command)
        {
            var isExisted = _context.Contacts.Any(o => o.ContactId == command.OrganisationId);
            if (!isExisted)
                throw new ContactNotFoundException(_USER_Not_EXIST);
        }
    }
}