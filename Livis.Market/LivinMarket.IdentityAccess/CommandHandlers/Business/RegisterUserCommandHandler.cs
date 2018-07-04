using LivinMarket.IdentityAccess.Exceptions.Business;
using LivinMarket.IdentityAccess.Model.Commands.Business;
using Livis.Common.Notifications;
using Livis.Market.Data;
using Livis.Market.Infrastructure;
using Livis.Market.Utilities.ApplicationSettings;
using Livis.Market.Utilities.Helper;
using System;
using System.Threading.Tasks;

namespace LivinMarket.IdentityAccess.CommandHandlers.Business
{
    public class RegisterUserCommandHandler : CommandHandler<RegisterUserCommand>
    {
        private readonly INotificationService _notification;
        private readonly IAppSettings _appSettings;

        public RegisterUserCommandHandler(INotificationService notification, IAppSettings appSettings)
        {
            _appSettings = appSettings;
            _notification = notification;
        }

        protected override async Task HandleCommandAsync(RegisterUserCommand command)
        {
            try
            {
                var user = new LevisUser()
                {
                    Email = command.Email,
                    EmailConfirmed = true,
                    IsPowerUser = false,
                    NormalizedEmail = command.Email,
                    NormalizedUserName = command.Email,
                    PhoneNumber = command.PhoneNumber,
                    PhoneNumberConfirmed = true,
                    UserName = command.Email
                };
                var chkUser = await _userManager.CreateAsync(user, command.Password).ConfigureAwait(false);
                if (chkUser.Succeeded)
                {
                    var result = await _userManager.AddToRoleAsync(user, ComponentsHelper.Customer);
                    await _notification.ThankYouAgencyConfirmationAsync(command.Email);
                    var primaryContact = new CustomerContact();
                    primaryContact.Created = DateTime.Now;
                    primaryContact.CreatorId = user.Id;
                    primaryContact.Modified = DateTime.Now;
                    primaryContact.ModifierId = user.Id;
                    primaryContact.CustomerGroup = CustomerGroup.Customer;
                    primaryContact.Email = command.Email;
                    primaryContact.FirstName = command.FirstName;
                    primaryContact.LastName = command.LastName;
                    primaryContact.FullName = $"{command.LastName} {command.FirstName}";
                    primaryContact.Organisation = null;
                    primaryContact.OrganisationId = null;
                    primaryContact.OwnerId = user.Id;
                    primaryContact.User = user;
                    primaryContact.PhoneNumber = command.PhoneNumber;
                    primaryContact.PreferredLanguage = _appSettings.DefaultLanguage;
                    primaryContact.PreferredCurrency = _appSettings.DefaultCurrency;
                    primaryContact.BillingAddress = new CustomerAddress()
                    {
                        Created = DateTime.Now,
                        CreatorId = user.Id,
                    };
                    primaryContact.BillingAddress.AddressType = CustomerAddressTypeEnum.Billing;
                    primaryContact.BillingAddress.CityOrTownOrVillage = command.City;
                    primaryContact.BillingAddress.Email = command.Email;
                    primaryContact.BillingAddress.FirstName = command.FirstName;
                    primaryContact.BillingAddress.LastName = command.LastName;
                    primaryContact.BillingAddress.Modified = DateTime.Now;
                    primaryContact.BillingAddress.ModifierId = user.Id;
                    primaryContact.BillingAddress.Name = primaryContact.FullName;
                    primaryContact.BillingAddress.PostCode = command.PostCode;
                    primaryContact.BillingAddress.StreetAndHouseNumber = command.Street;
                    primaryContact.BillingAddress.Prefecture = command.Prefecture;
                    primaryContact.BillingAddress.PhoneNumber = command.PhoneNumber;
                    _context.Contacts.Add(primaryContact);
                    _context.SaveChanges();
                }
                else
                {
                    throw new RegisterUserException("Please contact Administrator. System is unable creating user.");
                }
            }
            catch(Exception ex)
            {
                throw new RegisterUserException(ex.Message, ex);
            }
        }
    }
}
