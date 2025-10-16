using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Application.Products.Queries.GetPublicProductList
{
    public class PublicProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
        public bool IsInStock { get; set; }
        public string ThumbnailUrl { get; set; }
    }
}
