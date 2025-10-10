using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using SimpleShop.Application.Categories.Commands.CreateCategory;
using SimpleShop.Application.Categories.Commands.DeleteCategory;
using SimpleShop.Application.Categories.Commands.EditCategory;
using SimpleShop.Application.Categories.Queries.GetCategoryDetails;
using SimpleShop.Application.Categories.Queries.GetCategoriesList;
using System.Threading.Tasks;
namespace SimpleShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator) => _mediator = mediator;

        //TODO : (R) show categories list
        //GET : Admin/Category/Index
        public async Task<IActionResult> Index()
        {
            var query = new GetCategoriesListQuery();
            var model = await _mediator.Send(query);

            return View(model);
        }

        //TODO : (C) Create new Category
        //GET : Admin/Category/Create
        public IActionResult Create() => View();

        //POST : Admin/Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCategoryCommand command)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _mediator.Send(command);
                    TempData["SuccessMessage"] = $"{command.Name} Category Created";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error during create category");
                }
            }
            return View(command);
        }

        //TODO : (U) Update category
        //GET : Admin/Category/Edit
        public async Task<IActionResult> Edit(int id)
        {
            var query = new GetCategoryDetailsQuery { Id = id };
            var model = await _mediator.Send(query);

            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }
        //POST : Admin/Category/Edit
        public async Task<IActionResult> Edit(int id, EditCategoryCommand command)
        {
            if (id != null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            try
            {
                await _mediator.Send(command);
                TempData["SuccessMessage"] = "Category edited successfuly";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Error during edit category");
                return View(command);
            }
        }

        //TODO : (D) Delete category
        //POST : Admin/Category/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var command = new DeleteCategoryCommand { Id = id };

            try
            {
                await _mediator.Send(command);
                TempData["SuccessMessage"] = "Category deleted successfuly";
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = "Delete failed! The category has related products and cannot be deleted.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
