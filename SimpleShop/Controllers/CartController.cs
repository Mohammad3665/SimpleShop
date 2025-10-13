using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleShop.Application.Carts.Commands.AddToCart;
using SimpleShop.Application.Carts.Commands.RemoveCartItem;
using SimpleShop.Application.Carts.Commands.UpdateCartItem;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SimpleShop.Web.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IMediator _mediator;
        public CartController(IMediator mediator) => _mediator = mediator;

        //TODO : Show Cart
        //GET : Cart/Index
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var query = new { UserId = userId };
            var model = await _mediator.Send(query);
            return View(model);
        }

        //TODO : Add to cart
        //POST : Product/AddToCart
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
                TempData["SuccessMessage"] = "Product Added to cart successfuly.";

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }

        //TODO : (U) Update cart
        //POST : Cart/UpdateQuantity
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                TempData["SuccessMessage"] = "Update quantuty updated";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }

        //TODO : (D) Delete remove item
        //POST : //Cart/RemoveItem
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                TempData["SuccessMessage"] = "Product deleted of Cart";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
