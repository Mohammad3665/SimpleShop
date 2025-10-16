using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Application.Dashboard.Queries.GetDashboardStatus
{
    public class GetDashboardStatusQuery : IRequest<DashboardStatusDTO>
    {
    }
}
