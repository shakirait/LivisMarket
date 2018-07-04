using Livis.Market.Data;
using Livis.Market.Infrastructure;

namespace LivinMarket.Order.Model.Commands.Business
{
    public class UpdateCartCommand : ICommand
    {
        public CartView CartView { get; set; }
    }
}
