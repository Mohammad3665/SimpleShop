using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SimpleShop.Application.Common.Behaviors;
using SimpleShop.Application.Common.Interfaces;
using SimpleShop.Domain.IdentityEntities;
using SimpleShop.Infrastructure.DatabaseContext;
using SimpleShop.Infrastructure.Services;
using System.Reflection;

namespace SimpleShop.Web.StartupExtensions
{
    public static class ConfigurationServicesExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews();
            services.AddScoped<IPathService, WebPathService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<SimpleShopDbContext>());
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddDbContext<SimpleShopDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            );
            //enable identity in this project
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 4;
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddEntityFrameworkStores<SimpleShopDbContext>()
            .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
            });
            return services;
        }
    }
}
