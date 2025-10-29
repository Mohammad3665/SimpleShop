using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Application.Common.Interfaces;
using SimpleShop.Application.Products.Queries.GetPublicProductList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Application.Products.Queries.SearchProducts
{
    public class SearchProductsQueryHandler : IRequestHandler<SearchProductsQuery, List<PublicProductDTO>>
    {
        private readonly IApplicationDbContext _context;
        public SearchProductsQueryHandler(IApplicationDbContext context) => _context = context;

        public async Task<List<PublicProductDTO>> Handle(SearchProductsQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.SearchTerm))
                return new List<PublicProductDTO>();

            var term = request.SearchTerm.Trim().ToLower();
            return await _context.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .Where(p => p.Stock > 0 && (p.Name.ToLower().Contains(term)))
                .Select(p => new PublicProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    CategoryName = p.Category.Name,
                    IsInStock = p.Stock > 0,
                    Price = p.Price,
                    MainImageUrl = p.ImageName
                }).ToListAsync(cancellationToken);
        }
    }
}
