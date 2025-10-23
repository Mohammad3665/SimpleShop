using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleShop.Application.Categories.Queries.GetCategoriesList;
using SimpleShop.Web.Models.ComponentModels;
using System.Threading.Tasks;

public class CategoryNavigationViewComponent : ViewComponent
{
    private readonly IMediator _mediator;

    // تزریق MediatR
    public CategoryNavigationViewComponent(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var categories = await _mediator.Send(new GetCategoriesListQuery());
        var categoryId = ViewContext.ViewBag.CategoryId as int?;

        // 3. ایجاد و ارسال مدل جدید
        var componentModel = new CategoryNavigationComponentModel
        {
            Categories = categories,
            CurrentCategoryId = categoryId
        };
        return View(componentModel);
    }
}