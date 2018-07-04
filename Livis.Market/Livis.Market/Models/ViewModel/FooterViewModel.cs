using Livis.Market.Models.ViewModel.ComponentViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livis.Market.Models.ViewModel
{
    public class FooterViewModel : BaseComponentViewModel
    {
        public virtual LinkItemCollection SocialFooter { get; set; }

        public virtual LinkItemCollection NavigationLinksAreaFooter { get; set; }

        public virtual FooterInfo CustomerServiceFooter { get; set; }

        public virtual string CopyrightTextFooter { get; set; }

    }

    public class FooterInfo
    {
        public virtual string Alias { get; set; }
        public virtual string OpenTime { get; set; }
        public virtual string Tel { get; set; }
    }
}
