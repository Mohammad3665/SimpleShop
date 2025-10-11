using MediatR;
using SimpleShop.Application.Products.Queries.GetCategoryDropDown;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Application.Products.Queries.GetCategoryDropdown
{
    public class GetCategoryDropdownQuery : IRequest<List<CategoryDropdownDTO>>
    {

    }
}
