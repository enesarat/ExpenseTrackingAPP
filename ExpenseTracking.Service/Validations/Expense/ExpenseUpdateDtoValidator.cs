using ExpenseTracking.Core.DTOs.Concrete.Expense;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracking.Service.Validations.Expense
{
    public class ExpenseUpdateDtoValidator : AbstractValidator<ExpenseUpdateDto>
    {
        public ExpenseUpdateDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage(" {PropertyName} must have any value ").NotEmpty().WithMessage(" {PropertyName} is required ");
            RuleFor(x => x.Description).NotNull().WithMessage(" {PropertyName} must have any value ").NotEmpty().WithMessage(" {PropertyName} is required ");
            RuleFor(x => x.PaymentTypeId).InclusiveBetween(1, int.MaxValue).WithMessage(" {PropertyName} must be greater than 0 ");
            RuleFor(x => x.Cost).InclusiveBetween(1, decimal.MaxValue).WithMessage(" {PropertyName} must be greater than 0 ");
            RuleFor(x => x.CategoryId).InclusiveBetween(1, int.MaxValue).WithMessage(" {PropertyName} must be greater than 0 ");
        }
    }
}
