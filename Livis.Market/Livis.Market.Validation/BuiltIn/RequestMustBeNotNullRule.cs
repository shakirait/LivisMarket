using System;
using System.Threading.Tasks;

namespace Livis.Market.Validation.BuiltIn
{
    public class RequestMustBeNotNullRule<TRequest> : IValidationRule<TRequest>
    {
        public Task TestAsync(TRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return Task.CompletedTask;
        }
    }
}