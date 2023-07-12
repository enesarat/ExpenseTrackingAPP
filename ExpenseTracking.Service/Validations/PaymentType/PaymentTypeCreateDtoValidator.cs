using ExpenseTracking.Core.DTOs.Concrete.PaymentType;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracking.Service.Validations.PaymentType
{
    public class PaymentTypeCreateDtoValidator : AbstractValidator<PaymentTypeCreateDto>
    {
        public PaymentTypeCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage(" {PropertyName} must have any value ").NotEmpty().WithMessage(" {PropertyName} is required ");
        }
    }
}
