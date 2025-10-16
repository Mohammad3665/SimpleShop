using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Application.Users.Queries.GetUserManagmentList
{
    public class UserManagmentDTO
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Role { get; set; }
    }
}
