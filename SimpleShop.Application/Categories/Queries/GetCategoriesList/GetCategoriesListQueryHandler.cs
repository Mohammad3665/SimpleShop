using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Application.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQueryHandler : IRequestHandler<GetCategoriesListQuery, List<CategoryListDTO>>
    {
        private readonly IApplicationDbContext _context;
        public GetCategoriesListQueryHandler(IApplicationDbContext context) => _context = context;
        public async Task<List<CategoryListDTO>> Handle(GetCategoriesListQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.Categories
                .AsNoTracking() 
                .ToListAsync(cancellationToken);
            var dtos = entities.Select(c => new CategoryListDTO
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
            }).ToList();

            return dtos;
        }
    }
}
