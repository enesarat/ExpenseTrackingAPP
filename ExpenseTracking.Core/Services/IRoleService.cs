using ExpenseTracking.Core.DTOs.Concrete.Category;
using ExpenseTracking.Core.DTOs.Concrete.Response;
using ExpenseTracking.Core.DTOs.Concrete.Role;
using ExpenseTracking.Core.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracking.Core.Services
{
    public interface IRoleService : IGenericService<Role, RoleDto>
    {
        public Task<CustomResponse<NoContentResponse>> AddAsync(RoleCreateDto roleCreateDto);
        public Task<CustomResponse<NoContentResponse>> UpdateAsync(RoleUpdateDto roleUpdateDto);
    }
}
