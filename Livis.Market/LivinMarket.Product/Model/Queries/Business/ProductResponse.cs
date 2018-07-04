using Livis.Market.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace LivinMarket.Product.Model.Queries.Business
{
    public class ProductResponse
    {
       public  Livis.Market.Data.Product[] Products { get; set; }
    }

    public class ProductShoppingView {
        public string Sku { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string MainImageUrl { get; set; }
        public string Description { get; set; }
        public string VariantKeys { get; set; }
        public string Options { get; set; }
        public ICollection<ProductPhotoForPartner> PartnerPhotos { get; set; }
    }
    public class ProductShoppingResponse
    {
        public IEnumerable<ProductShoppingView> ProductsShopping { get; set; }
    }
}
