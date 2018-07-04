
namespace Livis.Market.Infrastructure
{
    // By defaut, we can define VoidReturn as the type response from system
    public interface ICommand : IRequest<VoidReturn>
    {
    }
}
