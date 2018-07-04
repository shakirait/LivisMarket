
using Livis.Market.Data;
using Livis.Market.Utilities.ApplicationSettings;
using Livis.Market.Utilities.Services.UserContext;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Livis.Market.Infrastructure;
using LivinMarket.Contact.Model.Queries.Business;

namespace Livis.Market.Services
{
    public class UserContext : ILivisUserContext
    {
        protected readonly UserManager<LevisUser> _userManager;
        protected readonly IRequestProcessor _processor;
        protected readonly IAppSettings _appSettings;
        protected readonly Controller _controller;
        private string _currency { get; set; }
        private LevisUser _currentUser { get; set; }
        private CustomerContact _currentContact { get; set; }

        public UserContext(UserManager<LevisUser> userManager, IAppSettings appSettings, IRequestProcessor processor, Controller controller)
        {
            _userManager = userManager;
            _processor = processor;
            _appSettings = appSettings;
            _controller = controller;
        }

        public async Task<LevisUser> GetCurrentUser()
        {
            if (_currentUser == null)
            {
                _currentUser = await _userManager.GetUserAsync(_controller.HttpContext.User);
            }
            return _currentUser;
        }

        public async Task<CustomerContact> GetCurrentContact()
        {
            if (_currentContact == null)
            {
                var currentUser = await GetCurrentUser();
                if (currentUser == null || currentUser.IsPowerUser)
                {
                    _currentContact = null;
                }
                else
                {
                    var response = await _processor.ProcessQueryAsync<ContactQuery, ContactResponse>(new ContactQuery()
                    {
                        OwnerId = currentUser.Id
                    });
                    _currentContact = response.Contact;
                }
            }
            return _currentContact;
        }

        public async Task ChangePassword(string newPassword, LevisUser currentUser)
        {
            currentUser.PasswordHash = _userManager.PasswordHasher.HashPassword(currentUser, newPassword);
            await _userManager.UpdateAsync(currentUser);
        }

        public async Task<CustomerGroup> GetCustomerGroup()
        {
            var contact = await GetCurrentContact();
            if (contact == null)
            {
                return CustomerGroup.NonMember;
            }

            return contact.CustomerGroup;
        }

        public async Task<string> GetCurrentCurrency()
        {
            if (!string.IsNullOrEmpty(_currency))
            {
                return _currency;
            }
            var contact = await GetCurrentContact();
            _currency = contact?.PreferredCurrency;
            if (string.IsNullOrEmpty(_currency))
            {
                _currency = _appSettings.DefaultCurrency;
            }
            return _currency;
        }

        public async Task GenerateClaimIdentity(string username)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username)
                };
            var userIdentity = new ClaimsIdentity(claims, "login");
            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
            await _controller.HttpContext.SignInAsync(principal);
        }
    }
}
