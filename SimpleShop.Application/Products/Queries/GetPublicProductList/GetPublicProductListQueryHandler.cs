using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Application.Common.Interfaces;
using SimpleShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Application.Products.Queries.GetPublicProductList
{
    public class GetPublicProductListQueryHandler : IRequestHandler<GetPublicProductListQuery, List<PublicProductDTO>>
    {
        private readonly IApplicationDbContext _context;
        public GetPublicProductListQueryHandler(IApplicationDbContext context) => _context = context;

        public async Task<List<PublicProductDTO>> Handle(GetPublicProductListQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .Where(p => p.Stock > 0);
            //if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            //{
            //    var term = request.SearchTerm.Trim().ToLower();
            //    query = query.Where(p =>
            //        p.Name.ToLower().Contains(term) ||
            //        p.Description.ToLower().Contains(term)
            //    );
            //}

            if (request.CategoryId.HasValue && request.CategoryId.Value > 0)
            {
                query = query.Where(p => p.CategoryId == request.CategoryId.Value);
            }
            return await query
                .Select(p => new PublicProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    CategoryName = p.Category.Name,
                    IsInStock = p.Stock > 0,
                    Price = p.Price,
                    ThumbnailUrl = p.ImageName,
                }).ToListAsync(cancellationToken);
        }
    }
}
