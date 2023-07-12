using ExpenseTracking.Core.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracking.Core.Models.Concrete
{
    public class Expense : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public double Cost { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
