using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Livis.Market.Data
{
    public enum OrderStatus
    {
        [Display(Name = "On Hold")]
        OnHold = 1,
        [Display(Name = "Partially Shipped")]
        PartiallyShipped = 2,
        [Display(Name = "In Progress")]
        InProgress = 3,
        [Display(Name = "Completed")]
        Completed = 4,
        [Display(Name = "Cancelled")]
        Cancelled = 5,
        [Display(Name = "Awaiting Exchange")]
        AwaitingExchange = 6
    }
}
