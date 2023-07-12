using ExpenseTracking.Core.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracking.Core.Models.Concrete
{
    public class PaymentType : BaseModel
    {
        public string Name { get; set; }
        public ICollection<Expense> Expenses { get; set; }
    }
}
