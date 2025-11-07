using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleShop.Domain.IdentityEntities;
using SimpleShop.Web.Models.Account;
using System.Threading.Tasks;

namespace SimpleShop.Api.Controllers.V1.Public
{
    [ApiController]
    [Route("api/v1/Public/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        //TODO : Register
        //GET : api/v1/Public/Account/Register
        [HttpGet]
        public ActionResult Register([FromQuery] string returnUrl = null) => Ok();

        //POST : api/v1/Public/Account/Register
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model, [FromQuery] string returnUrl = null)
        {
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                await _signInManager.SignInAsync(user, isPersistent: false);

                return StatusCode(201, new
                {
                    message = "Regiteration was successful.",
                    uerId = user.Id,
                    returnUrl = returnUrl
                });
            }
            var errors = result.Errors.Select(e => e.Description);
            return BadRequest(new
            {
                message = "Registration failed.",
                errors = errors
            });
        }


        //TODO : Login
        //GET : api/v1/Public/Account/Login
        [HttpGet]
        public ActionResult Login([FromQuery] string returnUrl = null) => Ok();

        //POST : api/v1/Public/Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password, [FromQuery] string returnUrl = null)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return NoContent();
            }
            return Unauthorized(new { message = "Invalid credentials." });
        }

        //TODO : Logout
        //POST : api/v1/Account/Logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return NoContent();
        }
    }
}
