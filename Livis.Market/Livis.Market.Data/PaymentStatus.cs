using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Livis.Market.Data
{
    public enum PaymentStatus
    {
        [Display(Name = "Pending")]
        Pending = 1,
        [Display(Name = "Awaiting Payment")]
        AwaitingPayment = 2,
        [Display(Name = "Awaiting Fulfillment")]
        AwaitingFulfillment = 3
    }
}
