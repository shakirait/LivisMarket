using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livis.Market.Models.ViewModel.ComponentViewModels
{
    public class HeaderViewModel : BaseComponentViewModel
    {
        public virtual string LogoImageHeader { get; set; }
        public LinkItemCollection NavigationLinksAreaHeader { get; set; }
        public virtual MembershipHeaderViewModel MembershipHeaderViewModel { get; set; }

    }
}
