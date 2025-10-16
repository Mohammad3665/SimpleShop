using MediatR;
using SimpleShop.Application.Products.Queries.GetPublicProductList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Application.Products.Queries.SearchProducts
{
    public class SearchProductsQuery : IRequest<List<PublicProductDTO>>
    {
        public string SearchTerm { get; set; }
    }
}
