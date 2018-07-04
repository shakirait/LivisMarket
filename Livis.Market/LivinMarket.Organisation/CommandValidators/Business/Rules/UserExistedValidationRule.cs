using LivinMarket.Organisation.Exceptions.Business;
using LivinMarket.Organisation.Model.Commands.Business;
using Livis.Market.Data;
using Livis.Market.Validation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LivinMarket.Organisation.CommandValidators.Business.Rules
{
    public class UserExistedValidationRule : IValidationRule<AddOrganisationCommand>
    {
        private const string _USER_EXIST = "User is exist.";
        private readonly UserManager<LevisUser> _userManager;

        public UserExistedValidationRule(UserManager<LevisUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task TestAsync(AddOrganisationCommand command)
        {
            var user = await _userManager.FindByEmailAsync(command.Email);
            if (user != null)
            {
                throw new UserExistedException(_USER_EXIST);
            }
        }
    }
}