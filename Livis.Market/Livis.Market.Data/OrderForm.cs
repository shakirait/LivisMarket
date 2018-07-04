using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Livis.Market.Data
{
    public class OrderForm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long OrderFormId { get; set; }
        public long? OrderGroupId { get; set; }
        [ForeignKey("OrderGroupId")]
        public virtual OrderGroup OrderGroup { get; set; }
        public string Name { get; set; }
        public Guid? BillingAddressId { get; set; }
        [ForeignKey("BillingAddressId")]
        public virtual CustomerAddress BillingAddress { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ShippingTotal { get; set; }
        public decimal HandlingTotal { get; set; }
        public decimal TaxTotal { get; set; }
        public decimal Total { get; set; }
        public PaymentStatus Status { get; set; }
        public string ReturnComment { get; set; }
        public decimal AuthorizedPaymentTotal { get; set; }
        public decimal CapturedPaymentTotal { get; set; }
        public string Data { get; set; }
    }
}
