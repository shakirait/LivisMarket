using Livis.Market.Models.ViewModel.ComponentViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livis.Market.Models.ViewModel
{
    public class LayoutViewModel
    {
        public FooterViewModel Footer { get; set; }

        public HeaderViewModel Header { get; set; }

        public string Title { get; set; }
    }
}
