using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SimpleShop.Application.Categories.Commands.EditCategory;
namespace SimpleShop.Application.Categories.Queries.GetCategoryDetails
{
    public class GetCategoryDetailsQuery : IRequest<EditCategoryCommand>
    {
        public int Id { get; set; }

    }
}
