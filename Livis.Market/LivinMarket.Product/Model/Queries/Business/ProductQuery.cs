using Livis.Market.Data;
using Livis.Market.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace LivinMarket.Product.Model.Queries.Business
{
    public class ProductQuery : IQuery<ProductResponse>
    {
        public string NameOrId { get; set; }
    }

    public class ProductShoppingQuery : IQuery<ProductShoppingResponse>
    {
        public string Currency { get; set; }
        public string SkuOrSkuVariantOrName { get; set; }
        public CustomerGroup CustomerGroup { get; set; }
    }
}