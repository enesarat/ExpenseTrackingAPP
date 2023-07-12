using ExpenseTracking.Core.DTOs.Concrete.Category;
using ExpenseTracking.Core.DTOs.Concrete.Response;
using ExpenseTracking.Core.DTOs.Concrete.User;
using ExpenseTracking.Core.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracking.Core.Services
{
    public interface IUserService : IGenericService<User, UserDto>
    {
        public Task<CustomResponse<NoContentResponse>> AddAsync(UserCreateDto userCreateDto);
        public Task<CustomResponse<NoContentResponse>> UpdateAsync(UserUpdateDto userUpdateDto);

    }
}
