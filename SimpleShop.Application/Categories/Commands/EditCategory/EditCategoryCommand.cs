using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Application.Categories.Commands.EditCategory
{
    public class EditCategoryCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Category name can not be blank.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Category description can not be blank.")]
        public string Description { get; set; }
    }
}
