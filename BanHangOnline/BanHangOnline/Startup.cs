using BanHangOnline.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanHangOnline
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            environment = env;
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment environment;
        // This method gets called by the runtime. Use this method to add services to the container.

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();           // Đăng ký dịch vụ lưu cache trong bộ nhớ (Session sẽ sử dụng nó)
            services.AddSession(cfg =>
            {                    // Đăng ký dịch vụ Session
                cfg.Cookie.Name = "ShopBanHang";             // Đặt tên Session - tên này sử dụng ở Browser (Cookie)
                cfg.IdleTimeout = new TimeSpan(0, 30, 0);    // Thời gian tồn tại của Session
            });
            services.AddControllersWithViews();
            services.AddDbContext<DataContext>(
             options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));
            //services.AddIdentity<ApplicationUser,Role>().AddEntityFrameworkStores<DbContext>();
            //services.AddIdentity<IdentityUser, IdentityRole>()
            //   .AddEntityFrameworkStores<DataContext>()
            //   .AddDefaultTokenProviders();

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
            })
                .AddEntityFrameworkStores<DataContext>()
               .AddDefaultTokenProviders();


            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddSession(options =>
            {
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.IsEssential = true;
            });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseExceptionHandler("/ErrorPage");
                //// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();

                app.UseExceptionHandler("/Error/500");
                app.UseStatusCodePagesWithRedirects("/Error/{0}");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseSession();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // Router manager
                endpoints.MapAreaControllerRoute(
                    name: "Manager",
                    areaName: "Manager",
                    pattern: "Manager/{controller=Home}/{action=Index}/{id?}");

                // Router mặc định
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                // Router xóa cart
                endpoints.MapControllerRoute(
                    name: "login",
                    pattern: "dang-nhap",
                    defaults: new { controller = "Account", action = "Login" });

                endpoints.MapControllerRoute(
                    name: "Register",
                    pattern: "dang-ky",
                    defaults: new { controller = "Account", action = "Register" });

                // Router trang lỗi
                endpoints.MapControllerRoute(
                    name: "error",
                    pattern: "error",
                    defaults: new { controller = "Home", action = "Error" });

                // Router Lỗi
                endpoints.MapControllerRoute(
                    name: "NotFound",
                    pattern: "NotFound",
                    defaults: new { controller = "Home", action = "NotFound" });

                // Router danh mục sản phẩm
                endpoints.MapControllerRoute(
                    name: "danh-muc-san-pham",
                    pattern: "{controller=Home}/{action=Index}/{page?}");

                // Router danh mục sản phẩm
                endpoints.MapControllerRoute(
                    name: "CategoryProduct",
                    pattern: "danh-muc-san-pham",
                    defaults: new { controller = "Product", action = "ProductCategory" });
                
                endpoints.MapControllerRoute(
                    name: "SalePage",
                    pattern: "san-pham-khuyen-mai",
                    defaults: new { controller = "Product", action = "SalePage" });

                endpoints.MapControllerRoute(
                name: "CartPage",
                pattern: "Gio-hang",
                defaults: new { controller = "Product", action = "Cart" });

                endpoints.MapControllerRoute(
                    name: "tim-kiem-san-pham",
                    pattern: "{controller=Search}/{action=Index}/{page?}");

                endpoints.MapControllerRoute(
                    name: "Search",
                    pattern: "tim-kiem-san-pham/{currentPageIndex:int?}",
                    defaults: new { controller = "Search", action = "SearchForm" });

                // Router chi tiết sản phẩm
                endpoints.MapControllerRoute(
                    name: "ProductDetail",
                    pattern: "chi-tiet-san-pham/{ProductDetailUrl?}",
                    defaults: new { controller = "Product", action = "ProductDetail" });

                endpoints.MapControllerRoute(
                  name: "ProductList",
                  pattern: "danh-muc-san-pham-{categoryUrl}",
                  defaults: new { controller = "Product", action = "ProductList" });

                // Router danh mục yêu thích
                endpoints.MapControllerRoute(
                    name: "WishList",
                    pattern: "san-pham-yeu-thich",
                    defaults: new { controller = "Product", action = "WishList" });

                // Router thanh toán
                endpoints.MapControllerRoute(
                    name: "CheckOut",
                    pattern: "thanh-toan",
                    defaults: new { controller = "Product", action = "CheckOut" });

                endpoints.MapControllerRoute(
                    name: "SalePage",
                    pattern: "khuyen-mai-san-pham",
                    defaults: new { controller = "Product", action = "SalePage" });

                // Router giỏ hàng
                endpoints.MapControllerRoute(
                    name: "WishList",
                    pattern: "thanh-toan",
                    defaults: new { controller = "Product", action = "Cart" });

                // Router add to card
                endpoints.MapControllerRoute(
                    name: "addcart",
                    pattern: "gio-hang/{Id?}",
                    defaults: new { controller = "Product", action = "AddToCart" });

                // Router update cart
                endpoints.MapControllerRoute(
                    name: "updatecart",
                    pattern: "sua-san-pham",
                    defaults: new { controller = "Product", action = "UpdateCart" });

                // Router xóa cart
                endpoints.MapControllerRoute(
                    name: "removecart",
                    pattern: "xoa-san-pham/{Id?}",
                    defaults: new { controller = "Product", action = "Removecart" });

                // Router xóa cart
                endpoints.MapControllerRoute(
                    name: "SearchProduct",
                    pattern: "tim-kiem-san-pham",
                    defaults: new { controller = "search", action = "SearchProduct" });
            });
        }
    }
}
