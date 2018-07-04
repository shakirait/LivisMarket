using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Livis.Market.Data
{
    public class LineItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long LineItemId { get; set; }
        public long? OrderGroupId { get; set; }
        [ForeignKey("OrderGroupId")]
        public virtual OrderGroup OrderGroup { get; set; }
        public long? OrderFormId { get; set; }
        [ForeignKey("OrderFormId")]
        public virtual OrderForm OrderForm { get; set; }
        public string Sku { get; set; }
        public string VariantId { get; set; }
        public decimal Quantity { get; set; }
        public decimal PlacedPrice { get; set; }
        public decimal ExtendedPrice { get; set; }
        public string DisplayName { get; set; }
        public string ReturnReason { get; set; }
        public string Data { get; set; }
    }
}
