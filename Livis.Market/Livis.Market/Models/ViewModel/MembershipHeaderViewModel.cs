using Livis.Market.Models.ViewModel.ComponentViewModels;
using System;

namespace Livis.Market.Models.ViewModel
{
    public class MembershipHeaderViewModel
    {
        public Uri WholesalersLink { get; set; }

        public LinkItemCollection MenuItemMembershipHeader { get; set; }

        public string LoginLink { get; set; }

        public bool IsLevisUser { get; set; }
    }
}
