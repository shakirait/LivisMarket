
namespace Livis.Market.Models.ViewModel
{
    
    public class ShoppingViewModel : LayoutViewModel
    {
       public ProductShoppingViewModel[] Products { get; set; }
    }

    public class ProductShoppingViewModel
    {
        public string Sku { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Image { get; set; }
    }
}
