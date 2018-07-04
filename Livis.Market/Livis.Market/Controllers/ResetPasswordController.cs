using LivinMarket.IdentityAccess.Exceptions.Business;
using LivinMarket.IdentityAccess.Model.Commands.Business;
using Livis.Market.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Constant = Livis.Market.Infrastructure.Constants;
using System.Threading.Tasks;

namespace Livis.Market.Controllers
{
    public class ResetPasswordController : BaseController
    {
        public async Task<IActionResult> Index()
        {
            return View(new ResetPasswordViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            TempData[Constant.ResetPassword.HasResetPasswordForUser] = model.Email;
            try
            {
                await _processor.ProcessCommandAsync<ResetPasswordCommand>(
                    new ResetPasswordCommand()
                    {
                        Email = model.Email
                    });
            }
            catch(UserNotFoundException)
            {
                return GetDefaultPageToRedirect(model);
            }
            catch(UserResetPasswordException)
            {
                return GetDefaultPageToRedirect(model);
            }
            return GetDefaultPageToRedirect(model);
        }

        #region Private Method
        private ActionResult GetDefaultPageToRedirect(ResetPasswordViewModel model)
        {
            return View("Index", model);
        }
        #endregion
    }
}