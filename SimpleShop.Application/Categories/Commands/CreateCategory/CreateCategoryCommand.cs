using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<int>
    {
        [Required(ErrorMessage = "Category name can not be blank.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Category description can not be blank.")]
        public string Description { get; set; }
    }
}
