using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleShop.Application.Orders.Queries.GetOrderDetails;
using SimpleShop.Application.Orders.Queries.GetUserOrders;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SimpleShop.Api.Controllers.V1.Public
{
    [ApiController]
    [Route("api/v1/Public/[controller]/[action]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator) => _mediator = mediator;

        //TODO : Show user orders history
        //GET : api/v1/Public/Order/Index
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var query = new GetUserOrdersQuery { UserId = userId };
            var model = await _mediator.Send(query);
            if (model == null) return NotFound();
            return Ok(model);
        }

        //TODO : Show user order details
        //GET : api/v1/Public/Order/Details
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var query = new GetOrderDetailsQuery { UserId = userId };
            var model = await _mediator.Send(query);
            if (model == null) return NotFound();
            return Ok(model);
        }
    }
}
