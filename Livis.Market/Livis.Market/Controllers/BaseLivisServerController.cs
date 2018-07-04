using Autofac;
using AutoMapper;
using Livis.Market.Autofac;
using Livis.Market.Data;
using Livis.Market.Infrastructure;
using Livis.Market.Utilities.IoC;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Livis.Market.Controllers
{

    public abstract class BaseLivisServerController : Controller
    {
        private readonly ILifetimeScope _scope;
        protected SignInManager<LevisUser> _signInManager;
        protected UserManager<LevisUser> _userManager;
        protected IRequestProcessor _processor;
        protected IMapper _mapper;
        private LevisUser _currentUser { get; set; }

        private void InitializeServices(IServiceProvider serviceLocator)
        {
            _signInManager = serviceLocator.GetService<SignInManager<LevisUser>>();
            _userManager = serviceLocator.GetService<UserManager<LevisUser>>();
            _mapper = serviceLocator.GetService<IMapper>();
            _processor = serviceLocator.GetService<IRequestProcessor>();
        }

        protected BaseLivisServerController()
        {
            var container = Container.Instance;
            _scope = container.BeginLifetimeScope();
            var serviceProvider = _scope.Resolve<IServiceProvider>();
            InitializeServices(serviceProvider);
        }

        protected UrlHelper Helper
        {
            get
            {
                return new UrlHelper(this.Url.ActionContext);
            }
        }

        #region Populate metadata of current user
        protected void PopulateErrorMessage(string errorMessage)
        {
            ViewBag.ErrorMessage = errorMessage;
        }
       
        protected async Task<LevisUser> GetCurrentUser()
        {
            if (_currentUser == null)
            {
                _currentUser = await _userManager.GetUserAsync(HttpContext.User);
            }
            return _currentUser;
        }

        protected async Task ChangePassword(string newPassword, LevisUser currentUser)
        {
            currentUser.PasswordHash = _userManager.PasswordHasher.HashPassword(currentUser, newPassword);
            await _userManager.UpdateAsync(currentUser);
        }

        protected async Task GenerateClaimIdentity(string username) {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username)
                };
            var userIdentity = new ClaimsIdentity(claims, "login");
            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
            await HttpContext.SignInAsync(principal);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _scope.Dispose();
            }
        }
        #endregion
    }
}
