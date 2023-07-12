using ExpenseTracking.Core.DTOs.Concrete.Category;
using ExpenseTracking.Core.DTOs.Concrete.PaymentType;
using ExpenseTracking.Core.DTOs.Concrete.Response;
using ExpenseTracking.Core.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracking.Core.Services
{
    public interface IPaymentTypeService : IGenericService<PaymentType, PaymentTypeDto>
    {
        public Task<CustomResponse<NoContentResponse>> AddAsync(PaymentTypeCreateDto paymentTypeCreateDto);
        public Task<CustomResponse<NoContentResponse>> UpdateAsync(PaymentTypeUpdateDto paymentTypeUpdateDto);
    }
}
