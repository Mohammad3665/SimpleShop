using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Application.Common.Interfaces;
using SimpleShop.Domain.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Application.Users.Queries.GetUserManagmentList
{
    public class GetUserManagmentListQueryHandler : IRequestHandler<GetUserManagmentListQuery, List<UserManagmentDTO>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public GetUserManagmentListQueryHandler(UserManager<ApplicationUser> userManager) => _userManager = userManager;

        public async Task<List<UserManagmentDTO>> Handle(GetUserManagmentListQuery request, CancellationToken cancellationToken)
        {
            var users = await _userManager.Users.ToListAsync(cancellationToken);
            var userDTOs = new List<UserManagmentDTO>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                userDTOs.Add(new UserManagmentDTO
                {
                    UserId = user.Id,
                    Email = user.Email,
                    IsActive = user.IsActive,
                    RegistrationDate = user.RegistrationDate,
                    Role = string.Join(", ", roles)
                });
            }
            return userDTOs;
        }
    }
}
