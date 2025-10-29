using MediatR;
using SimpleShop.Application.Common.Interfaces;
using SimpleShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileService _fileService;
        public CreateProductCommandHandler(IApplicationDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            //var fileName = await _fileService.SaveFileAsync(request.ImageFile, "images/products");
            var entity = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Stock = request.Stock,
                CategoryId = request.CategoryId,
                Images = new List<ProductImage>()
            };
            if (request.ImageFiles is not null &&  request.ImageFiles.Any())
            {
                bool isFirst = true;
                foreach (var file in request.ImageFiles)
                {
                    var fileName = await _fileService.SaveFileAsync(file, "images/products");
                    entity.Images.Add(new ProductImage
                    {
                        FileName = fileName,
                        IsMain = isFirst
                    });
                    isFirst = false;
                }
            }
            entity.ImageName = entity.Images.FirstOrDefault(i => i.IsMain)?.FileName;
            _context.Products.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
