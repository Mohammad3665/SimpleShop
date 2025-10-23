
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Application.Categories.Commands.CreateCategory;
using SimpleShop.Application.Common.Interfaces;

namespace SimpleShop.Infrastructure.Data
{
    public static class CategorySeed
    {
        public static async Task SeedCategories(IApplicationDbContext context, IMediator mediator)
        {
            if (await context.Categories.AnyAsync())
            {
                return;
            }

            var commands = new List<CreateCategoryCommand>
            {
                new CreateCategoryCommand { Name = "Category1", Description = "Cat number 1" },
            };

            foreach (var command in commands)
            {
                await mediator.Send(command);
            }
        }
    }
}