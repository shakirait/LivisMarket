using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Livis.Market.Data
{

    public class LivisMarketContext : IdentityDbContext<LevisUser>
    {
        public LivisMarketContext(DbContextOptions<LivisMarketContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<StoreProduct>()
                .HasKey(t => new { t.StoreId, t.ProductId });
        }

        public DbSet<CustomerAddress> Addresses { get; set; }
        public DbSet<CustomerContact> Contacts { get; set; }
        public DbSet<Party> Parties { get; set; }
        public DbSet<SerializableCart> Cart { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductPhoto> ProductPhotos { get; set; }
        public DbSet<ProductPhotoForPartner> ProductPhotoForPartners { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Organisation> Organisations { get; set; }
        public DbSet<LineItem> LineItems { get; set; }
        public DbSet<OrderForm> OrderForms { get; set; }
        public DbSet<OrderFormFederatedPayment> OrderFormsFederatedPayment { get; set; }
        public DbSet<OrderGroup> OrderGroups { get; set; }
    }
}
