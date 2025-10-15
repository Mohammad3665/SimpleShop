using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleShop.Application.Orders.Commands.UpdateOrderStatus;
using SimpleShop.Application.Orders.Queries.GetAdminOrders;
using SimpleShop.Application.Orders.Queries.GetOrderDetails;
using SimpleShop.Domain.Enums;
using System.Threading.Tasks;

namespace SimpleShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator) => _mediator = mediator;
        //TODO : Show list of orders
        //GET : Admin/Order/Index
        public async Task<IActionResult> Index()
        {
            var model = await _mediator.Send(new GetAdminOrdersQuery());
            return View(model);
        }

        //TODO : Show Details of order
        //GET : Admin/Order/Details
        public async Task<IActionResult> Details(int id)
        {
            var query = new GetOrderDetailsQuery { OrderId = id };
            var model = await _mediator.Send(query);
            if (model == null) return NotFound();
            return View(model);
        }

        //TODO : Show update form
        //GET : Admin/Order/UpdateStatus
        public async Task<IActionResult> UpdateStatus(int id)
        {
            var detailsQuery = new GetOrderDetailsQuery { OrderId = id };
            var model = await _mediator.Send(detailsQuery);
            if (model == null) return NotFound();

            ViewBag.Statuses = new SelectList(Enum.GetValues(typeof(OrderStatus)));

            var command = new UpdateOrderStatusCommand
            {
                OrderId = id,
                NewOrderStatus = (OrderStatus)Enum.Parse(typeof(OrderStatus), model.Status)
            };
            ViewData["OrderDetails"] = model;
            return View(command);
        }

        //TODO : Recieve new order status
        //POST : Admin/Order/UpdateStatus
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(UpdateOrderStatusCommand command)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Statuses = new SelectList(Enum.GetValues(typeof(OrderStatus)));
                return View(command);
            }

            try
            {
                await _mediator.Send(command);
                TempData["SuccessMessage"] = $"Order status {command.OrderId} successfully changed to {command.NewOrderStatus}.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error updating status: " + ex.Message;
                ViewBag.Statuses = new SelectList(Enum.GetValues(typeof(OrderStatus)));
                return View(command);
            }
        }
    }
}
