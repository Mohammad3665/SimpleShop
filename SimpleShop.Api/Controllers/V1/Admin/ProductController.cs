using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleShop.Application.Categories.Queries.GetCategoriesList;
using SimpleShop.Application.Products.Commands.CreateProduct;
using SimpleShop.Application.Products.Commands.DeleteProduct;
using SimpleShop.Application.Products.Commands.EditProduct;
using SimpleShop.Application.Products.Queries.GetCategoryDropdown;
using SimpleShop.Application.Products.Queries.GetProductDetails;
using SimpleShop.Application.Products.Queries.GetProductsList;
using System.Threading.Tasks;

namespace SimpleShop.Api.Controllers.V1.Admin
{
    [ApiController]
    [Route("api/v1/Admin/Product/[controller]")]
    [Authorize(Roles = "Admin")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator) => _mediator = mediator;
        //TODO : (R) show products
        //GET : api/v1/Admin/Product
        public async Task<ActionResult> Index()
        {
            var query = new GetProductsListQuery();
            var model = await _mediator.Send(query);
            return Ok(model);
        }

        //TODO : (C) create new product
        //GET : api/v1/Admin/Product/Create
        public async Task<ActionResult> Create()
        {
            var categories = await _mediator.Send(new GetCategoriesListQuery());
            return Ok(new CreateProductCommand());
        }

        //TODO : (C) create new product
        //POST : api/v1/Admin/Product/Create
        [HttpPost]
        public async Task<ActionResult> Create(CreateProductCommand command)
        {
            try
            {
                var productId = await _mediator.Send(command);
                return CreatedAtAction(
                    actionName: "GetProductDetails",
                    routeValues: new { id = productId },
                    value: new { message = "Product added successfuly.", id = productId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred on the server.", error = ex.Message });
            }
        }

        //TODO : (C) create new product
        //GET : api/v1/Admin/Product/Edit
        [HttpGet("{id}")]
        public async Task<ActionResult<EditProductCommand>> Edit(int id)
        {
            var productQuery = new GetProductDetailsQuery { Id = id };
            var categoriesQuery = new GetCategoryDropdownQuery();

            var productCommand = await _mediator.Send(productQuery);
            var categories = await _mediator.Send(categoriesQuery);
            if (productCommand == null) return NotFound();
            return Ok(productCommand);
        }

        //TODO : (C) create new product
        //PUT : api/v1/Admin/Product/Create
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] EditProductCommand command)
        {
            if (id != command.Id) return BadRequest(new { message = "ID in the route must match the ID in the request body." });
            try
            {
                await _mediator.Send(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred on the server.", error = ex.Message });
            }
        }

        //TODO : (C) create new product
        //DELETE : api/v1/Admin/Product/Create
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteProductCommand { Id = id };
            try
            {
                await _mediator.Send(command);
                return NoContent();
            }
            catch
            {
                return StatusCode(500, new { message = "Delete failed! Probably due to related orders." });
            }
        }
    }
}
