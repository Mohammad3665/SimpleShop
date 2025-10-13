using MediatR;
using SimpleShop.Application.Common.Interfaces;

namespace SimpleShop.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileService _fileService;

        public DeleteProductCommandHandler(IApplicationDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Products.FindAsync(new object[] { request.Id });
            if (entity == null) return Unit.Value;
            if (!string.IsNullOrEmpty(entity.ImageName))
            {
                _fileService.DeleteFile(entity.ImageName, "Products/Images");
            }
            _context.Products.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
