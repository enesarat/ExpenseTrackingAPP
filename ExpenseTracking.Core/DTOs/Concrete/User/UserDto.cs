using ExpenseTracking.Core.DTOs.Astract;
using ExpenseTracking.Core.DTOs.Concrete.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracking.Core.DTOs.Concrete.User
{
    public class UserDto : BaseDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public RoleDto RoleProp { get; set; }
    }
}
