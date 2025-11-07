using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleShop.Application.Categories.Commands.CreateCategory;
using SimpleShop.Application.Categories.Commands.DeleteCategory;
using SimpleShop.Application.Categories.Commands.EditCategory;
using SimpleShop.Application.Categories.Queries.GetCategoriesList;
using SimpleShop.Application.Categories.Queries.GetCategoryDetails;
using System.Threading.Tasks;

namespace SimpleShop.Api.Controllers.V1.Admin
{
    [ApiController]
    [Route("api/v1/Admin/[controller]/[action]")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator) => _mediator = mediator;
        //TODO : (R) show categories list
        //GET : api/v1/Admin/Categroy/Index
        [HttpGet]
        public async Task<ActionResult<CategoryListDTO>> Index()
        {
            var query = new GetCategoriesListQuery();
            var model = await _mediator.Send(query);
            return Ok(model);
        }

        //TODO : (C) Create new Categroy
        //GET : api/v1/Admin/Category/Create
        [HttpGet("Create")]
        public ActionResult<CreateCategoryCommand> GetCreateTemplate() => new CreateCategoryCommand();

        //POST: api/v1/Admin/Category/Create
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryCommand command)
        {
            try
            {
                var categroyId = await _mediator.Send(command);
                return CreatedAtAction(
                    actionName: "GetCategoryDetails",
                    routeValues: new { id = categroyId },
                    value: new { message = "Category created successfuly.", id = categroyId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred on the server.", error = ex.Message });
            }
        }

        //TODO : (U) Update category
        //GET : api/v1/Admin/Category/Edit
        [HttpGet("{id}")]
        public async Task<ActionResult<EditCategoryCommand>> Edit(int id)
        {
            var query = new GetCategoryDetailsQuery { Id = id};
            var model = await _mediator.Send(query);
            if (model == null) return NotFound();
            return Ok(model);
        }

        //PUT: api/v1/Admin/Category/Edit
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, EditCategoryCommand command)
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

        //TODO : (D) Delete category
        //DELETE : api/v1/Admin/Category/Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteCategoryCommand { Id = id };
            try
            {
                await _mediator.Send(command);
                return NoContent();
            }
            catch
            {
                return BadRequest(new
                {
                    message = "Delete failed! The category has related products and cannot be deleted."
                });
            }
        }
    }
}
