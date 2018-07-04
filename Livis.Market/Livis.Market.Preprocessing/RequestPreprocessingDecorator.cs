using Livis.Market.Infrastructure;
using Livis.Market.Preprocessing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livis.Market.Preprocessing
{
    public class RequestPreprocessingDecorator<TRequest, TResponse> : RequestHandler<TRequest, TResponse>
          where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IPreprocessor<TRequest>> _preprocessors;
        private readonly IRequestHandler<TRequest, TResponse> _inner;

        public RequestPreprocessingDecorator(
            IEnumerable<IPreprocessor<TRequest>> preprocessors,
            IRequestHandler<TRequest, TResponse> inner)
        {
            _preprocessors = preprocessors;
            _inner = inner;
        }

        public override async Task<TResponse> HandleAsync(TRequest request)
        {
            foreach (var preprocessor in _preprocessors)
            {
                await preprocessor.ProcessAsync(request);
            }

            return await _inner.HandleAsync(request);
        }
    }
}