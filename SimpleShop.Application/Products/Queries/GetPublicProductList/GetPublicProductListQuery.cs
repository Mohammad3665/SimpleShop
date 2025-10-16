using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Application.Products.Queries.GetPublicProductList
{
    public class GetPublicProductListQuery : IRequest<List<PublicProductDTO>>
    {
        public int? CategoryId { get; set; }
    }
}
