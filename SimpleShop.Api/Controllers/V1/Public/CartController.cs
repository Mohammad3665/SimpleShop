using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleShop.Application.Carts.Commands.AddToCart;
using SimpleShop.Application.Carts.Commands.RemoveCartItem;
using SimpleShop.Application.Carts.Commands.UpdateCartItem;
using SimpleShop.Application.Carts.Queries.GetCart;
using SimpleShop.Application.Orders.Commands.CreateOrder;
using SimpleShop.Domain.Entities;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SimpleShop.Api.Controllers.V1.Public
{
    [ApiController]
    [Route("api/v1/Public/[controller]/[action]")]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CartController(IMediator mediator) => _mediator = mediator;

        //TODO : Show user cart
        //GET : api/v1/Public/Cart/Index
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var query = new GetCartQuery { UserId = userId };
            var model = await _mediator.Send(query);
            if (model == null) return NotFound();
            return Ok(model);
        }

        //TODO : Add to cart
        //POST : api/v1/Public/Cart/AddToCart
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var command = new AddToCartCommand
            {
                ProductId = productId,
                Quantity = quantity,
                UserId = userId
            };

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

        //TODO : Update cart
        //POST : api/v1/Public/Cart/UpdateQuantity
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int cartItemId, int newQuantity)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var command = new UpdateCartItemCommand
            {
                CartItemId = cartItemId,
                NewQuantity = newQuantity,
                UserId = userId
            };

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

        //TODO : Remove item
        //POST : api/v1/Public/Cart/RemoveItem
        [HttpPost]
        public async Task<IActionResult> RemoveItem(int cartItemId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var command = new RemoveCartItemCommand
            {
                CartItemId = cartItemId,
                UserId = userId
            };
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

        //TODO: show the address form
        //GET: api/v1/Public/Cart/Checkout
        [HttpGet]
        public ActionResult Checkout() => Ok(new CreateOrderCommand());

        //TODO: Place order
        //POST: api/v1/Public/Cart/PlaceOrder
        [HttpPost]
        public async Task<IActionResult> PlaceOrder(CreateOrderCommand command)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var orderId = await _mediator.Send(command);
                return CreatedAtAction(
                    actionName: "GetOrderDetails",
                    routeValues: new { id = orderId },
                    value: new { message = "Order successfuly placed.", orderId = orderId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred on the server.", error = ex.Message });
            }
        }
    }
}
