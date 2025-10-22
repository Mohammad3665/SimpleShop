using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Application.Common.Interfaces;
using SimpleShop.Application.Products.Commands.EditProduct;
using SimpleShop.Application.Products.Queries.GetPublicProductList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Application.Products.Queries.GetProductDetails
{
    public class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQuery, ProductDetailsDTO>
    {
        private readonly IApplicationDbContext _context;
        public GetProductDetailsQueryHandler(IApplicationDbContext context) => _context = context;

        public async Task<ProductDetailsDTO> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (product == null)
            {
                return null;
            }

            var detailsDTO = new ProductDetailsDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                IsInStock = product.Stock > 0,
                ImageFileName = product.ImageName,
                CategoryName = product.Category.Name
            };

            return detailsDTO;
        }
    }
}
