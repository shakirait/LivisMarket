using AutoMapper;
using LivinMarket.Organisation.Exceptions.Business;
using LivinMarket.Organisation.Model.Commands.Business;
using Livis.Common.Notifications;
using Livis.Common.Password;
using Livis.Market.Data;
using Livis.Market.Infrastructure;
using Livis.Market.Utilities;
using Livis.Market.Utilities.ApplicationSettings;
using Livis.Market.Utilities.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivinMarket.Organisation.CommandHandlers.Business
{
    public class AgencyConfirmationCommandHandler : CommandHandler<AgencyConfirmationCommand>
    {
        private readonly INotificationService _notification;
        private readonly IAppSettings _appSettings;

        public AgencyConfirmationCommandHandler(INotificationService notification, IAppSettings appSettings)
        {
            _appSettings = appSettings;
            _notification = notification;
        }

        protected async override Task HandleCommandAsync(AgencyConfirmationCommand command)
        {
            var organisation = _context.Organisations.FirstOrDefault(o => o.OrganisationId.Equals(command.OrganisationId));
            if(organisation != null)
            {
                organisation.ShopName = command.ShopName;
                organisation.WebsiteUrl = command.WebsiteUrl;
                organisation.OpenTime = command.OpenTime;
                organisation.BankName = command.BankName;
                organisation.BankNumber = command.BankNumber;
                organisation.BranchName = command.BranchName;
                organisation.BranchNumber = command.BranchNumber;
                organisation.AccountNumber = command.AccountNumber;
                organisation.AccountHolder = command.AccountHolder;
                organisation.Prefecture = command.Prefecture;
                organisation.City = command.City;
                organisation.Street = command.Street;
                organisation.PostCode = command.PostCode;
                organisation.PhoneNumber = command.PhoneNumber;
                organisation.FaxNumber = command.FaxNumber;
                organisation.FirstName = command.FirstName;
                organisation.LastName = command.LastName;
                if(ComponentsHelper.Provisional.Equals(organisation.RegistrationStatus))
                {
                    organisation.RegistrationStatus = ComponentsHelper.Completed;
                }
                if(!organisation.IsSendThankYouMail)
                {
                    await _notification.ThankYouAgencyConfirmationAsync(organisation.Email);
                    organisation.IsSendThankYouMail = true;
                    await _notification.AnnounceAdminAgencyCompletedAsync(organisation.Email);
                }

                var primaryContact  = _context.Contacts.Include(p => p.BillingAddress).Where(x => x.OrganisationId.HasValue && x.OrganisationId == organisation.OrganisationId).OrderBy(x => x.Created).FirstOrDefault();
                if (primaryContact == null)
                {
                    primaryContact = new CustomerContact();
                    primaryContact.Created = DateTime.Now;
                    primaryContact.CreatorId = command.UserConfirmation.Id;
                }
                primaryContact.Modified = DateTime.Now;
                primaryContact.ModifierId = command.UserConfirmation.Id;
                primaryContact.CustomerGroup = CustomerGroup.Agency;
                primaryContact.Email = organisation.Email;
                primaryContact.FirstName = organisation.FirstName;
                primaryContact.LastName = organisation.LastName;
                primaryContact.FullName = $"{organisation.LastName} {organisation.FirstName}";
                primaryContact.Organisation = organisation;
                primaryContact.OrganisationId = organisation.OrganisationId;
                primaryContact.OwnerId = command.UserConfirmation.Id;
                primaryContact.User = command.UserConfirmation;
                primaryContact.PreferredLanguage = _appSettings.DefaultLanguage;
                primaryContact.PreferredCurrency = _appSettings.DefaultCurrency;
                primaryContact.PhoneNumber = organisation.PhoneNumber;

                if (!primaryContact.PreferredBillingAddressId.HasValue)
                {
                    primaryContact.BillingAddress = new CustomerAddress()
                    {
                        Created = DateTime.Now,
                        CreatorId = command.UserConfirmation.Id,
                    };
                }
                primaryContact.BillingAddress.AddressType = CustomerAddressTypeEnum.Billing;
                primaryContact.BillingAddress.CityOrTownOrVillage = organisation.City;
                primaryContact.BillingAddress.Email = organisation.Email;
                primaryContact.BillingAddress.FirstName = primaryContact.FirstName;
                primaryContact.BillingAddress.LastName = primaryContact.LastName;
                primaryContact.BillingAddress.Modified = DateTime.Now;
                primaryContact.BillingAddress.ModifierId = command.UserConfirmation.Id;
                primaryContact.BillingAddress.Name = primaryContact.FullName;
                primaryContact.BillingAddress.PostCode = organisation.PostCode;
                primaryContact.BillingAddress.StreetAndHouseNumber = organisation.Street;
                primaryContact.BillingAddress.Prefecture = organisation.Prefecture;
                primaryContact.BillingAddress.PhoneNumber = organisation.PhoneNumber;
                if (primaryContact.ContactId == null || primaryContact.ContactId == Guid.Empty)
                {
                    _context.Contacts.Add(primaryContact);

                }

                _context.SaveChanges();
            }
        }
    }
}
