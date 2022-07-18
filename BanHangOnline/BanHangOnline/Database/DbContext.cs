using BanHangOnline.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BanHangOnline.Database
{
    public class DataContext : IdentityDbContext<IdentityUser, IdentityRole, string, IdentityUserClaim<string>, IdentityUserRole<string>,
     IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public DbSet<UserProfileViewModel> UserProfile { get; set; }
        public DbSet<GenderViewModel> Gender { get; set; }
        public DbSet<WebInfoViewModels> WebInfo { get; set; }
        public DbSet<WebBannerViewModel> WebBanner { get; set; }
        public DbSet<WebBannerGroupViewModel> WebBannerGroup { get; set; }
        public DbSet<CategoryViewModel> category { get; set; }
        public DbSet<ProductViewModel> Product { get; set; }
        public DbSet<ProductImageViewModel> ProductImage { get; set; }
        public DbSet<ProductCategoryViewModel> ProductCategory { get; set; }
        public DbSet<ContactViewModel> Contact { get; set; }
        public DbSet<WishProductViewModel> WishProduct { get; set; }
        public DataContext(DbContextOptions<DataContext> options)
           : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityUser>();
        }


    }
}
