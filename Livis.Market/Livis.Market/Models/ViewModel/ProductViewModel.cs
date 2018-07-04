using LivinMarket.Product.Model.Queries.Business;
using Livis.Market.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Livis.Market.Models.ViewModel
{
    public class ProductViewModel
    {
        public Guid ProductId { get; set; }
        [Display(Name = "Original Links")]
        public string OriginalLinks { get; set; }
        [Required]
        public string Name { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        [Required]
        public string Weight { get; set; }
        [Required]
        public string Cost { get; set; }
        [Required]
        [Display(Name = "Suggested Price")]
        public string SuggestedPrice { get; set; }
        [Required]
        public string Description { get; set; }
        public string Videos { get; set; }
        public string Resources { get; set; }
        public Guid CategoryId { get; set; }
        public string Sku { get; set; }
        public virtual CategoryModel Category { get; set; }
        public virtual ICollection<ProductPhotoModel> Photos { get; set; }
        [Display(Name = "Partner Photos")]
        public virtual ICollection<ProductPhotoForPartnerModel> PartnerPhotos { get; set; }
        public virtual ICollection<OptionViewModel> Options { get; set; }
        public virtual ICollection<Variant> Variants { get; set; }
        [Display(Name = "Product Prices")]
        public virtual ICollection<ProductPriceView> Prices { get; set; }
    }

    public class ProductView
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Cost { get; set; }
        public string SuggestedPrice { get; set; }
        public string Sku { get; set; }
        public string ImageUrl { get; set; }
        public string ViewUrl { get; set; }
        public string EditUrl { get; set; }
        public string DeleteUrl { get; set; }
    }

    public class ProductPhotoForPartnerModel
    {
        public Guid PhotoId { get; set; }
        public string BlobUrl { get; set; }
    }

    public class ProductPhotoModel
    {
        public Guid PhotoId { get; set; }
        public string BlobUrl { get; set; }
    }

    public class CategoryModel {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
    }

    public class ProductPriceView
    {
        public CustomerGroup CustomerGroup { get; set; }
        public string CustomerGroupName { get; set; }
        public string Price { get; set; }
    }
}
