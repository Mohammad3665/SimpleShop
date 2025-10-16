using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleShop.Application.Dashboard.Queries.GetDashboardStatus;
using System.Threading.Tasks;

namespace SimpleShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly IMediator _mediator;
        public DashboardController(IMediator mediator) => _mediator = mediator;
        public async Task<IActionResult> Index()
        {
            var statsQuery = new GetDashboardStatusQuery();
            var model = await _mediator.Send(statsQuery);
            return View(model);
        }
    }
}
