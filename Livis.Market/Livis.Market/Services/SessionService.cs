using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Livis.Market.Extensions;
using Livis.Market.Infrastructure;

namespace Livis.Market.Services
{
    public class SessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ISession Session
        {
            get
            {
                return _session;
            }
        }

        public bool IsAvailable()
        {
            return Session != null;
        }

        public bool IsInOneTimePurchaseFlow()
        {
            if (Session == null)
                return false;
            bool oneTime = Session.Get<bool>(SessionConstant.OneTimePurchase);
            return oneTime;
        }

        public void SetInOneTimePurchaseFlow()
        {
            if (Session == null)
                return;
            Session.Set<bool>(SessionConstant.OneTimePurchase, true);
        }

        public void ClearInOneTimePurchaseFlow()
        {
            if (Session == null)
                return;
            Session.Remove(SessionConstant.OneTimePurchase);
        }
    }
}
