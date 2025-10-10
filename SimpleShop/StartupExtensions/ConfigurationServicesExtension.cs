using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Application.Common.Interfaces;
using SimpleShop.Domain.IdentityEntities;
using SimpleShop.Infrastructure.DatabaseContext;

namespace SimpleShop.Web.StartupExtensions
{
    public static class ConfigurationServicesExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews();

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<SimpleShopDbContext>());

            services.AddDbContext<SimpleShopDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            );
            //enable identity in this project
            _ = services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<SimpleShopDbContext>()
            .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
            });
            return services;
        }
    }
}
