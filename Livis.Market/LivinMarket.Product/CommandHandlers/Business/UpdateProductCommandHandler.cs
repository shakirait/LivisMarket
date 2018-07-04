using LivinMarket.Product.Model.Commands.Business;
using Livis.Market.Infrastructure;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LivinMarket.Product.CommandHandlers.Business
{
    public class UpdateProductCommandHandler : CommandHandler<UpdateProductCommand>
    {

        protected async override Task HandleCommandAsync(UpdateProductCommand command)
        {
            var oldPartnerPhoto = _context.ProductPhotoForPartners.Where(x => x.ProductId == command.ProductId);
            if(oldPartnerPhoto.Count() > 0)
            {
                _context.ProductPhotoForPartners.RemoveRange(oldPartnerPhoto);
            }
            var oldPhoto = _context.ProductPhotos.Where(x => x.ProductId == command.ProductId);
            if(oldPhoto.Count() > 0)
            {
                _context.ProductPhotos.RemoveRange(oldPhoto);
            }
            var product = _context.Products.First(x => x.ProductId == command.ProductId);
            product.CategoryId = command.CategoryId;
            product.Cost = command.Cost;
            product.Description = command.Description;
            product.Height = command.Height;
            product.Length = command.Length;
            product.Name = command.Name;
            product.OriginalLinks = command.OriginalLinks;
            product.PartnerPhotos = command.PartnerPhotos?.ToList();
            product.Photos = command.Photos?.ToList();
            product.Resources = command.Resources;
            product.SuggestedPrice = command.SuggestedPrice;
            product.VariantKeys = command.VariantKeys;
            product.VariantOptions = command.VariantOptions;
            product.Videos = command.Videos;
            product.Weight = command.Weight;
            product.Width = command.Width;
            product.Prices = command.Prices;
            product.Modified = DateTime.Now;
            product.ModifiedBy = command.UserConfirmation.Id;

            _context.SaveChanges();
        }
    }
}
