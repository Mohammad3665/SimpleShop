using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SimpleShop.Application.Common.Interfaces;
using SimpleShop.Domain.IdentityEntities;
using SimpleShop.Infrastructure.Data;
using SimpleShop.Web.StartupExtensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureServices(builder.Configuration);
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<IApplicationDbContext>();
    var midator = services.GetRequiredService<IMediator>();
    await CategorySeed.SeedCategories(context, midator);
    await IdentitySeed.CreateRoles(services);
}


app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting(); //Identifying action method based route
app.UseAuthentication(); //Reading Identity coockie
app.UseAuthorization();
app.MapControllers(); //Execute the filter pipline (action + filters)
app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
        );

    _ = endpoints.MapControllerRoute(
        name: "Default",
        pattern: "{controller=Product}/{action=Index}/{id?}"
        );
});
app.Run();
