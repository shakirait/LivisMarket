
using LivinMarket.Order.Model.Commands.Business;
using Livis.Market.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LivinMarket.Order.CommandHandlers.Business
{
    public class DeleteCartCommandHandler : CommandHandler<DeleteCartCommand>
    {
        protected async override Task HandleCommandAsync(DeleteCartCommand command)
        {
            var cart = _context.Cart.FirstOrDefault(x => x.CartId == command.CartView.Id);
            if (cart != null)
            {
                _context.Cart.Remove(cart);
                _context.SaveChanges();
            }
        }
    }
}
