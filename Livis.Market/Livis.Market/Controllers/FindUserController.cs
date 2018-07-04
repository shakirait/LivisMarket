using System;
using System.Threading.Tasks;
using Livis.Market.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Livis.Market.Controllers
{
    public class FindUserController : BaseLivisServerController
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UpdateInfo(FindUsernameView model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user == null)
                {
                    model = null;
                    PopulateErrorMessage("Unable found user match your condition.");
                }
                else
                {
                    if(!string.IsNullOrEmpty(model.Password?.Trim()))
                    {
                        await ChangePassword(model.Password?.Trim(), user);
                    }
                    if(model.IsLock)
                    {
                        var result = await _userManager.SetLockoutEnabledAsync(user, true);
                        if (result.Succeeded)
                        {
                            result = await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
                        }
                    }
                    else
                    {
                        var result = await _userManager.SetLockoutEnabledAsync(user, true);
                        if (result.Succeeded)
                        {
                            await _userManager.SetLockoutEndDateAsync(user, null);
                            result = await _userManager.ResetAccessFailedCountAsync(user);
                        }
                    }
                    PopulateErrorMessage("Update Successfully");
                }
            }

            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(FindUsernameView model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                if(user ==  null)
                {
                    model = null;
                    PopulateErrorMessage("Unable found user match your condition.");
                }
                else
                {
                    model.Username = user.UserName;
                    model.IsLock = await _userManager.IsLockedOutAsync(user);
                    PopulateErrorMessage(string.Empty);
                }
            }
           
            return View(model);
        }
    }
}