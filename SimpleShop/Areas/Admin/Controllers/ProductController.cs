using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleShop.Application.Products.Commands.CreateProduct;
using SimpleShop.Application.Products.Commands.DeleteProduct;
using SimpleShop.Application.Products.Commands.EditProduct;
using SimpleShop.Application.Products.Queries.GetCategoryDropdown;
using SimpleShop.Application.Products.Queries.GetProductDetails;
using SimpleShop.Application.Products.Queries.GetProductsList;
using System.Threading.Tasks;

namespace SimpleShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator) => _mediator = mediator;

        //TODO : (R) show products
        //GET : Admin/Product
        public IActionResult Index()
        {
            var model = _mediator.Send(new GetProductsListQuery());
            return View(model);
        }

        //TODO : (C) create new product
        //GET : Admin/Product/Create
        public async Task<IActionResult> Create()
        {
            var categories = await _mediator.Send(new GetCategoryDropdownQuery());
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(new CreateProductCommand());
        }

        //POST : Admin/Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductCommand command)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _mediator.Send(command);
                    TempData["SuccessMessage"] = "Product Created successfuly";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error during create product" + ex.Message);
                }
            }
            var categories = await _mediator.Send(new GetCategoryDropdownQuery());
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(command);
        }

        //TODO : (U) Update product
        //GET : Admin/Product/Edit
        public async Task<IActionResult> Edit(int id)
        {
            var productQuery = new GetProductDetailsQuery { Id = id };
            var categoriesQuery = new GetCategoryDropdownQuery();

            var productCommand = await _mediator.Send(productQuery);
            var categories = await _mediator.Send(categoriesQuery);
            if (productCommand == null) return NotFound();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", productCommand.CategoryId);

            return View(productCommand);
        }

        //POST : Admin/Product/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] EditProductCommand command)
        {
            if (id != command.Id || !ModelState.IsValid)
            {
                return NotFound();
            }
            try
            {
                await _mediator.Send(command);
                TempData["SuccessMessage"] = "Product edited successfuly";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error during edit product" + ex.Message);
                return View(command);
            }
        }

        //TODO : (D) Delete product
        //POST : Admin/Product/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var command = new DeleteProductCommand { Id = id };

            try
            {
                await _mediator.Send(command);
                TempData["SuccessMessage"] = "Product deleted successfuly";
            }
            catch
            {
                TempData["ErrorMessage"] = "Delete failed! Probably due to related orders.";
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
