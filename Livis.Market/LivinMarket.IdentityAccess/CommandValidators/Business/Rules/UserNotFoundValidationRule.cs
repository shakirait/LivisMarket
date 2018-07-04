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
    public class UserNotFoundValidationRule : IValidationRule<ResetPasswordCommand>
    {
        private const string _USER_NOT_EXIST = "User does not exist.";
        private readonly UserManager<LevisUser> _userManager;

        public UserNotFoundValidationRule(UserManager<LevisUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task TestAsync(ResetPasswordCommand command)
        {
            var user = await _userManager.FindByEmailAsync(command.Email);
            if (user == null)
            {
                throw new UserNotFoundException(_USER_NOT_EXIST);
            }
        }
    }
}
