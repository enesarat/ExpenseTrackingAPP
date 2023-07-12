using ExpenseTracking.Core.DTOs.Astract;
using ExpenseTracking.Core.DTOs.Concrete.Category;
using ExpenseTracking.Core.DTOs.Concrete.PaymentType;
using ExpenseTracking.Core.DTOs.Concrete.User;
using ExpenseTracking.Core.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracking.Core.DTOs.Concrete.Expense
{
    public class ExpenseDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Cost { get; set; }
        public string Category { get; set; }
        public CategoryDto CategoryProp { get; set; }
        public string PaymentType { get; set; }
        public PaymentTypeDto PaymentTypeDto { get; set; }
        public string User { get; set; }
        public UserDto UserProp { get; set; }
    }
}
