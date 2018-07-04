using Livis.Market.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Livis.Market.Validation
{
    public class RequestValidationDecorator<TRequest, TResponse> : RequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly IRequestHandler<TRequest, TResponse> _inner;
        private readonly IServiceProvider _serviceProvider;
        public RequestValidationDecorator(
            IEnumerable<IValidator<TRequest>> validators,
            IRequestHandler<TRequest, TResponse> inner,
            IServiceProvider serviceProvider)
        {
            _validators = validators;
            _inner = inner;
            _serviceProvider = serviceProvider;
        }

        public override async Task<TResponse> HandleAsync(TRequest request)
        {
            foreach (var validator in _validators)
            {
                await validator.ValidateAsync(request);
            }
            _inner.InitializeServices(_serviceProvider);
            return await _inner.HandleAsync(request);
        }
    }
}
