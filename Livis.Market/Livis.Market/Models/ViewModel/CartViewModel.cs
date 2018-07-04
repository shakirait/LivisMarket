using Livis.Market.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livis.Market.Models.ViewModel
{
    public class CartShippingViewModel  : CartViewModel
    {
        public ShippingView ShippingView { get; set; }
    }
    public class CartViewModel : LayoutViewModel
    {
        public string CssCartClass { get; set; }
        public string CssCustomerInfoClass { get; set; }
        public string CssPaymentMethodClass { get; set; }
        public string CssConfirmationClass { get; set; }
        public string CssOrderCompleteClass { get; set; }

        public string LinkCart { get; set; }
        public string LinkCustomerInfo { get; set; }
        public string LinkPaymentMethod { get; set; }
        public string LinkConfirmation { get; set; }
        public string LinkOrderComplete { get; set; }
    }
}