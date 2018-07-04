using LivinMarket.Contact.Model.Commands.Business;
using Livis.Market.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LivinMarket.Contact.CommandHandlers.Business
{
    public class CustomerConfirmationCommandHandler : CommandHandler<CustomerConfirmationCommand>
    {
        protected async override Task HandleCommandAsync(CustomerConfirmationCommand command)
        {
            var primaryContact = _context.Contacts.Include(x => x.BillingAddress).First(x => x.ContactId == command.OrganisationId);
            primaryContact.FullName = $"{command.LastName} {command.FirstName}";
            primaryContact.FirstName = command.FirstName;
            primaryContact.LastName = command.LastName;
            primaryContact.PhoneNumber = command.PhoneNumber;
            primaryContact.Modified = DateTime.Now;
            primaryContact.ModifierId = command.UserConfirmation.Id;
            primaryContact.BillingAddress.CityOrTownOrVillage = command.City;
            primaryContact.BillingAddress.FirstName = command.FirstName;
            primaryContact.BillingAddress.LastName = command.LastName;
            primaryContact.BillingAddress.Modified = DateTime.Now;
            primaryContact.BillingAddress.ModifierId = command.UserConfirmation.Id;
            primaryContact.BillingAddress.Name = primaryContact.FullName;
            primaryContact.BillingAddress.PostCode = command.PostCode;
            primaryContact.BillingAddress.StreetAndHouseNumber = command.Street;
            primaryContact.BillingAddress.Prefecture = command.Prefecture;
            primaryContact.BillingAddress.PhoneNumber = command.PhoneNumber;

            _context.SaveChanges();
        }
    }
}
