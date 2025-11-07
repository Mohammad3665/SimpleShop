using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleShop.Application.Users.Commands.ToggleUserStatus;
using SimpleShop.Application.Users.Queries.GetUserManagmentList;
using System.Threading.Tasks;

namespace SimpleShop.Api.Controllers.V1.Admin
{
    [ApiController]
    [Route("api/v1/Admin/[controller]/[action]")]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator) => _mediator = mediator;

        //TODO : Users list
        //GET : api/v1/Admin/User/Index
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var query = new GetUserManagmentListQuery();
            var model = await _mediator.Send(query);
            return Ok(model);
        }

        //TODO : Active|Deactive user
        //GET : api/v1/Admin/Order/ToggleStatus
        [HttpGet("{userId}")]
        public async Task<ActionResult> ToggleStatus(string userId)
        {
            try
            {
                await _mediator.Send(new ToggleUserStatusCommand { UserId = userId });
                return NoContent();
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred on the server.", error = ex.Message });
            }
        }
    }
}
