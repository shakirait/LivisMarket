
using Livis.Market.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System;
using LivinMarket.Product.Model.Queries.Business;
using Microsoft.EntityFrameworkCore;

namespace LivinMarket.Product.QueryHandlers.Business
{
    public class ProductQueryHandler : QueryHandler<ProductQuery, ProductResponse>
    {

        protected override async Task<ProductResponse>  HandleQueryAsync(ProductQuery query)
        {
            ProductResponse response = new ProductResponse() {
                Products = new Livis.Market.Data.Product[] { }
            };
            if (string.IsNullOrEmpty(query.NameOrId))
            {
                response.Products = _context.Products.Include(p => p.Photos).ToArray();
                return response;
            }
            var foundGuid = Guid.Empty;
            if(Guid.TryParse(query.NameOrId, out foundGuid))
            {
                var foundEntityById = _context.Products.FirstOrDefault(x => x.ProductId.Equals(foundGuid));
                if(foundEntityById != null)
                {
                    response.Products = new Livis.Market.Data.Product[] { foundEntityById };
                    return response;
                }
            }
            var foundEntityByName = _context.Products.Include(p => p.Photos).Include(p => p.PartnerPhotos).FirstOrDefault(x => x.Name.Equals(query.NameOrId) || x.Sku.Equals(query.NameOrId));
            ;
            if (foundEntityByName != null)
            {
                response.Products = new Livis.Market.Data.Product[] { foundEntityByName };
                return response;
            }

            return response;
        }
    }
}
