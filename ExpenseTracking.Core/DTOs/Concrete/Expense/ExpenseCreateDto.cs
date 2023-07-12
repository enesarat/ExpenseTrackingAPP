using ExpenseTracking.Core.DTOs.Astract;
using ExpenseTracking.Core.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracking.Core.DTOs.Concrete.Expense
{
    public class ExpenseCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public int CategoryId { get; set; }
        public int PaymentTypeId { get; set; }
        public int UserId { get; set; }
    }
}
