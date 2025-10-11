using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Application.Common.Interfaces;
using SimpleShop.Application.Products.Queries.GetCategoryDropDown;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Application.Products.Queries.GetCategoryDropdown
{
    public class GetCategoryDropdownQueryHandler : IRequestHandler<GetCategoryDropdownQuery, List<CategoryDropdownDTO>>
    {
        private readonly IApplicationDbContext _context;
        public GetCategoryDropdownQueryHandler(IApplicationDbContext context) => _context = context;
        public async Task<List<CategoryDropdownDTO>> Handle(GetCategoryDropdownQuery request, CancellationToken cancellationToken)
        {
            return await _context.Categories
                .AsNoTracking()
                .Select(c => new CategoryDropdownDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                }).ToListAsync(cancellationToken);
        }
    }
}
