using MediatR;
using SimpleShop.Api.StartupExtensions;
using SimpleShop.Application.Common.Interfaces;
using SimpleShop.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting(); //Identifying action method based route
app.UseAuthentication(); //Reading Identity coockie
app.UseAuthorization();
app.MapControllers(); //Execute the filter pipline (action + filters)
app.Run();
