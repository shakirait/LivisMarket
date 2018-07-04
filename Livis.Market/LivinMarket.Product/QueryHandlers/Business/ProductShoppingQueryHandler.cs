using Livis.Market.Data;
using Livis.Market.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using LivinMarket.Product.Model.Queries.Business;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace LivinMarket.Product.QueryHandlers.Business
{
    public class ProductShoppingQueryHandler : QueryHandler<ProductShoppingQuery, ProductShoppingResponse>
    {
        // Todo: Thanh.nq will enhance and support different currency in system
        private decimal GetPrice(Livis.Market.Data.Product product, CustomerGroup customerGroup, string currency)
        {
            if(string.IsNullOrEmpty(product.Prices))
            {
                return 0;
            }
            // we are not only support get price base on customer group but also support base on currency such VND, EUR, USD, CAD, GBP
            // we will enhance this feature after finishing Livis Rose and Livis Market
            /// Now we will assume all price with currency : EUR
            var prices = JsonConvert.DeserializeObject<List<ProductPrice>>(product.Prices);
            return prices.First(x => x.CustomerGroup == customerGroup).Price;
        }
        private bool IsVariantIdMatch(Livis.Market.Data.Product product, string variantKey)
        {
            if (product.VariantKeys == null)
                return false;
            var variants = JsonConvert.DeserializeObject<List<Variant>>(product.VariantKeys);
            if (variants == null)
                return false;
            return variants.Any(v => v.Id == variantKey);
        }
        protected override async Task<ProductShoppingResponse>  HandleQueryAsync(ProductShoppingQuery query)
        {
            ProductShoppingResponse response = new ProductShoppingResponse()
            {
                ProductsShopping = new ProductShoppingView[] { }
            };
            if (string.IsNullOrEmpty(query.SkuOrSkuVariantOrName))
            {
                response.ProductsShopping = _context.Products.Include(x => x.PartnerPhotos).Where(x => x.PartnerPhotos != null && x.PartnerPhotos.Count > 0).OrderByDescending(x => x.Modified).ToList().Select(x => new ProductShoppingView() {
                    Name = x.Name,
                    Sku= x.Sku,
                    Price = GetPrice(x, query.CustomerGroup, query.Currency),
                    MainImageUrl = x.PartnerPhotos.First().BlobUrl,
                    Description = x.Description,
                    VariantKeys = x.VariantKeys,
                    PartnerPhotos = x.PartnerPhotos
                }).Where(x => x.Price > 0).ToArray();
                return response;
            }
            var product = _context.Products.Include(x => x.PartnerPhotos).FirstOrDefault(x => x.Sku.Equals(query.SkuOrSkuVariantOrName));
            if(product != null)
            {
                response.ProductsShopping = new ProductShoppingView[] {
                    new ProductShoppingView() {
                        Name = product.Name,
                        Sku= product.Sku,
                        Price = GetPrice(product, query.CustomerGroup, query.Currency),
                        MainImageUrl = product.PartnerPhotos.First().BlobUrl,
                        Description = product.Description,
                        VariantKeys = product.VariantKeys,
                        PartnerPhotos = product.PartnerPhotos,
                        Options = product.VariantOptions
                    }
                };
                return response;
            }

            product = _context.Products.Include(x => x.PartnerPhotos).FirstOrDefault(x => IsVariantIdMatch(x, query.SkuOrSkuVariantOrName));
            if (product != null)
            {
                response.ProductsShopping = new ProductShoppingView[] {
                    new ProductShoppingView() {
                        Name = product.Name,
                        Sku= product.Sku,
                        Price = GetPrice(product, query.CustomerGroup, query.Currency),
                        MainImageUrl = product.PartnerPhotos.First().BlobUrl,
                        Description = product.Description,
                        VariantKeys = product.VariantKeys,
                        PartnerPhotos = product.PartnerPhotos
                    }
                };
                return response;
            }

            response.ProductsShopping = _context.Products.Include(x => x.PartnerPhotos).Where(x => x.Name.Contains(query.SkuOrSkuVariantOrName) && x.PartnerPhotos != null && x.PartnerPhotos.Count > 0).OrderByDescending(x => x.Modified).ToList().Select(x => new ProductShoppingView()
            {
                Name = x.Name,
                Sku = x.Sku,
                Price = GetPrice(x, query.CustomerGroup, query.Currency),
                MainImageUrl = x.PartnerPhotos.First().BlobUrl,
                Description = x.Description,
                VariantKeys = x.VariantKeys,
                PartnerPhotos = x.PartnerPhotos
            }).Where(x => x.Price > 0).ToArray();
            return response;
        }
    }
}
