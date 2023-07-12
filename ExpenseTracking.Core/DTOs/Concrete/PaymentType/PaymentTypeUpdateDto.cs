using ExpenseTracking.Core.DTOs.Astract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracking.Core.DTOs.Concrete.PaymentType
{
    public class PaymentTypeUpdateDto : BaseDto
    {
        public string Name { get; set; }
    }
}
