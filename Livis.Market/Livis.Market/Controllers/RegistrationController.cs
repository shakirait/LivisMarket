using System.Threading.Tasks;
using LivinMarket.IdentityAccess.Model.Commands.Business;
using LivinMarket.IdentityAccess.Model.Queries.Business;
using Livis.Market.Infrastructure.Constants;
using Livis.Market.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Livis.Market.Controllers
{
    public class RegistrationController : BaseController
    {
        public IActionResult Index()
        {
            var model = new MemberRegistrationViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(MemberRegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
               var command = _mapper.Map<MemberRegistrationViewModel, RegisterUserCommand>(model);
                await _processor.ProcessCommandAsync<RegisterUserCommand>(command);
                var query =  await _processor.ProcessQueryAsync<LoginQuery, LoginResponse>(new LoginQuery()
                {
                    Username = model.Email,
                    Password = model.Password
                });
                var isValid = query.IsValid;
                if (!isValid)
                    ModelState.AddModelError(string.Empty, Login.Validations.Failure);
                else
                {
                    await _userContext.GenerateClaimIdentity(model.Email);
                    return GetShoppingPageToRedirect();
                }
                
            }
            return View(model);
        }
    }
}