using Livis.Market.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using Livis.Market.Data;

namespace LivinMarket.Product.Model.Commands.Business
{
    public class UpdateProductCommand : ICommand
    {
        public Guid ProductId { get; set; }
        public string OriginalLinks { get; set; }
        public string Name { get; set; }
        public decimal? Length { get; set; }
        public decimal? Width { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public decimal Cost { get; set; }
        public decimal SuggestedPrice { get; set; }
        public string Description { get; set; }
        public string Videos { get; set; }
        public string Resources { get; set; }
        public Guid? CategoryId { get; set; }
        public virtual ICollection<ProductPhoto> Photos { get; set; }
        public virtual ICollection<ProductPhotoForPartner> PartnerPhotos { get; set; }
        public string Sku { get; set; }
        public string VariantOptions { get; set; }
        public string VariantKeys { get; set; }
        public string Prices { get; set; }
        public LevisUser UserConfirmation { get; set; }
    }
}
