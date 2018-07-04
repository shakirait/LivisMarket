using LivinMarket.Product.Model.Commands.Business;
using Livis.Market.Data;
using Livis.Market.Infrastructure;
using Livis.Market.Utilities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivinMarket.Product.CommandHandlers.Business
{
    public class DeleteProductCommandHandler : CommandHandler<DeleteProductCommand>
    {
        protected async override Task HandleCommandAsync(DeleteProductCommand command)
        {
            var product = _context.Products.FirstOrDefault(p => p.Sku.Equals(command.Sku));
            if(product != null)
            {
                var oldPartnerPhoto = _context.ProductPhotoForPartners.Where(x => x.ProductId == product.ProductId);
                if (oldPartnerPhoto.Count() > 0)
                {
                    _context.ProductPhotoForPartners.RemoveRange(oldPartnerPhoto);
                }
                var oldPhoto = _context.ProductPhotos.Where(x => x.ProductId == product.ProductId);
                if (oldPhoto.Count() > 0)
                {
                    _context.ProductPhotos.RemoveRange(oldPhoto);
                }

                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            
        }
    }
}
