using ExpenseTracking.Core.DTOs.Astract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracking.Core.DTOs.Concrete.Expense
{
    public class ExpenseSumDto : BaseDto
    {
        public double Sum { get; set; }
        public IEnumerable<ExpenseDto> Expenses { get; set; }
    }
}
