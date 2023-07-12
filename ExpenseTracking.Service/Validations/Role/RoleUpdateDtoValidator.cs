using ExpenseTracking.Core.DTOs.Concrete.Role;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracking.Service.Validations.Role
{
    public class RoleUpdateDtoValidator : AbstractValidator<RoleUpdateDto>
    {
        public RoleUpdateDtoValidator()
        {
            RuleFor(x => x.Id).InclusiveBetween(1, int.MaxValue).WithMessage(" {PropertyName} must be greater than 0 ");
            RuleFor(x => x.Name).NotNull().WithMessage(" {PropertyName} must have any value ").NotEmpty().WithMessage(" {PropertyName} is required ");
        }
    }
}
