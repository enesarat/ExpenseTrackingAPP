using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracking.Core.Model.Concrete
{
    public class PaymentType
    {
        public string Name { get; set; }
        public ICollection<Expense> Expenses { get; set; }
    }
}
