
using Livis.Market.Data;
using System;

namespace Livis.Market.Models.ViewModel
{
    public class ShippingViewModel : AddressView
    {
        public Guid? CustomerId { get; set; }
    }
}
