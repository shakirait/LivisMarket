using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Livis.Market.Data
{
    [Table("Product")]
    public class Product
    {
        public Product()
        {
            StoreProducts = new List<StoreProduct>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ProductId { get; set; }
        [Display(Name = "Original Links")]
        public string OriginalLinks { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Length")]
        public decimal? Length { get; set; }
        [Display(Name = "Width")]
        public decimal? Width { get; set; }
        [Display(Name = "Height")]
        public decimal? Height { get; set; }
        [Display(Name = "Weight")]
        public decimal? Weight { get; set; }
        [Display(Name = "Cost")]
        public decimal Cost { get; set; }
        [Display(Name = "Suggested Price")]
        public decimal SuggestedPrice { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Videos")]
        public string Videos { get; set; }
        [Display(Name = "Resources")]
        public string Resources { get; set; }
        public Guid? CategoryId { get; set; }
        [JsonIgnore]
        [Display(Name = "Categories")]
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        [JsonIgnore]
        [Display(Name = "Images")]
        public virtual ICollection<ProductPhoto> Photos { get; set; }
        [JsonIgnore]
        [Display(Name = "Images for Partner")]
        public virtual ICollection<ProductPhotoForPartner> PartnerPhotos { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual LevisUser CreatedBy { get; set; }
        [JsonIgnore]
        public virtual ICollection<StoreProduct> StoreProducts { get; set; }
        [Display(Name = "SKU")]
        public string Sku { get; set; }
        [Display(Name = "Variants")]
        public string VariantOptions { get; set; }
        [Display(Name = "VariantKey")]
        public string VariantKeys { get; set; }
        [Display(Name = "Prices")]
        public string Prices { get; set; }
        public DateTime Modified { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime Created { get; set; }
    }

    public class ProductPrice
    {
        public CustomerGroup CustomerGroup { get; set; }
        public decimal Price { get; set; }
    }

    public class Variant
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string BlockUrl { get; set; }
        public string Color { get; set; }
    }
}
