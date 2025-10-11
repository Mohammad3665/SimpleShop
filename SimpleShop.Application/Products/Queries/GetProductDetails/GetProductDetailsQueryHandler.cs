using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Application.Common.Interfaces;
using SimpleShop.Application.Products.Commands.EditProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Application.Products.Queries.GetProductDetails
{
    public class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQuery, EditProductCommand>
    {
        private readonly IApplicationDbContext _context;
        public GetProductDetailsQueryHandler(IApplicationDbContext context) => _context = context;

        public async Task<EditProductCommand> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            if (entity == null) return null;
            return new EditProductCommand
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Price = entity.Price,
                Stock = entity.Stock,
                CategoryId = entity.CategoryId,
                ExistingImageName = entity.ImageName
            };
        }
    }
}
