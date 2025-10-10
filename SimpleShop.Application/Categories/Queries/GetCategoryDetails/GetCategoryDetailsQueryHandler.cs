using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Application.Categories.Commands.EditCategory;
using SimpleShop.Application.Common.Interfaces;

namespace SimpleShop.Application.Categories.Queries.GetCategoryDetails
{
    public class GetCategoryDetailsQueryHandler : IRequestHandler<GetCategoryDetailsQuery, EditCategoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public GetCategoryDetailsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<EditCategoryCommand> Handle(GetCategoryDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (entity is null)
            {
                return null;
            }

            return new EditCategoryCommand
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description
            };
                
        }
    }
}
