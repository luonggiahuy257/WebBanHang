using BanHangOnline.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BanHangOnline.Database
{
    public class DataContext : IdentityDbContext<IdentityUser, IdentityRole, string, IdentityUserClaim<string>, IdentityUserRole<string>,
     IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public DbSet<WebInfoViewModels> WebInfo { get; set; }
        public DbSet<WebBannerViewModel> WebBanner { get; set; }
        public DbSet<WebBannerGroupViewModel> WebBannerGroup { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
           : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityUser>();
        }
        public DbSet<BanHangOnline.ViewModels.categoryViewModel> categoryViewModel { get; set; }
        public DbSet<BanHangOnline.ViewModels.ProductViewModel> ProductViewModel { get; set; }

       
    }
}
