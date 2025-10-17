using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleShop.Application.Users.Commands.ToggleUserStatus;
using SimpleShop.Application.Users.Queries.GetUserManagmentList;
using System.Threading.Tasks;

namespace SimpleShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator) => _mediator = mediator;
        
        //TODO : Users list
        //GET : Admin/User/Index
        public async Task<IActionResult> Index()
        {
            var model = await _mediator.Send(new GetUserManagmentListQuery());
            return View(model);
        }

        //TODO : Active|Deactive Users
        //POST : Admin/User/ToggleStatus
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(string userId)
        {
            try
            {
                await _mediator.Send(new ToggleUserStatusCommand { UserId = userId });
                TempData["SuccessMessage"] = "User status edited successfuly";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Operation error" + ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
