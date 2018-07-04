
using Livis.Market.Data;
using System.Collections.Generic;

namespace Livis.Market.Models.ViewModel
{
    public class ShoppingProductViewModel : LayoutViewModel
    {
        public string Sku { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string[] Photos { get; set; }
        public virtual IList<Variant> Variants { get; set; }
        public virtual IList<OptionViewModel> Options { get; set; }
    }
}