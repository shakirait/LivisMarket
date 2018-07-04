using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Livis.Market.Data
{
    public enum CustomerGroup
    {
        [Display(Name = "Customer")]
        Customer = 1,
        [Display(Name = "Agency")]
        Agency = 2,
        [Display(Name = "Non-Member")]
        NonMember = 3
    }
}
