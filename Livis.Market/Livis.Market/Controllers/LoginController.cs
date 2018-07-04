using LivinMarket.IdentityAccess.Exceptions.Business;
using LivinMarket.IdentityAccess.Model.Queries.Business;
using Livis.Market.Infrastructure.Constants;
using Livis.Market.Models.ViewModel;
using Livis.Market.Validation.BuiltIn;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Livis.Market.Controllers
{
    public class LoginController : BaseController
    {
        public async Task<ActionResult> Index(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
                return GetShoppingPageToRedirect();
            var model = LoadBody(returnUrl);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid || ! await ValidateUser(viewModel))
            {
                var model = LoadBody(viewModel.ReturnUrl, viewModel);
                return View(model);
            }
            if (!string.IsNullOrEmpty(viewModel.ReturnUrl))
                return Redirect(viewModel.ReturnUrl);

            return GetShoppingPageToRedirect();
        }

        #region Private Methods
        private LoginViewModel LoadBody(string returnUrl, LoginViewModel model = null)
        {
            model = model == null ? new LoginViewModel() : model;
            string referer = Request.Headers["Referer"].ToString();
            if (string.IsNullOrEmpty(returnUrl) && !string.IsNullOrEmpty(referer))
                returnUrl = referer;
            model.ReturnUrl = returnUrl;
            model.RegistrationUrl = Helper.Action("Index", "Registration");
            model.ResetPasswordUrl = Helper.Action("Index", "ResetPassword");
            return model;
        }

        private async Task<bool> ValidateUser(LoginViewModel viewModel)
        {
            var isValid = true;
            try
            {
                var query = await _processor.ProcessQueryAsync<LoginQuery, LoginResponse>(new LoginQuery()
                {
                    Username = viewModel.Email,
                    Password = viewModel.Password
                });
                isValid = query.IsValid;
            }
            catch (CompositeValidationException)
            {
                isValid = false;
            }
            catch (UserNotFoundException)
            {
                isValid = false;
            }
            if (!isValid)
                ModelState.AddModelError(string.Empty, Login.Validations.Failure);
            else
                await _userContext.GenerateClaimIdentity(viewModel.Email);

            return isValid;
        }
        #endregion
    }
}