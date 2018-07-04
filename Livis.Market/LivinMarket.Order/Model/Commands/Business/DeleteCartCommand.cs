using Livis.Market.Data;
using Livis.Market.Infrastructure;

namespace LivinMarket.Order.Model.Commands.Business
{
    public class DeleteCartCommand : ICommand
    {
        public CartView CartView { get; set; }
    }
}
