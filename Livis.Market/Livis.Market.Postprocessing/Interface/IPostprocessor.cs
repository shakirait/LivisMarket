using System.Threading.Tasks;

namespace Livis.Market.Postprocessing
{
    public interface IPostprocessor
    {
    }

    public interface IPostprocessor<TRequest, TResponse> : IPostprocessor
    {
        Task ProcessAsync(TRequest request, TResponse response);
    }
}