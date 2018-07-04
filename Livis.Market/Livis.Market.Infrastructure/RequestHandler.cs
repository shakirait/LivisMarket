using Autofac;
using AutoMapper;
using Livis.Market.Data;
using Livis.Market.Utilities.IoC;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Livis.Market.Infrastructure
{
    public abstract class RequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        protected LivisMarketContext _context;
        protected SignInManager<LevisUser> _signInManager;
        protected UserManager<LevisUser> _userManager;
        protected IMapper _mapper;
        private Object thisLock = new Object();

        public void InitializeServices(IServiceProvider serviceProvider)
        {
            lock (thisLock)
            {
                _signInManager = _signInManager == null ? serviceProvider.GetService<SignInManager<LevisUser>>() : _signInManager;
                _mapper = _mapper == null ? serviceProvider.GetService<IMapper>() : _mapper;
                _userManager = _userManager == null ? serviceProvider.GetService<UserManager<LevisUser>>() : _userManager;
                _context = _context == null ? serviceProvider.GetService<LivisMarketContext>() : _context;
            }
        }
        public abstract Task<TResponse> HandleAsync(TRequest request);

        public async Task<object> HandleAsync(object request)
        {
            return await HandleAsync((TRequest)request);
        }
    }
}
