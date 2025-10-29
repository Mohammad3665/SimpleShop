using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleShop.Application.Products.Queries.GetProductDetails;
using SimpleShop.Application.Products.Queries.GetPublicProductList;
using SimpleShop.Application.Products.Queries.SearchProducts;
using System.Threading.Tasks;

namespace SimpleShop.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator) => _mediator = mediator;

        //TODO : Main page
        //GET : Product/Index
        public async Task<IActionResult> Index(int? categoryId, string term)
        {
            object query;
            if (!string.IsNullOrWhiteSpace(term))
            {
                query = new SearchProductsQuery { SearchTerm = term };
                ViewBag.SearchTerm = term;
            }
            else
            {
                query = new GetPublicProductListQuery { CategoryId = categoryId };
                ViewBag.SearchTerm = null;
            }
            var model = await _mediator.Send(query);
            ViewBag.CategoryId = categoryId;
            ViewBag.SearchTerm = term;
            return View(model);
        }

        //TODO : Prdouc details
        //GET : Product/Details
        public async Task<IActionResult> Details(int id)
        {
            var query = new GetProductDetailsQuery { Id = id };
            var model = await _mediator.Send(query);
            if (model == null) return NotFound();
            return View(model);
        }
    }
}
