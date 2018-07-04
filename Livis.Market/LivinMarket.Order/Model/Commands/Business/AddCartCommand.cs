using Livis.Market.Data;
using Livis.Market.Infrastructure;

namespace LivinMarket.Order.Model.Commands.Business
{
    public class AddCartCommand : ICommand
    {
        public CartView CartView { get; set; }
    }
}
