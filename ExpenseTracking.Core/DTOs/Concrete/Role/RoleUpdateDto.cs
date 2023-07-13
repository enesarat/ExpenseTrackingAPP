using ExpenseTracking.Core.DTOs.Astract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracking.Core.DTOs.Concrete.Role
{
    public class RoleUpdateDto : BaseDto
    {
        public string Name { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
