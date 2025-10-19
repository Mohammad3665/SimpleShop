using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Application.Products.Queries.GetProductDetails
{
        public class ProductDetailsDTO
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public bool IsInStock { get; set; }
            public string ImageFileName { get; set; }
            public string CategoryName { get; set; }
        }
}
