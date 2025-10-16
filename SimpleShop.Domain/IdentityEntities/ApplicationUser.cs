using Microsoft.AspNetCore.Identity;
using SimpleShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Domain.IdentityEntities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        // Navigation Property
        public Cart Cart { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public bool IsActive { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
