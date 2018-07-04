using LivinMarket.IdentityAccess.Model.Queries.Business;

using Livis.Market.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System;
using Livis.Market.Utilities.Helper;
using LivinMarket.IdentityAccess.Exceptions.Business;

namespace LivinMarket.IdentityAccess.QueryHandlers.Business
{
    public class LoginQueryHandler : QueryHandler<LoginQuery, LoginResponse>
    {
        protected override async Task<LoginResponse> HandleQueryAsync(LoginQuery query)
        {
            var result = await _signInManager.PasswordSignInAsync(query.Username, query.Password, true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(query.Username);
                if(!user.IsPowerUser)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    if(roles.Any(r => r.Equals(ComponentsHelper.Agency, StringComparison.OrdinalIgnoreCase)))
                    {
                        var org = _context.Organisations.FirstOrDefault(x => x.Email.Equals(query.Username));
                        if(org != null && ComponentsHelper.Rejected.Equals(org.RegistrationStatus))
                        {
                            throw new UserLockedException("Currently your account is locked. Please contact supportor of Livis Market");
                        }
                    }
                }
                
                return new LoginResponse()
                {
                    IsPowerUser = user.IsPowerUser,
                    IsValid = true
                };
            }
            if(result.IsLockedOut)
            {
                throw new UserLockedException("Currently your account is locked. Please contact supportor of Livis Market");
            }

            return new LoginResponse()
            {
                IsPowerUser = false,
                IsValid = false
            };
        }
    }
}
