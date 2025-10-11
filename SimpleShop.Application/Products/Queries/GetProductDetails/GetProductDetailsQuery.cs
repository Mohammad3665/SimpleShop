using MediatR;
using SimpleShop.Application.Products.Commands.EditProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Application.Products.Queries.GetProductDetails
{
    public class GetProductDetailsQuery : IRequest<EditProductCommand>
    {
        public int Id { get; set; }
    }
}
