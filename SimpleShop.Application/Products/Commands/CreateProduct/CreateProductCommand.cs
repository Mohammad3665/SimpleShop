using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<int>
    {
        [Required(ErrorMessage = "Product name can not be blank.")]
        [StringLength(30)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Prdoduct description can not be blank.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Product price can not be blank.")]
        [Range(0.01, 1000000, ErrorMessage = "Price must more than 0.")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Product stock can not be blank.")]
        public int Stock { get; set; }
        [Required(ErrorMessage = "Choose category is required.")]
        public int CategoryId { get; set; }
        //public IFormFile ImageFile { get; set; }
        [Required(ErrorMessage = "Product Image can not be blank.")]
        public List<IFormFile> ImageFiles { get; set; }
    }
}
