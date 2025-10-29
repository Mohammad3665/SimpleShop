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
            public string FileName { get; set; }
            public bool IsMain { get; set; }

            public Product Product { get; set; }
    }
}
