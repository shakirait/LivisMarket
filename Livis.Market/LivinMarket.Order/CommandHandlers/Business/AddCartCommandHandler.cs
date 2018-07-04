
using LivinMarket.Order.Model.Commands.Business;
using Livis.Market.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LivinMarket.Order.CommandHandlers.Business
{
    public class AddCartCommandHandler : CommandHandler<AddCartCommand>
    {
        protected async override Task HandleCommandAsync(AddCartCommand command)
        {
            var cart = new Livis.Market.Data.SerializableCart()
            {
                Created = command.CartView.Created,
                Modified = command.CartView.Modified,
                CustomerId = command.CartView.CustomerId,
                MarketId = command.CartView.Market,
                Name = command.CartView.Name,
            };
            _context.Cart.Add(cart);
            _context.SaveChanges();
            command.CartView.Id = cart.CartId;
            cart.Data = JsonConvert.SerializeObject(command.CartView);
            _context.SaveChanges();
        }
    }
}
