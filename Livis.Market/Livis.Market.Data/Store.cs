using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Livis.Market.Data
{
    public class Store
    {
        public Store()
        {
            StoreProducts = new List<StoreProduct>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid StoreId { get; set; }
        public Guid? ContactId { get; set; }
        [ForeignKey("ContactId")]
        public virtual CustomerContact Contact { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual LevisUser CreateBy { get; set; }
        public string StoreName { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public virtual ICollection<StoreProduct> StoreProducts { get; set; }
    }
}
