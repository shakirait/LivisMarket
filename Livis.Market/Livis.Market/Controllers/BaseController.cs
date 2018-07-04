using Autofac;
using AutoMapper;
using Livis.Market.Data;
using Livis.Market.Helper;
using Livis.Market.Infrastructure;
using Livis.Market.Models.ViewModel;
using Livis.Market.Services;
using Livis.Market.Utilities.ApplicationSettings;
using Livis.Market.Utilities.IoC;
using Livis.Market.Utilities.Services.UserContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Threading.Tasks;

namespace Livis.Market.Controllers
{

    public abstract class BaseController : Controller
    {
        private readonly ILifetimeScope _scope;

        protected SignInManager<LevisUser> _signInManager;
        protected UserManager<LevisUser> _userManager;
        protected IRequestProcessor _processor;
        protected IMapper _mapper;
        protected IAppSettings _appSettings;
        protected ILivisUserContext _userContext;
        protected ICartService _cartService;

        private void InitializeServices(IServiceProvider serviceLocator)
        {
            _signInManager = serviceLocator.GetService<SignInManager<LevisUser>>();
            _userManager = serviceLocator.GetService<UserManager<LevisUser>>();
            _mapper = serviceLocator.GetService<IMapper>();
            _processor = serviceLocator.GetService<IRequestProcessor>();
            _appSettings = serviceLocator.GetService<IAppSettings>();

            _userContext = new UserContext(_userManager, _appSettings, _processor, this);
            _cartService = new CartService(_appSettings, _userContext, _mapper, _processor);
        }

        protected BaseController()
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

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            var layoutModel = ((Controller)filterContext.Controller).ViewData.Model as LayoutViewModel;
            if(layoutModel != null)
                LayoutResolverHelper.InitLayout(layoutModel, Helper);
        }
        #region Redirect end user to another pages
        protected ActionResult GetHomePageToRedirect()
        {
            return RedirectToAction("Index", "Home");
        }

        protected ActionResult GetShoppingPageToRedirect()
        {
            return RedirectToAction("Index", "Shopping");
        }

        protected ActionResult GetMemberPageToRedirect()
        {
            return RedirectToAction("Member", "Account");
        }
        #endregion

        #region Populate metadata of current user
        protected void PopulateErrorMessage(string errorMessage)
        {
            ViewBag.ErrorMessage = errorMessage;
        }

        protected async Task<Guid> GetOrCreateDefaultCustomerId()
        {
            var customer = await _userContext.GetCurrentContact();
            if(customer == null)
            {
                return Guid.NewGuid();
            }
            return customer.ContactId;
        }

        protected async Task<Guid> GetCustomerId()
        {
            var customer = await _userContext.GetCurrentContact();
            if (customer == null)
            {
                return Guid.Empty;
            }
            return customer.ContactId;
        }

        protected ActionResult SuccessActionJson(string message)
        {
            return Json(new { @success = true, @message = message });
        }

        protected ActionResult ReturnFailJson(string message)
        {
            return Json(new { @success = false, @message = message });
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _scope.Dispose();
            }
        }
    }
}
