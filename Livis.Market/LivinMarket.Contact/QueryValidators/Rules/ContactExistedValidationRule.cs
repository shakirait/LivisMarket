using LivinMarket.Contact.Exceptions.Business;
using LivinMarket.Contact.Model.Queries.Business;
using Livis.Market.Data;
using Livis.Market.Utilities.ApplicationSettings;
using Livis.Market.Validation;
using Microsoft.AspNetCore.Identity;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LivinMarket.Contact.QueryValidators.Rules
{
    public class ContactExistedValidationRule : IValidationRule<ContactQuery>
    {
        private const string _CONTACT_NOT_EXIST = "Contact not found.";
        private readonly LivisMarketContext _context;

        public ContactExistedValidationRule(LivisMarketContext context)
        {
            _context = context;
        }
        public async Task TestAsync(ContactQuery command)
        {
            var isExist = _context.Contacts.Any(c => c.OwnerId.Equals(command.OwnerId));
            if (!isExist)
            {
                throw new ContactNotFoundException(_CONTACT_NOT_EXIST);
            }
        }
    }
}
