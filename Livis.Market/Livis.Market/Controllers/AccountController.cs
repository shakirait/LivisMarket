using System.Linq;
using LivinMarket.Organisation.Model.Queries.Business;
using Livis.Market.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Livis.Market.Models.ViewModel;
using LivinMarket.Organisation.Model.Commands.Business;
using LivinMarket.Contact.Model.Queries.Business;
using LivinMarket.Contact.Model.Commands.Business;

namespace Livis.Market.Controllers
{
    [Authorize(Roles = "Admin, Agency, Customer")]
    public class AccountController : BaseController
    {
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return GetHomePageToRedirect();
        }

        public async Task<IActionResult> Member()
        {
            var currentUser = await _userContext.GetCurrentUser();
            var currentCustomerGroup = await _userContext.GetCustomerGroup();

            AgencyViewModel model = new AgencyViewModel() {
                Title = nameof(Member)
            };

            if (currentCustomerGroup == CustomerGroup.Agency)
            {
                var response = await _processor.ProcessQueryAsync<OrganisationQuery, OrganisationResponse>(new OrganisationQuery() { NameOrId = currentUser.Email });
                var organisation = response.Organisations.FirstOrDefault();
                if (organisation == null)
                {
                    PopulateErrorMessage("Not found any organisation match your account.");
                }
                else
                {
                    model = _mapper.Map<Livis.Market.Data.Organisation, AgencyViewModel>(organisation);
                    model.CustomerGroup = CustomerGroup.Agency;
                    PopulateErrorMessage(string.Empty);
                }
            }
            else
            {
                if (currentCustomerGroup == CustomerGroup.Customer)
                {
                    var response = await _processor.ProcessQueryAsync<ContactQuery, ContactResponse>(new ContactQuery { OwnerId = currentUser.Id });
                    model.FirstName = response.Contact.FirstName;
                    model.LastName = response.Contact.LastName;
                    model.Email = response.Contact.Email;
                    model.Street = response.Contact.BillingAddress.StreetAndHouseNumber;
                    model.PostCode = response.Contact.BillingAddress.PostCode;
                    model.PhoneNumber = response.Contact.BillingAddress.PhoneNumber;
                    model.Prefecture = response.Contact.BillingAddress.Prefecture;
                    model.City = response.Contact.BillingAddress.CityOrTownOrVillage;
                    model.OrganisationId = response.Contact.ContactId;
                    model.CustomerGroup = CustomerGroup.Customer;
                    PopulateErrorMessage(string.Empty);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Addresses() {
            var model = new AddressesViewModel()
            {
                Title = "Addresses"
            };
            return View(model);
        }

        public async Task<IActionResult> Inquiry()
        {
            var model = new ContactInquiryViewModel()
            {
                Title = "Contact Inquiry"
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Member(AgencyViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userContext.GetCurrentUser();
                var currentCustomerGroup = await _userContext.GetCustomerGroup();
                if (currentCustomerGroup == CustomerGroup.Agency)
                {
                    var command = _mapper.Map<AgencyViewModel, AgencyConfirmationCommand>(model);
                    command.UserConfirmation = currentUser;
                    await _processor.ProcessCommandAsync<AgencyConfirmationCommand>(command);
                }
                else
                {
                    if (currentCustomerGroup == CustomerGroup.Customer)
                    {
                        var command = _mapper.Map<AgencyViewModel, CustomerConfirmationCommand>(model);
                        command.UserConfirmation = currentUser;
                        await _processor.ProcessCommandAsync<CustomerConfirmationCommand>(command);
                    }
                }
                if (!string.IsNullOrEmpty(model.Password?.Trim()))
                {
                    await _userContext.ChangePassword(model.Password.Trim(), currentUser);
                }
                return GetShoppingPageToRedirect();
            }
            return View(model);
        }

    }
}