using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Livis.Market.Preprocessing
{
    public interface IPreprocessor
    {
    }

    public interface IPreprocessor<TRequest> : IPreprocessor
    {
        Task ProcessAsync(TRequest request);
    }
}
