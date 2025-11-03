using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleShop.Application.Orders.Commands.UpdateOrderStatus;
using SimpleShop.Application.Orders.Queries.GetAdminOrders;
using SimpleShop.Application.Orders.Queries.GetOrderDetails;
using SimpleShop.Domain.Enums;
using System.Threading.Tasks;

namespace SimpleShop.Api.Controllers.V1.Admin
{
    [ApiController]
    [Route("api/v1/Admin/[controller]/[action]")]
    [Authorize(Roles = "Admin")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator) => _mediator = mediator;

        //TODO : (R) Show orders list
        //GET : api/v1/Admin/Order/Index
        public async Task<ActionResult> Index()
        {
            var query = new GetAdminOrdersQuery();
            var model = await _mediator.Send(query);
            return Ok(model);
        }

        //TODO : Show details of order
        //GET : api/v1/Admin/Order/Details
        [HttpGet("{id}")]
        public async Task<ActionResult> Details(int id)
        {
            var query = new GetOrderDetailsQuery { OrderId = id};
            var model = await _mediator.Send(query);
            return Ok(model);
        }

        //TODO : Show update form
        //GET : api/v1/Admin/Order/UpdateStatus
        [HttpGet("{id}")]
        public async Task<ActionResult> UpdateStatus(int id)
        {
            var detailsQuery = new GetOrderDetailsQuery { OrderId= id };
            var model = await _mediator.Send(detailsQuery);
            if (model == null) return NotFound();
            var command = new UpdateOrderStatusCommand
            {
                OrderId = id,
                NewOrderStatus = (OrderStatus)Enum.Parse(typeof(OrderStatus), model.Status)
            };
            return Ok(command);
        }

        //TODO : Recieve new order status
        //PUT : api/v1/Admin/Order/UpdateStatus
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStatus(UpdateOrderStatusCommand command)
        {
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
    }
}
