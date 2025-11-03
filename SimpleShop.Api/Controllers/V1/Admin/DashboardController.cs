using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleShop.Application.Dashboard.Queries.GetDashboardStatus;
using System.Threading.Tasks;

namespace SimpleShop.Api.Controllers.V1.Admin
{
    [ApiController]
    [Route("api/v1/Admin/[controller]/[action]")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DashboardController(IMediator mediator) => _mediator = mediator;
        public async Task<ActionResult<DashboardStatusDTO>> Index()
        {
            var query = new GetDashboardStatusQuery();
            var model = await _mediator.Send(query);
            return Ok(model);
        }
    }
}
