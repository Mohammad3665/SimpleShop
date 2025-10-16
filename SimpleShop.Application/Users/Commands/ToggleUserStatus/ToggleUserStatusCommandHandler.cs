using MediatR;
using Microsoft.AspNetCore.Identity;
using SimpleShop.Domain.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Application.Users.Commands.ToggleUserStatus
{
    public class ToggleUserStatusCommandHandler : IRequestHandler<ToggleUserStatusCommand, Unit>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public ToggleUserStatusCommandHandler(UserManager<ApplicationUser> userManager) => _userManager = userManager;

        public async Task<Unit> Handle(ToggleUserStatusCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null) throw new Exception("User not found.");

            user.IsActive = !user.IsActive;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new Exception($"Error in updating user status: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
            return Unit.Value;
        }
    }
}
