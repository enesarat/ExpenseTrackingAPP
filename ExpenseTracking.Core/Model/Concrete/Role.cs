using ExpenseTracking.Core.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracking.Core.Model.Concrete
{
    public class Role : BaseModel
    {
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }

    }
}
