using System;
using System.Threading.Tasks;

namespace Livis.Market.Infrastructure
{
    // Old design we use common type is object to treat all type of system
    public interface IRequestHandler
    {
        void InitializeServices(IServiceProvider serviceProvider);
        Task<Object> HandleAsync(object request);
    }

    // This step is an enhancement. It allow developer define the type rqquest and type response
    public interface IRequestHandler<TRequest, TResponse> : IRequestHandler 
        where TRequest : IRequest<TResponse>
    {
        Task<TResponse> HandleAsync(TRequest request);
    }
}
