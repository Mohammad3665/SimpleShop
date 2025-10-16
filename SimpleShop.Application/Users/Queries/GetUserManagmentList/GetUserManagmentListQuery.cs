using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Application.Users.Queries.GetUserManagmentList
{
    public class GetUserManagmentListQuery : IRequest<List<UserManagmentDTO>>
    {
    }
}
