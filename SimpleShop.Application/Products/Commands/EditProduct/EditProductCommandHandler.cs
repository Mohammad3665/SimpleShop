using MediatR;
using SimpleShop.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Application.Products.Commands.EditProduct
{
    public class EditProductCommandHandler : IRequestHandler<EditProductCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileService _fileService;

        public EditProductCommandHandler(IApplicationDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<Unit> Handle(EditProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Products.FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity == null)
            {
                throw new Exception("Product not found.");
            }

            if (request.NewImageFile is not null)
            {
                _fileService.DeleteFile(request.ExistingImageName, "Products/Images");
                var newFileName = await _fileService.SaveFileAsync(request.NewImageFile, "Products/Images");
                entity.ImageName = newFileName;
            }
            entity.Name = request.Name;
            entity.Description = request.Description;
            entity.Price = request.Price;
            entity.Stock = request.Stock;
            entity.CategoryId = request.CategoryId;
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
