using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleShop.Application.Orders.Queries.GetOrderDetails;
using SimpleShop.Application.Orders.Queries.GetUserOrders;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SimpleShop.Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator) => _mediator = mediator;
        
        //TODO : Show user orders history
        //GET : Order/Index
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var query = new GetUserOrdersQuery { UserId = userId };
            var model = await _mediator.Send(query);
            return View(model);
        }

        //TODO : Show order ditails
        //GET : Order/Details
        public IActionResult Details(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var query = new GetOrderDetailsQuery {  UserId = userId , OrderId = id };
            var model = _mediator.Send(query);
            if (model == null) return NotFound();
            return View(model);
        }
    }
}
