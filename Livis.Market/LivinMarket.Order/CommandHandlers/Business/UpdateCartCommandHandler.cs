
using LivinMarket.Order.Model.Commands.Business;
using Livis.Market.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LivinMarket.Order.CommandHandlers.Business
{
    public class UpdateCartCommandHandler : CommandHandler<UpdateCartCommand>
    {
        protected async override Task HandleCommandAsync(UpdateCartCommand command)
        {
            var cart = _context.Cart.First(x => x.CartId == command.CartView.Id);
            cart.Modified = DateTime.Now;
            command.CartView.Modified = DateTime.Now;
            cart.Data = JsonConvert.SerializeObject(command.CartView);
            _context.SaveChanges();
        }
    }
}
