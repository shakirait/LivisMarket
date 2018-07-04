using Livis.Market.Data;
using Livis.Market.Utilities.Services.UserContext;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Livis.Market.Infrastructure;
using LivinMarket.Product.Model.Queries.Business;

namespace Livis.Market.Services
{
    public class ProductService : IProductService
    {
        protected readonly ILivisUserContext _userContext;
        protected readonly IRequestProcessor _processor;

        public ProductService(ILivisUserContext userContext, IRequestProcessor processor)
        {
            _userContext = userContext;
            _processor = processor;
        }

        public async  Task<decimal> GetProductPrice(string sku)
        {
            var customerGroup = await _userContext.GetCustomerGroup();
            var response  = await _processor.ProcessQueryAsync<ProductQuery, ProductResponse>(new ProductQuery()
            {
                NameOrId = sku
            });
            var products = response.Products;
            if(products != null && products.Length > 0)
            {
                var product = products.FirstOrDefault(p => p.Sku == sku);
                if (product != null)
                {
                    var price = JsonConvert.DeserializeObject<List<ProductPrice>>(product.Prices).FirstOrDefault(p => p.CustomerGroup == customerGroup);
                    if (price != null && price.Price > 0)
                        return product.SuggestedPrice;
                    return product.SuggestedPrice;
                }
            }
            return 0;
        }
    }
}
