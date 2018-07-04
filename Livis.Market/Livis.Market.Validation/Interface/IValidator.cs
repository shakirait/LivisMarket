
using System.Threading.Tasks;

namespace Livis.Market.Validation
{
    public interface IValidator
    {
    }

    public interface IValidator<TRequest> : IValidator
    {
        Task ValidateAsync(TRequest request);
    }
}
