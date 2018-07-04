using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Livis.Market.Data
{

    public class CartView
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string Currency { get; set; }
        public Guid CustomerId { get; set; }
        public IList<OrderFormView> Forms { get; set; }
        public string Market { get; set; }
        public string Name { get; set; }
        public OrderGroupView OrderLink { get; set; }
    }

    public class OrderGroupView
    {
        public long OrderGroupId { get; set; }
        public string Name { get; set; }
        public Guid CustomerId { get; set; }
        public OrderStatus Status { get; set; }
    }

    public class OrderFormView
    {
        public long OrderFormId { get; set; }
        public decimal AuthorizedPaymentTotal { get; set; }
        public decimal CapturedPaymentTotal { get; set; }
        public decimal HandlingTotal { get; set; }
        public string Name { get; set; }
        public IList<ShippingView> Shipment { get; set; }
        public IList<PaymentView> Payments { get; set; }
    }

    public class PaymentView
    {
        public decimal Amount { get; set; }
        public AddressView BillingAddress { get; set; }
        public string CustomerName { get; set; }
        public string PaymentMethodName { get; set; }
        public PaymentStatus Status { get; set; }
        public string ExtendedData { get; set; }
    }

    public class ShippingView {
        // Current system not support type of shipment
        // So we just only contain info shipping
        public AddressView ShippingAddress { get; set; }
        public IList<LineItemView> LineItems { get; set; }
    }
    public class AddressView {
        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Country")]
        [Required]
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        [Display(Name = "Email")]
        [Required]
        public string Email { get; set; }
        [Display(Name = "Street")]
        [Required]
        public string StreetAndHouseNumber { get; set; }
        [Display(Name = "City")]
        [Required]
        public string CityOrTownOrVillage { get; set; }
        [Display(Name = "PostCode")]
        [Required]
        public string PostCode { get; set; }
        [Required]
        public string Prefecture { get; set; }
        [Display(Name = "Phone")]
        [Required]
        public string PhoneNumber { get; set; }
    }
    public class LineItemView
    {
        public long LineItemId { get; set; }
        public string Sku { get; set; }
        public string VariantId { get; set; }
        public decimal Quantity { get; set; }
        public decimal PlacedPrice { get; set; }
        public decimal ExtendedPrice { get; set; }
        public string DisplayName { get; set; }
        public string ImageUrl { get; set; }
    }
}
