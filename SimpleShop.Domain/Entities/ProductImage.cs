using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Domain.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ImageFileName { get; set; }
        public bool IsMainImage { get; set; }

        //Forigen Keys
        public Product Product { get; set; }
    }
}
