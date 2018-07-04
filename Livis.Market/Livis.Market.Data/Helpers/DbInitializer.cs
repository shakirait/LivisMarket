using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Livis.Market.Data.Helpers
{
    public static class DbInitializer
    {
        public static void Initialize(LivisMarketContext context)
        {
            context.Database.EnsureCreated();
            if (context.Categories.Any())
            {
                return;
            }

            #region Init Categories
            var cat1 = new Category() { Name = "Toys, Gadgets" };
            var cat2 = new Category() { Name = "Apparel" };
            var cat3 = new Category() { Name = "MISC" };
            var cat4 = new Category() { Name = "Health and Beauty" };
            var cat5 = new Category() { Name = "Jewelry" };
            var cat6 = new Category() { Name = "Flower" };

            context.Categories.Add(cat1);
            context.Categories.Add(cat2);
            context.Categories.Add(cat3);
            context.Categories.Add(cat4);
            context.Categories.Add(cat5);
            #endregion
            context.SaveChanges();
        }
    }
}
