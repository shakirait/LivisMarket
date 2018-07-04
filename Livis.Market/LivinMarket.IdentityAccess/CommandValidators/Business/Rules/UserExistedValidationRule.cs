using LivinMarket.IdentityAccess.Exceptions.Business;
using LivinMarket.IdentityAccess.Model.Commands.Business;
using Livis.Market.Data;
using Livis.Market.Utilities.ApplicationSettings;
using Livis.Market.Validation;
using Microsoft.AspNetCore.Identity;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LivinMarket.IdentityAccess.CommandValidators.Business.Rules
{
    public class UserExistedValidationRule : IValidationRule<RegisterUserCommand>
    {
        private const string _USER_EXIST = "The email has already registered.";
        private readonly UserManager<LevisUser> _userManager;

        public UserExistedValidationRule(UserManager<LevisUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task TestAsync(RegisterUserCommand command)
        {
            var user = await _userManager.FindByEmailAsync(command.Email);
            if (user != null)
            {
                throw new EmailAlreadyExistedException(_USER_EXIST);
            }
        }
    }
}
