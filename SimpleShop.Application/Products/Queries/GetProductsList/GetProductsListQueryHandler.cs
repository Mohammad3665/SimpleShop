using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Application.Products.Queries.GetProductsList
{
    public class GetProductsListQueryHandler : IRequestHandler<GetProductsListQuery, List<ProductListDTO>>
    {
        private readonly IApplicationDbContext _context;
        public GetProductsListQueryHandler(IApplicationDbContext context) => _context = context;
        public async Task<List<ProductListDTO>> Handle(GetProductsListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .Select(p => new ProductListDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Stock = p.Stock,
                    ImageName = p.ImageName,
                    CategoryName = p.Category.Name
                }).ToListAsync(cancellationToken);
        }
    }
}
