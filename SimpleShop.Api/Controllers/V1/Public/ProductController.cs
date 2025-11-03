using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleShop.Application.Products.Queries.GetProductDetails;
using SimpleShop.Application.Products.Queries.GetPublicProductList;
using SimpleShop.Application.Products.Queries.SearchProducts;
using System.Threading.Tasks;

namespace SimpleShop.Api.Controllers.V1.Public
{
    [ApiController]
    [Route("api/v1/Public/[controller]/[action]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator) => _mediator = mediator;

        //TODO : Main page
        //GET : api/v1/Public/Product/Index
        public async Task<ActionResult<List<PublicProductDTO>>> Index([FromQuery] int? categoryId, [FromQuery] string term)
        {
            object query;
            if (!string.IsNullOrWhiteSpace(term))
            {
                query = new SearchProductsQuery { SearchTerm = term };
            }
            else
            {
                query = new GetPublicProductListQuery { CategoryId = categoryId };
            }
            var model = await _mediator.Send(query);
            return Ok(model);
        }

        //TODO : Product details
        //GET : api/v1/Public/Product/Details
        [HttpGet("{id}")]
        public async Task<ActionResult<List<ProductDetailsDTO>>> Details (int productId)
        {
            var query = new GetProductDetailsQuery { Id = productId };
            var model = await _mediator.Send(query);
            if(model == null) return NotFound();
            return Ok(model);
        }
    }
}
