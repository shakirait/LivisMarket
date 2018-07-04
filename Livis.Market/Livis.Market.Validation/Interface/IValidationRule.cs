using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Livis.Market.Validation
{
    public interface IValidationRule
    {
    }

    public interface IValidationRule<TRequest> : IValidationRule
    {
        Task TestAsync(TRequest request);
    }
}
