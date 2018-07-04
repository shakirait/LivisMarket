using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Livis.Market.Data
{
    public class OrderGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long OrderGroupId { get; set; }
        public Guid InstanceId { get; set; }
        public string Name { get; set; }
        public Guid? CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual CustomerContact Customer { get; set; }
        public string CustomerName { get; set; }
        public Guid? AddressId { get; set; }
        [ForeignKey("AddressId")]
        public virtual CustomerAddress ShippingAddress { get; set; }
        public decimal ShippingTotal { get; set; }
        public decimal HandlingTotal { get; set; }
        public decimal TaxTotal { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public string BillingCurrency { get; set; }
        public OrderStatus Status { get; set; }
        public string Data { get; set; }
    }
}
