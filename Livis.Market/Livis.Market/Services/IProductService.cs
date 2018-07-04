using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Livis.Market.Services
{
    public interface IProductService
    {
        Task<decimal> GetProductPrice(string sku);
    }
}
