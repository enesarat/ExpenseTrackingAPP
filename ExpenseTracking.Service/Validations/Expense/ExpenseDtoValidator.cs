using ExpenseTracking.Core.DTOs.Concrete.Expense;
using ExpenseTracking.Core.DTOs.Concrete.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracking.Service.Validations.Expense
{
    public class ExpenseDtoValidator : AbstractValidator<ExpenseDto>
    {
        public ExpenseDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage(" {PropertyName} must have any value ").NotEmpty().WithMessage(" {PropertyName} is required ");
            RuleFor(x => x.Description).NotNull().WithMessage(" {PropertyName} must have any value ").NotEmpty().WithMessage(" {PropertyName} is required ");
            RuleFor(x => x.TransactionDate).NotNull().WithMessage(" {PropertyName} must have any value ").NotEmpty().WithMessage(" {PropertyName} is required ");
            RuleFor(x => x.Cost).InclusiveBetween(1, decimal.MaxValue).WithMessage(" {PropertyName} must be greater than 0 ");
        }
    }
}
