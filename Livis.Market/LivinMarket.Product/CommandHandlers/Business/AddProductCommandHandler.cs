using LivinMarket.Product.Model.Commands.Business;
using Livis.Market.Data;
using Livis.Market.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LivinMarket.Product.CommandHandlers.Business
{
    public class AddProductCommandHandler : CommandHandler<AddProductCommand>
    {
        protected async override Task HandleCommandAsync(AddProductCommand command)
        {
            var sku = RandomString(3);
            while (_context.Products.Any(x => x.Sku.Equals(sku)))
            {
                sku = RandomString(3);
            }
            command.NewProduct.Sku = sku;
            command.NewProduct.Prices = GenerateProductPrices(command.NewProduct.SuggestedPrice);
            _context.Products.Add(command.NewProduct);
            _context.SaveChanges();
        }

        private string GenerateProductPrices(decimal suggestedPrice)
        {
            var prices = new List<ProductPrice>();
            var values = Enum.GetValues(typeof(CustomerGroup)).Cast<CustomerGroup>();
            foreach (var value in values)
            {
                prices.Add(new ProductPrice()
                {
                    CustomerGroup = value,
                    Price = suggestedPrice
                });
            }
            return JsonConvert.SerializeObject(prices);
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
